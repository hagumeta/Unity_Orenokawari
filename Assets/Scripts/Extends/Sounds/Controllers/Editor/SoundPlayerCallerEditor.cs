using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Extends.Sounds
{
    [CustomEditor(typeof(SoundPlayerCaller))]
    public class SoundPlayerCallerEditor : Editor
    {
        protected SoundPlayerCaller SoundPlayerCaller;
        private void OnEnable()
        {
            this.SoundPlayerCaller = target as SoundPlayerCaller;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            string defaultMixerGroup = "未指定";
            try
            {
                if (this.SoundPlayerCaller.Sound is BgmSound)
                {
                    defaultMixerGroup = BgmSoundPlayer.DefaultAudioMixerGroup.name;
                }
                else if (this.SoundPlayerCaller.Sound is SeSound)
                {
                    defaultMixerGroup = SeSoundPlayer.DefaultAudioMixerGroup.name;
                }
            }catch (NullReferenceException){}
             
            EditorGUILayout.LabelField("DefaultAudioMixerGroup:", defaultMixerGroup);


            SoundEditor.ShowSoundData(this.SoundPlayerCaller.Sound);
        }
    }
}
