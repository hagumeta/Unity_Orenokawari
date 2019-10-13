using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Sounds
{
    [CreateAssetMenu(menuName = "Option/SESoundsSetting", fileName = "ParameterTable")]
    public class SeSettings : ScriptableObject
    {
        [SerializeField] private AudioClip menuEnterSound;
        [SerializeField] private AudioClip menuSelectSound;
        [SerializeField] private AudioClip menuBackSound;

        public AudioClip MenuEnterSound => this.menuEnterSound;
        public AudioClip MenuSelectSound => this.menuSelectSound;
        public AudioClip MenuBackSound => this.menuBackSound;
    }
}