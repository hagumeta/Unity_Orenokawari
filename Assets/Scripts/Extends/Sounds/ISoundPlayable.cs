using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Sounds
{
    public interface ISoundPlayable
    {
        void PlaySound(Sound sound, float volume);
    }
}
