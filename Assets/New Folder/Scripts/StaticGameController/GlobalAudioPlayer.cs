using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalAudioPlayer : SingletonMonoBehaviourFast<GlobalAudioPlayer>
    {
        private AudioSource audioSource;
        protected override void Init()
        {
            this.audioSource = this.GetComponent<AudioSource>();
        }

        private void playAudio(AudioClip audioClip)
        {
            this.audioSource.PlayOneShot(audioClip);
        }

        public static void PlayAudio(AudioClip audioClip)
        {
            Instance.playAudio(audioClip);
        }
    }
}