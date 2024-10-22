namespace Grace.Event;

public class LoadMonstersEventArgs(bool reloadData = false) : EventArgs
{
    public bool ReloadData { get; set; } = reloadData;
}
