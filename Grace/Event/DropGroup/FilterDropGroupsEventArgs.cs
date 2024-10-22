namespace Grace.Event.DropGroup;

public class FilterDropGroupsEventArgs(DropGroupFilterType filterType) : EventArgs
{
    public DropGroupFilterType FilterType { get; set; } = filterType;
}

public enum DropGroupFilterType
{
    ITEM_ID,
    DROP_GROUP_ID,
    NAME,
    ID,
    ALL
}
