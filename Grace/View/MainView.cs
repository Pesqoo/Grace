namespace Grace.View;
public partial class MainView : Form
{
    public TabPage MonstersTab { get { return tabPage_Monsters; } }
    public TabPage DropGroupsTab { get { return tabPage_DropGroups; } }

    public MainView()
    {
        InitializeComponent();
        this.Show();
    }
}
