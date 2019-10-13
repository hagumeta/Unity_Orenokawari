using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;


namespace Game
{
    [CreateAssetMenu(menuName = "Option/DeathIconCollection", fileName = "DeathIconCollection")]
    public class DeathIconCollection : ScriptableObject
    {
        [SerializeField]
        public HowDeath[] deaths;

        public HowDeath Get(DeathType deathType)
        {
            return this.deaths.Single(a => a.deathType == deathType);
        }
    }

    [System.Serializable]
    public class HowDeath
    {
        public DeathType deathType;
        public string name
        {
            get => Enum.GetName(typeof(DeathType), deathType);
        }
        public Sprite imageIcon;
    }


    /// <summary>
    /// ここに死亡種を書きこめ！！！
    /// </summary>
    public enum DeathType
    {
        StickingDeath,
        BurningDeath,
        FallingDeath
    }

    public static class DeathTypeExtend{
        public static string GetName(this DeathType deathType)
        {
            return Enum.GetName(typeof(DeathType), deathType);
        }
    }
}