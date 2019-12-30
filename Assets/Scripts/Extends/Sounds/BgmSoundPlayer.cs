﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Sounds
{
    public class BgmSoundPlayer : SoundPlayer
    {
        public override void PlaySound(Sound sound, float volume)
        {
            this.audioSource.PlayOneShot(sound.Audio, volume);
        }
    }

}