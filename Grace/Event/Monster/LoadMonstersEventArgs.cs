namespace Grace.Event.Monster;

public class LoadMonstersEventArgs(bool reloadCache = false) : EventArgs
{
    public bool ReloadCache { get; set; } = reloadCache;
}
