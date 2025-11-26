using UnityEngine;

/// <summary>
/// 모든 매니저를 관리하는 클래스
/// partial 클래스로 묶어서 Managers에서만 초기화 가능하도록 설정
/// </summary>
public partial class Managers : MonoBehaviour
{
    // Singleton
    private static Managers _instance;
    public static Managers Instance => _instance;

    // Managers
    public MonsterManager Monster { get; } = new();

    // Player
    [field: SerializeField] public Player Player { get; private set; }

    #region 초기화
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        Player = FindObjectOfType<Player>();

        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (var m in monsters)
        {
            Monster.Register(m);
        }
    }
    #endregion
}
