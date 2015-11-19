using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODaemon
{
    public class RSSListBoxItem
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; }

        public RSSListBoxItem(long id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
