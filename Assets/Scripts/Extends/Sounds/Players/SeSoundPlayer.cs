using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Extends.Sounds
{
    public class SeSoundPlayer : SoundPlayer
    {

        public override AudioMixerGroup DefaultAudioMixerGroup 
            => SoundController.Instance.GetAudioMixerGroup("SE");

        protected override void PlaySound(Sound sound, float volume)
        {
            this.AudioSource.PlayOneShot(sound.Audio, volume);
        }
    }
}
