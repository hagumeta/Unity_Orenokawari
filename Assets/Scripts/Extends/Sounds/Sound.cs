using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Sounds
{
    public abstract class Sound : ScriptableObject
    {
        public int Index;
        public AudioClip Audio;
        public string Title;
        public string Author;        
    }
}
