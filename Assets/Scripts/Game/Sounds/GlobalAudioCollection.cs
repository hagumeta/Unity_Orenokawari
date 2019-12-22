using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Sounds;


namespace Game.Sounds
{
    public class GlobalAudioCollection : SingletonMonoBehaviourFast<GlobalAudioCollection>
    {
        [SerializeField]private SeSettings SeStting;

        protected override void Init(){}

        public static SeSettings SESetting => Instance.SeStting;
    }
}