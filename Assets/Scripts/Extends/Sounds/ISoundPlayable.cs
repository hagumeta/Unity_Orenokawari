using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Extends.Sounds
{
    public interface ISoundPlayable<T> where T : Sound
    {
        void Play(T sound, float volume, AudioMixerGroup audioMixerGroup);
    }
}
