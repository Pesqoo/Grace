namespace Grace.Event;

public class ContextMenuEventArgs : EventArgs
{
    public ContextMenuEventType EventType { get; set; }
    public int ItemIndex { get; set; }

    public ContextMenuEventArgs(ContextMenuEventType eventType, int itemIndex)
    {
        EventType = eventType;
        ItemIndex = itemIndex;
    }
}

public enum ContextMenuEventType
{
    REMOVE,
    SET_ITEM,
    SET_DROPGROUP,
    RENAME,
}