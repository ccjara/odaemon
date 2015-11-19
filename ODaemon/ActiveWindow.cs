using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;

public class ActiveWindow
{
    /// <summary>Returns true if the current application has focus, false otherwise</summary>
    public static bool isActivated()
    {
        var activatedHandle = GetForegroundWindow();
        if (activatedHandle == IntPtr.Zero)
        {
            return false;       // No window is currently activated
        }

        var procId = Process.GetCurrentProcess().Id;
        int activeProcId;
        GetWindowThreadProcessId(activatedHandle, out activeProcId);

        return activeProcId == procId;
    }


    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
}