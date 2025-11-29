/// <summary>
/// 현재 던전 정보(던전 기본 정보, 진행 상황 등)를 연결하는 Presenter
/// </summary>
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

    public void OnClickBattleBossButton()
    {
        // 보스 소환
        Managers.Instance.Dungeon.Spawner.SpawnBoss();
    }
}
