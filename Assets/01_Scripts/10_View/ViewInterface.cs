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

public interface IProfileView
{
    public void UpdateNameText(string name);
    public void UpdateAttackText(int attack);
    public void UpdateDefenseText(int defense);
    public void UpdateTotalHealthText(int health);
}