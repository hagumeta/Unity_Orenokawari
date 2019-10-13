using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage
{
    public interface IPlayer
    {
        void Death(DeathType deathType);
        void OnReborn();
        void CreateCorpse(DeathType deathType);
        PlayerType playerType { get; }
    }
}