namespace Grace.Event;

public class FilterMonstersEventArgs(MonsterFilterType filterType) : EventArgs
{
    public MonsterFilterType FilterType { get; set; } = filterType;
}

public enum MonsterFilterType
{
    ITEM_ID,
    DROP_GROUP_ID,
    DROP_TABLE_ID,
    LOCATION,
    NAME,
    ID,
    ALL
}
