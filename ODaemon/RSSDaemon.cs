using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;

namespace ODaemon
{
    public enum RSSNotificationType
    {
        None,
        Error,
        Spy,
        Irak,
    };

    public class RSSNotification
    {
        public long id;
        public RSSNotificationType notificationType;
        public string description;
        public DateTime dateTime;
        public bool seen;
    }

    public struct RSSFileEntry
    {
        public long id;
        public bool seen;
    }

    public class RSSDaemon
    {
        private string feedFile = "feed";
        private List<RSSNotification> notificationList = new List<RSSNotification>();

        public RSSDaemon()
        {
        }

        public DateTime fromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }

        private Task<string> connect(string feedUrl, string username, string password)
        {
            return Task.Run(() =>
            {
                try {
                    feedUrl = feedUrl.Replace("feed://", "http://");
                    Match m = Regex.Match(feedUrl, @"http://([^/]*)");
                    if (!m.Success)
                    {
                        throw new Exception("Keine gültige URL - bitte kopiere den exakten Feedlink aus dem Spiel.");
                    }
                    string host = m.Groups[1].Value;

                    Uri uri = new Uri(feedUrl);
                    ServicePoint p = ServicePointManager.FindServicePoint(uri);
                    p.Expect100Continue = false;
                    HttpWebRequest req = HttpWebRequest.Create(uri) as HttpWebRequest;

                    req.ServicePoint.Expect100Continue = false;
                    req.Method = "GET";
                    req.Host = host;
                    req.UserAgent = "FeedY";
                    req.Accept = "*/*";
                    req.Headers.Add("Accept-Encoding", "gzip, deflate");
                    req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    req.AllowAutoRedirect = false;

                    // We need to use the PreAuthenticate cache in conjunction with a basic authorisation to avoid any 401 challenges
                    string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(username + ":" + password));
                    req.PreAuthenticate = true;
                    req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                    req.Headers.Add("Authorization", auth); 

                    using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader sr = new StreamReader(resp.GetResponseStream());
                            string s = sr.ReadToEnd();
                            sr.Close();
                            return s;
                        }
                        else
                        {
                            throw new Exception("Server antwortete mit Statuscode: " + resp.StatusCode);
                        }
                    }
                }
                catch (WebException e)
                {
                    throw new Exception("Es konnte keine Verbindung aufgebaut werden. Bitte prüfe den Feedlink und deine Internetverbindung.\n\nFehler: " + e.Message);
                }
                catch (Exception e)
                {
                    throw new Exception("Fehler beim Verbinden:\n" + e.Message);
                }
            });
        }

        public void importFeedEntries()
        {
            if (!File.Exists(feedFile))
            {
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(feedFile, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                int numItems = reader.ReadInt32();
                for (int i = 0; i < numItems; i++)
                {
                    RSSNotification r = new RSSNotification();
                    r.id = reader.ReadInt64();
                    r.notificationType = (RSSNotificationType) reader.ReadInt32();
                    r.seen = reader.ReadBoolean();
                    r.dateTime = DateTime.Parse(reader.ReadString());
                    r.description = reader.ReadString();
                    notificationList.Add(r);
                }
                reader.Close();
            }
        }

        public void exportFeedEntries() 
        {
            // delete a file that is not from today
            FileMode fMode = FileMode.OpenOrCreate;
            if(File.GetLastWriteTime(feedFile).Date != DateTime.Today) {
                fMode = FileMode.Create;
            }

            int numItems = notificationList.Count;
            using (BinaryWriter writer = new BinaryWriter(File.Open(feedFile, fMode, FileAccess.Write, FileShare.Write)))
            {
                writer.Write(numItems);
                foreach (RSSNotification item in notificationList)
                {
                    writer.Write(item.id);
                    writer.Write((int) item.notificationType);
                    writer.Write(item.seen);
                    writer.Write(item.dateTime.ToString("yy-MM-dd HH:mm:ss"));
                    writer.Write(item.description);
                }
                writer.Close();
            }
        }

        public async Task< List<RSSNotification> > fetch(string feedUrl, string username, string password)
        {
            try
            {
                string rss = await connect(feedUrl, username, password);
                XmlReader reader = XmlReader.Create(new StringReader(rss));
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem item in feed.Items)
                {
                    if (DateTime.Today != item.PublishDate.Date)
                    {
                        continue;
                    }

                    string idStr = Regex.Match(item.Links[0].Uri.ToString(), @"item=([^&]*)").Groups[1].Value;
                    long itemId = long.Parse(idStr);
                    if (notificationList.Exists(x => x.id == itemId))
                    {
                        continue;
                    }

                    String subject = item.Title.Text;
                    // TODO: extended filtering
                    if (subject.IndexOf("Spionageaktion") != -1)
                    {
                        string spier = Regex.Match(item.Summary.Text, @"\(([^)]*)").Groups[1].Value; 
                        string target = Regex.Match(item.Title.Text, @"Spionageaktion auf (\[[^\]]*\])").Groups[1].Value;
                        string timeStr = item.PublishDate.ToString("[HH:mm]");

                        notificationList.Add(new RSSNotification()
                        {
                            id = itemId,
                            notificationType = RSSNotificationType.Spy,
                            description = timeStr + " " + spier + " -> " + target,
                            dateTime = item.PublishDate.DateTime,
                        });
                    }
                }
                exportFeedEntries();
                return notificationList;
            } catch(Exception e) {
                throw e;
            }
        }

        public void markItemAsRead(long itemId)
        {
            RSSNotification n = notificationList.Find(x => x.id == itemId);
            n.seen = true;
        }

        public List<RSSNotification> getNotificationList()
        {
            return notificationList;
        }
    }
}
