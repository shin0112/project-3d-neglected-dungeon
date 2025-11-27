public interface IStatView
{
    public void UpdateStaminaProgress(float stamina);
}

public interface IHeaderView
{
    public void UpdateLevelText(int level);
    public void UpdateLevelProgress(float progress);
    public void UpdateKeyText(int key);
    public void UpdateGoldText(int gold);
    public void UpdateGemText(int gem);
}