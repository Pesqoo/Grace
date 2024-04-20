using Grace.Event;
using Timer = System.Windows.Forms.Timer;

namespace Grace;

public partial class MainView : Form
{
    public event EventHandler<LoadMonstersEventArgs>? LoadMonstersEventHandler;
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

    public MainView()
    {
        InitializeComponent();
        InitializeEvent();

        monsterDataGrid.AutoGenerateColumns = false;
    }

    private void InitializeEvent()
    {
        btn_ReloadData.Click += (sender, e) => { LoadMonstersEventHandler?.Invoke(sender, new LoadMonstersEventArgs(true)); };
        btn_Filter.Click += (sender, e) => { FilterMonstersEventHandler?.Invoke(sender, new FilterMonstersEventArgs(GetFilterType())); };
        monsterDataGrid.CellClick += (sender, e) => { SelectMonsterEventHandler?.Invoke(sender, e); };
        dropTreeView.AfterSelect += (sender, e) => { SelectDropEventHandler?.Invoke(sender, e); };
        btn_Save.Click += (sender, e) => { SaveDropEventHandler?.Invoke(sender, e); };
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
