using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Extends.Sounds
{
    public static class SoundPlayableExtended
    {
        public static void Play(this Sound sound, GameObject gameObject, float volume = 1, AudioMixerGroup audioMixerGroup = null)
        {
            var soundPlayer = AddSoundPlayer(gameObject);
            soundPlayer.Play(sound, volume, audioMixerGroup);
        }

        public static void PlayGlobal(this Sound sound, float volume = 1, AudioMixerGroup audioMixerGroup = null)
        {
            var soundPlayer = AddSoundPlayer(SoundController.Instance.gameObject);
            soundPlayer.Play(sound, volume, audioMixerGroup);
        }

        public static SoundPlayer AddSoundPlayer(GameObject gameObject)
        {
            var soundPlayer = gameObject.GetComponent<SoundPlayer>();
            if (soundPlayer == null)
            {
                soundPlayer = gameObject.AddComponent<SoundPlayer>();
            }
            return soundPlayer;
        }
    }
}
