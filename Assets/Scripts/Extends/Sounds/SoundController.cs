using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Extends.Sounds
{
    [ExecuteInEditMode]
    public class SoundController : SingletonMonoBehaviourFast<SoundController>
    {
        protected override void Init()
        {
            Instance.LoadData();
        }

        protected Dictionary<string, List<Sound>> list;
        public List<string> pathes;
        public AudioMixer audioMixer;

        protected void LoadData()
        {
            this.list = new Dictionary<string, List<Sound>>();
            foreach (var path in this.pathes)
            {
                this.list.Add(path, new List<Sound>(Resources.LoadAll<Sound>(path)));
            }
        }

        public AudioMixerGroup GetAudioMixerGroup(string key)
        {
            AudioMixerGroup[] groups = this.audioMixer.FindMatchingGroups(key);
            if (groups.Length < 1)
            {
                return null;
            }
            else
            {
                return groups[0];
            }
        }

        public List<Sound> GetSoundsList(string key)
        {
            if (this.list.ContainsKey(key))
            {
                return this.list[key];
            }
            else
            {
                return null;
            }
        }
    }
}
