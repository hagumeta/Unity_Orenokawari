using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extends.ScriptableDatas;

namespace Extends.Sounds
{
    public abstract class Sound : ScriptableData
    {
        public AudioClip Audio;
        public string Title;
        public string Author;
    }
}
