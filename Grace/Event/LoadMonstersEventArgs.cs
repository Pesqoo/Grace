namespace Grace.Event;

public class LoadMonstersEventArgs(bool reloadDatabase = false) : EventArgs
{
    public bool ReloadDatabase { get; set; } = reloadDatabase;
}
