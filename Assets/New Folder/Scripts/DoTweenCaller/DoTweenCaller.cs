using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace Extends.DoTweenSupporter
{
    public class DoTweenCaller : MonoBehaviour
    {
        public Vector2 MoveTo;
        public float MoveTime;
        public Ease EaseType;
        public bool LocalMove;

        void OnEnable()
        {
            if (this.LocalMove)
            {
                this.transform.DOLocalMove(this.MoveTo, this.MoveTime).SetEase(this.EaseType);
            }
            else
            {
                this.transform.DOMove(this.MoveTo, this.MoveTime).SetEase(this.EaseType);
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(DoTweenCaller))]
    public class DoTweenCallerEditor : Editor
    {

        private SerializedProperty _moveTo, _moveTime, _easeType, _isLocal;
        private void OnEnable()
        {
            this._moveTo = this.serializedObject.FindProperty("MoveTo");
            this._easeType = this.serializedObject.FindProperty("EaseType");
            this._moveTime = this.serializedObject.FindProperty("MoveTime");
            this._isLocal = this.serializedObject.FindProperty("LocalMove");
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.HelpBox("起動時に下記設定の通り移動をします", MessageType.Info);


            EditorGUILayout.PropertyField(this._moveTo, new GUIContent("移動先の座標"));
            EditorGUILayout.PropertyField(this._isLocal, new GUIContent("ローカル座標系？"));
            EditorGUILayout.PropertyField(this._moveTime, new GUIContent("移動に要する時間"));
            EditorGUILayout.PropertyField(this._easeType, new GUIContent("EaseType"));

            this.serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}