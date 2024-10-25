namespace Grace.Event.ContextMenu;

public class DeleteSubIdEventArgs(string nodeTag) : EventArgs
{
    public string NodeTag { get; set; } = nodeTag;
}
