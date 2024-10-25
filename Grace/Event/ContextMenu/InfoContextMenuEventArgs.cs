namespace Grace.Event.ContextMenu;

public class InfoContextMenuEventArgs(int itemIndex) : EventArgs
{
    public int ItemIndex { get; set; } = itemIndex;
}
