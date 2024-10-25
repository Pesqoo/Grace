namespace Grace.Event;

public class AddSubIdEventArgs(int dropTableId) : EventArgs
{
    public int DropTableId { get; } = dropTableId;
}
