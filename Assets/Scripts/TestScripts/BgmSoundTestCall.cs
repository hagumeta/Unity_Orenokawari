using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extends.Sounds;

namespace TestScripts
{
    public class BgmSoundTestCall : MonoBehaviour
    {
        public BgmSound bgmSound;
        void Start()
        {
            this.bgmSound.PlayGlobal();
        }
    }
}