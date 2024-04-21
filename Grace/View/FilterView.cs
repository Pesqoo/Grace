using Grace.Event;

namespace Grace.View;

public partial class FilterView : Form
{
    public string SearchInput => textBox_Filter.Text;

    public FilterView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
        InitializeEvent();

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
            case MonsterFilterType.DROP_ID:
                label_Filter.Text = "Drop ID";
                break;
        }

        return ShowDialog();
    }

    private void InitializeEvent()
    {
        btn_Filter.Click += (sender, e) => { DialogResult = DialogResult.OK; };
    }
}
