using Grace.View;

namespace Grace.Presenter;
public class RenameDropGroupPresenter
{
    private RenameView _renameView;

    public RenameDropGroupPresenter(RenameView renameView)
    {
        _renameView = renameView;
    }

    public string? OnShowView()
    {
        DialogResult dialogResult = _renameView.ShowDialog();

        if (dialogResult == DialogResult.OK)
        {
            return _renameView.SearchInput;
        }

        return null;
    }
}
