using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터를 관리하는 매니저
/// </summary>
public partial class Managers
{
    public class MonsterManager
    {
        internal MonsterManager() { }

        [field: SerializeField] public List<Monster> Monsters { get; } = new();

        public void Register(Monster monster)
        {
            Monsters.Add(monster);
        }

        public void Unregister(Monster monster)
        {
            Monsters.Remove(monster);
        }
    }
}