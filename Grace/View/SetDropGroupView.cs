using Grace.Event.SetDropGroup;

namespace Grace.View;

public partial class SetDropGroupView : Form
{
    public EventHandler<FilterDropGroupsEventArgs>? FilterDropGroupsEventHandler;
    public EventHandler? ResetDropGroupsEventHandler;
    public DataGridView DropGroupDataGrid => dropGroupDataGrid;
    public SetDropGroupView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
        InitializeEvent();
    }

    private void InitializeEvent()
    {
        textBox_Filter.KeyPress += CheckEnter;
        btn_Filter.Click += (sender, e) => FilterDropGroupsEventHandler?.Invoke(
            sender,
            new FilterDropGroupsEventArgs(radioButton_Id.Checked ? DropGroupFilterType.ID : DropGroupFilterType.NAME, textBox_Filter.Text)
        );
        btn_Reset.Click += (sender, e) => ResetDropGroupsEventHandler?.Invoke(sender, e);
    }

    private void CheckEnter(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Return)
        {
            FilterDropGroupsEventHandler?.Invoke(
                sender,
                new FilterDropGroupsEventArgs(radioButton_Id.Checked ? DropGroupFilterType.ID : DropGroupFilterType.NAME, textBox_Filter.Text)
            );
            e.Handled = true;
        }
    }
}
