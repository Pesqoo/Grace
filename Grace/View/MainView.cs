using Grace.Event;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Grace;

public partial class MainView : Form
{
    public event EventHandler? LoadMonstersEventHandler;
    public event EventHandler<FilterMonstersEventArgs>? FilterMonstersEventHandler;
    public event EventHandler<DataGridViewCellEventArgs>? SelectMonsterEventHandler;
    public event EventHandler<TreeViewEventArgs>? SelectDropEventHandler;
    public event EventHandler? SaveDropEventHandler;

    public DataGridView MonsterDataGrid => monsterDataGrid;
    public TreeView DropTreeView { get { return dropTreeView; } set { dropTreeView = value; } }
    public TextBox[] DropNames => GetControlsByName<TextBox>(groupBox_DropGroupDetails, "DropName");
    public NumericUpDown[] DropMinCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupDetails, "DropMinCount");
    public NumericUpDown[] DropMaxCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupDetails, "DropMaxCount");
    public MaskedTextBox[] DropChances => GetControlsByName<MaskedTextBox>(groupBox_DropGroupDetails, "DropChance");
    public Label TotalDropPercentage => label_TotalPercentage;
    public TextBox DropGroupId => textBox_DropId;
    public ProgressBar ProgressBar => progressBar1;
    private MonsterFilterType _monsterFilterType = MonsterFilterType.ALL;
    private RadioButton[] FilterButtons => GetControlsByName<RadioButton>(groupBox_Filter, "radioButton");

    public MainView()
    {
        InitializeComponent();
        InitializeEvent();

        monsterDataGrid.AutoGenerateColumns = false;
    }

    private void InitializeEvent()
    {
        btn_ReloadData.Click += (sender, e) => { LoadMonstersEventHandler?.Invoke(sender, e); };
        btn_Filter.Click += (sender, e) => { FilterMonstersEventHandler?.Invoke(sender, new FilterMonstersEventArgs(GetFilterType())); };
        monsterDataGrid.CellClick += (sender, e) => { SelectMonsterEventHandler?.Invoke(sender, e); };
        dropTreeView.AfterSelect += (sender, e) => { SelectDropEventHandler?.Invoke(sender, e); };
        btn_Save.Click += (sender, e) => { SaveDropEventHandler?.Invoke(sender, e); };
    }

    private void HandleFilterMonsters(object sender, FilterMonstersEventArgs e)
    {

    }

    private static T[] GetControlsByName<T>(Control parentControl, string name) where T : Control
    {
        return parentControl.Controls
            .OfType<T>()
            .Where(t => t.Name.Contains(name))
            .Reverse()
            .ToArray();
    }

    public void ShowResult(string message, bool success = true)
    {
        label_Result.ForeColor = success ? Color.Green : Color.Red;
        label_Result.Text = message;

        Timer timer = new();
        timer.Interval = 2000;
        timer.Start();
        timer.Tick += (sender, e) =>
        {
            Timer? _t = sender as Timer;
            label_Result.Text = "";
            _t?.Stop();
        };
    }

    private void radioButton_FilterId_CheckedChanged(object sender, EventArgs e)
    {
        Debug.WriteLine("Checked Change ID");
    }

    private void btn_Filter_Click(object sender, EventArgs e)
    {

    }

    private MonsterFilterType GetFilterType()
    {
        int checkedIndex = -1;
        foreach (RadioButton radioButton in groupBox_Filter.Controls.OfType<RadioButton>())
        {
            if (radioButton.Checked)
            {
                checkedIndex = groupBox_Filter.Controls.IndexOf(radioButton);
                break;
            }
        }

        return (MonsterFilterType)checkedIndex;
    }
}
