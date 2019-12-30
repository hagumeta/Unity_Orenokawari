using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SoundPlayer : MonoBehaviour, ISoundPlayable
    {
        protected AudioSource audioSource;
        protected float soundVolume;

        public void Play(Sound sound)
        {
            this.PlaySound(sound, this.soundVolume);
        }

        public abstract void PlaySound(Sound sound, float volume);
    }
}