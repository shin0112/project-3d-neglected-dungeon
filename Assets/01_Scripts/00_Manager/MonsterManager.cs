using System.Collections.Generic;
using UnityEngine;

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