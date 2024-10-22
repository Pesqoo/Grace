namespace Grace.View;

public partial class RenameView : Form
{
    public string SearchInput => textBox_Filter.Text;

    public RenameView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
    }
}
