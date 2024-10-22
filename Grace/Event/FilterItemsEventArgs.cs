namespace Grace.Event;

public class FilterItemsEventArgs : EventArgs
{
    public ItemFilterType FilterType { get; set; }
    public string Value { get; set; }

    public FilterItemsEventArgs(ItemFilterType filterType, string value)
    {
        FilterType = filterType;
        Value = value;
    }
}

public enum ItemFilterType
{
    ID,
    NAME
}

