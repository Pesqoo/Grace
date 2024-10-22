using Grace.Event.DropGroup;

namespace Grace.View;

public partial class FilterDropGroupsView : Form
{
    public string SearchInput => textBox_Filter.Text;

    public FilterDropGroupsView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
    }

    public DialogResult Open(DropGroupFilterType filterType)
    {
        switch (filterType)
        {
            case DropGroupFilterType.ID:
                label_Filter.Text = "ID";
                break;
            case DropGroupFilterType.NAME:
                label_Filter.Text = "Name";
                break;
            case DropGroupFilterType.DROP_GROUP_ID:
                label_Filter.Text = "DropGroup ID";
                break;
            case DropGroupFilterType.ITEM_ID:
                label_Filter.Text = "Item ID";
                break;
        }

        return ShowDialog();
    }
}
