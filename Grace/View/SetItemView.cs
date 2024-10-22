using Grace.Event;

namespace Grace.View;

public partial class SetItemView : Form
{
    public EventHandler<FilterItemsEventArgs>? FilterItemsEventHandler;
    public EventHandler? ResetItemsEventHandler;
    public DataGridView ItemDataGrid => itemDataGrid;
    public SetItemView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
        InitializeEvent();
    }

    private void InitializeEvent()
    {
        textBox_Filter.KeyPress += CheckEnter;
        btn_Filter.Click += (sender, e) => FilterItemsEventHandler?.Invoke(
            sender,
            new FilterItemsEventArgs(radioButton_Id.Checked ? ItemFilterType.ID : ItemFilterType.NAME, textBox_Filter.Text)
        );
        btn_Reset.Click += (sender, e) => ResetItemsEventHandler?.Invoke(sender, e);
    }

    private void CheckEnter(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Return)
        {
            FilterItemsEventHandler?.Invoke(
                sender,
                new FilterItemsEventArgs(radioButton_Id.Checked ? ItemFilterType.ID : ItemFilterType.NAME, textBox_Filter.Text)
            );
            e.Handled = true;
        }
    }
}
