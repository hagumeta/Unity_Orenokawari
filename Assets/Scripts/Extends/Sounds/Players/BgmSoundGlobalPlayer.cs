using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Sounds
{
    [RequireComponent(typeof(BgmSoundPlayer))]
    public class BgmSoundGlobalPlayer : SingletonMonoBehaviourFast<BgmSoundGlobalPlayer>
    {
        public BgmSoundPlayer BgmSoundPlayer { get; private set; }

        protected override void Init()
        {
            this.BgmSoundPlayer = this.GetComponent<BgmSoundPlayer>();
        }

        public static void Play(BgmSound sound, float volume=1)
        {
            Instance.BgmSoundPlayer.Play(sound, volume);
        }

        public static void Stop()
        {
            Instance.BgmSoundPlayer.Stop();
        }

        public static void IsPlaying(BgmSound sound)
        {
            Instance.BgmSoundPlayer.IsPlaying(sound);
        }

    }
}