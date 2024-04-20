namespace Grace.Event;

public class FilterMonstersEventArgs(MonsterFilterType filterType) : EventArgs
{
    public MonsterFilterType FilterType { get; set; } = filterType;
}

public enum MonsterFilterType
{
    DROP_ID,
    LOCATION,
    NAME,
    ID,
    ALL
}
