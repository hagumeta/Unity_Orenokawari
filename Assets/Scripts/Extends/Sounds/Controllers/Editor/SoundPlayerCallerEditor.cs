using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using EditorExtends;


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
            var pastSound = this.SoundPlayerCaller.Sound;

            base.OnInspectorGUI();
            SoundEditor.ShowSoundData(this.SoundPlayerCaller.Sound);
        }
    }
}
