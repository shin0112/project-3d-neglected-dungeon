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
    public DungeonManager Dungeon { get; } = new();
    public ObjectPoolManager ObjectPool { get; } = new();
    public InventoryManager Inventory { get; } = new();

    // Player
    [field: SerializeField] public Player Player { get; private set; }

    // Dungeon Map
    [SerializeField] private CorridorSetData[] Corridors;

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

        // Managers
        this.Dungeon.Initialize(Corridors);
    }

    private void Start()
    {
        EquipmentController equipment = FindObjectOfType<EquipmentController>();
        this.Inventory.Initialize(Player.Condition, equipment);

        Test();
    }
    #endregion

    #region Test
    [field: SerializeField] public DungeonData DungeonData { get; private set; }

    private void Test()
    {
        if (DungeonData != null)
        {
            Dungeon.StartDungeon(DungeonData);
        }
    }
    #endregion
}
