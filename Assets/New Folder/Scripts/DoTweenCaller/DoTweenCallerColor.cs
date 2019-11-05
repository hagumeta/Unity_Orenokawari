using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Extends.DoTweenSupporter
{

    [RequireComponent(typeof(Image))]
    public class DoTweenCallerColor : MonoBehaviour
    {
        public Color ColorTo;
        public float Time;
        public Ease EaseType;
        public Image sprite
            => this.GetComponent<Image>();

        void OnEnable()
        {
            this.sprite.DOColor(this.ColorTo, this.Time).SetEase(this.EaseType);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(DoTweenCallerColor))]
    public class DoTweenCallerColorEditor : Editor
    {

        private SerializedProperty _colorTo, _Time, _easeType;
        private void OnEnable()
        {
            this._colorTo = this.serializedObject.FindProperty("ColorTo");
            this._easeType = this.serializedObject.FindProperty("EaseType");
            this._Time = this.serializedObject.FindProperty("Time");
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.HelpBox("起動時に下記設定の通りImageの色を変えます", MessageType.Info);

            EditorGUILayout.PropertyField(this._colorTo, new GUIContent("変更後の色"));
            EditorGUILayout.PropertyField(this._Time, new GUIContent("変更に要する時間"));
            EditorGUILayout.PropertyField(this._easeType, new GUIContent("EaseType"));

            this.serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}