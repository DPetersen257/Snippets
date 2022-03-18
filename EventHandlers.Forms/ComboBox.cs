namespace EpiqExtensions.Forms.EventHandlers;

public static class ComboBoxHandlers
{
    /// <summary>
    /// Automatically resizes a ComboBox dropdown width to fit the contents
    /// </summary>
    /// <param name="sender">Combobox that fired the event</param>
    /// <param name="e">Event arguments</param>
    private static void ComboBoxAutoWidthDropDown(object sender, EventArgs e)
    {
        var senderComboBox = (ComboBox)sender;

        int width = senderComboBox.DropDownWidth;

        Graphics g = senderComboBox.CreateGraphics();

        Font font = senderComboBox.Font;

        int vertScrollBarWidth =
            senderComboBox.Items.Count > senderComboBox.MaxDropDownItems ?
                         SystemInformation.VerticalScrollBarWidth
                         : 0;

        int newWidth;

        var displaymember = senderComboBox.DisplayMember;

        foreach (var item in senderComboBox.Items)

        {
            if (item is string s)
                newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;
            else

            {
                string displayedString = (string)item.GetType().GetProperty(displaymember)?.GetValue(item, null);

                newWidth = (int)g.MeasureString(displayedString, font).Width + vertScrollBarWidth;
            }

            if (width < newWidth)
                width = newWidth;
        }

        senderComboBox.DropDownWidth = width;
    }
}