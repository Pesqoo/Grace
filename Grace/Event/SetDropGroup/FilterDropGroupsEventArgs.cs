namespace Grace.Event.SetDropGroup;

public class FilterDropGroupsEventArgs : EventArgs
{
    public DropGroupFilterType FilterType { get; set; }
    public string Value { get; set; }

    public FilterDropGroupsEventArgs(DropGroupFilterType filterType, string value)
    {
        FilterType = filterType;
        Value = value;
    }
}

public enum DropGroupFilterType
{
    ID,
    NAME
}

