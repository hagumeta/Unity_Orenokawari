﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Extends.Sounds
{
    public class SoundPlayerCaller : MonoBehaviour
    {
        public Sound Sound;
        public AudioMixerGroup AudioMixerGroup;
        [Range(0, 1)] public float Volume;

        void Start()
        {
            this.Sound.Play(this.gameObject, this.Volume, this.AudioMixerGroup);
        }
    }
}