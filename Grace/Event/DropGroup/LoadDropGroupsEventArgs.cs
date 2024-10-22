namespace Grace.Event.DropGroup;

public class LoadDropGroupsEventArgs(bool reloadData = false) : EventArgs
{
    public bool ReloadData { get; set; } = reloadData;
}
