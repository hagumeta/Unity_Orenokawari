using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Sounds
{
    public static class BgmSoundPlayableExtended
    {
        public static void PlayGlobal(this BgmSound sound, float volume = 1)
        {
            BgmSoundGlobalPlayer.Play(sound, volume);
        }

        public static void Play(this BgmSound sound, GameObject gameObject, float volume = 1)
        {
            var soundPlayer = AddSoundPlayer(gameObject);
            soundPlayer.Play(sound, volume);
        }

        public static BgmSoundPlayer AddSoundPlayer(GameObject gameObject)
        {
            var soundPlayer = gameObject.GetComponent<BgmSoundPlayer>();
            if (soundPlayer == null)
            {
                soundPlayer = gameObject.AddComponent<BgmSoundPlayer>();
            }
            return soundPlayer;
        }
    }
}