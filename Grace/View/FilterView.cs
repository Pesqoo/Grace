using Grace.Event;

namespace Grace.View;

public partial class FilterView : Form
{
    public string SearchInput => textBox_Filter.Text;

    public FilterView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
    }

    public DialogResult Open(MonsterFilterType filterType)
    {
        switch (filterType)
        {
            case MonsterFilterType.ID:
                label_Filter.Text = "ID";
                break;
            case MonsterFilterType.LOCATION:
                label_Filter.Text = "Location";
                break;
            case MonsterFilterType.NAME:
                label_Filter.Text = "Name";
                break;
            case MonsterFilterType.DROP_TABLE_ID:
                label_Filter.Text = "DropTable ID";
                break;
            case MonsterFilterType.DROP_GROUP_ID:
                label_Filter.Text = "DropGroup ID";
                break;
            case MonsterFilterType.ITEM_ID:
                label_Filter.Text = "Item ID";
                break;
        }

        return ShowDialog();
    }
}
