namespace Grace.Event.DropGroup;

public class LoadDropGroupsEventArgs(bool reloadCache = false) : EventArgs
{
    public bool ReloadCache { get; set; } = reloadCache;
}
