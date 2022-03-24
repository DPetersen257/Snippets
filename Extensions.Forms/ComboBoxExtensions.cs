namespace Extensions.Forms;
public static class ComboBoxExtensions
{
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lParam);

    /// <summary>
    /// Sets the place holder text. PlaceHolder text removed after selecting an item.
    /// <para/><see href="https://stackoverflow.com/a/60308321"/>
    /// </summary>
    /// <param name="box">The box.</param>
    /// <param name="text">The text.</param>
    public static void SetPlaceHolderText(this ComboBox box, string text)
        => _ = SendMessage(box.Handle, 0x1703, 0, text);
}
