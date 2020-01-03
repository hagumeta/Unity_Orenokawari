using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Sounds
{
    public class SeSoundPlayer : SoundPlayer
    {
        protected override void PlaySound(Sound sound, float volume)
        {
            this.AudioSource.PlayOneShot(sound.Audio, volume);
        }
    }
}
