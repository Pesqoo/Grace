namespace Grace.Event.ContextMenu;

public class TreeContextMenuEventArgs(TreeNode selectedNode) : EventArgs
{
    public TreeNode SelectedNode { get; } = selectedNode;
}
