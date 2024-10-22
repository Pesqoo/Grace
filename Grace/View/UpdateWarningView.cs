using Grace.Model;

namespace Grace.View;
public partial class UpdateWarningView : Form
{
    public void SetDataSource(List<Monster> monsters)
    {
        monsterDataGrid.DataSource = monsters;
        warningLabel.Text = $"Updating this DropGroup will affect the DropTable of {monsters.Count} Monsters.";
    }

    public UpdateWarningView()
    {
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponent();
        warningIcon.Image = SystemIcons.Warning.ToBitmap();
    }
}
