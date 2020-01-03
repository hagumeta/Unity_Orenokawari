﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Extends.Sounds
{
    public class BgmSoundPlayer : SoundPlayer, ISoundStoppable
    {
        public Sound PlayingBgmSound { private set; get; }
        public new static AudioMixerGroup DefaultAudioMixerGroup;

        public bool IsPlaying(Sound bgmSound = null)
        {
            if (bgmSound == null)
            {
                return (this.AudioSource.isPlaying);
            }
            else
            {
                return (this.AudioSource.isPlaying && bgmSound == this.PlayingBgmSound);
            }
        }

        protected void SetBgmSound(Sound sound, float volume)
        {
            if (!this.IsPlaying(sound))
            {
                this.PlayingBgmSound = sound;
                this.AudioSource.clip = sound.Audio;
                this.AudioSource.volume = volume;
            }
        }

        protected override void PlaySound(Sound sound, float volume)
        {
            if (this.IsPlaying(sound))
            {
                return;
            }
            this.Stop();
            this.SetBgmSound(sound, volume);
            this.AudioSource.Play();
        }

        public void Stop()
        {
            if (this.IsPlaying(null))
            {
                this.AudioSource.Stop();
                this.PlayingBgmSound = null;
            }
        }
    }
}
