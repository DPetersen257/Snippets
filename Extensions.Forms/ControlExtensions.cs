using System.ComponentModel;

namespace EpiqExtensions.Forms;
public static class ControlExtensions
{
    /// <summary>
    /// Extension method to automatically use thread-safe control invoke if required - async version
    /// </summary>
    /// <param name="c">Control to update in a thread-safe manner</param>
    /// <param name="act">Action the control should take</param>
    public static void RunAsync(this Control c, Action act)
    {
        if (c.InvokeRequired)
            c.BeginInvoke(act);
        else
        {
            act.Invoke();
        }
    }

    /// <summary>
    /// Extension method to automatically use thread-safe control invoke if required - sync version
    /// </summary>
    /// <param name="c">Control to update in a thread-safe manner</param>
    /// <param name="act">Action the control should take</param>
    public static void RunSync(this Control c, Action act)
    {
        if (c.InvokeRequired)
            c.Invoke(act);
        else
        {
            act.Invoke();
        }
    }

    /// <summary>
    /// Determines whether the control is in design mode.
    /// Used to fix a Microsoft bug.
    /// When calling my serviceProvider from a control's constructor, the designer will break.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if control is in design mode; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInDesignMode(this Control ctrl)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return true;

        while (ctrl != null)
        {
            if (ctrl.Site?.DesignMode == true)
                return true;
            ctrl = ctrl.Parent;
        }

        return System.Diagnostics.Process.GetCurrentProcess().ProcessName.Contains("devenv", StringComparison.OrdinalIgnoreCase);
    }

}