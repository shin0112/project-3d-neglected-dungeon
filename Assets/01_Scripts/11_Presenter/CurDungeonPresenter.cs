public class CurDungeonPresenter
{
    ICurDungeonView _view;

    public CurDungeonPresenter(ICurDungeonView view)
    {
        _view = view;

        Managers.Instance.Dungeon.Spawner.OnCurDungeonProgress += OnProgressChanged;
        Managers.Instance.Dungeon.OnDungeonNameFixed += OnCurDungeonNameChanged;
    }

    public void OnProgressChanged(float progress)
    {
        _view.UpdateProgress(progress);
    }

    public void OnCurDungeonNameChanged(string name)
    {
        _view.UpdateCurDungeonText(name);
    }
}
