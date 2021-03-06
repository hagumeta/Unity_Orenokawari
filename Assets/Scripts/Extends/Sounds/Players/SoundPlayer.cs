﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Extends.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SoundPlayer : MonoBehaviour, ISoundPlayable<Sound>
    {
        public AudioSource AudioSource
            => this.audioSource;
        public virtual AudioMixerGroup DefaultAudioMixerGroup
            => SoundController.Instance.GetAudioMixerGroup("master");

        private AudioSource audioSource;

        private void Awake()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            if (this.audioSource == null)
            {
                this.audioSource = this.gameObject.AddComponent<AudioSource>();
            }
        }

        protected void SetAudioMixerGroup(AudioMixerGroup audioMixerGroup)
        {
            if (audioMixerGroup != null)
            {
                this.audioSource.outputAudioMixerGroup = audioMixerGroup;
            }
            else
            {
                this.audioSource.outputAudioMixerGroup = this.DefaultAudioMixerGroup;
            }
        }

        public void Play(Sound sound, float volume = 1, AudioMixerGroup audioMixerGroup = null)
        {
            this.SetAudioMixerGroup(audioMixerGroup);
            this.PlaySound(sound, volume);
        }

        protected abstract void PlaySound(Sound sound, float volume);
    }
}