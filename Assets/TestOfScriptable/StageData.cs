using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "StageDatas/Create StageData", fileName = "ParameterTable")]
public class StageData : ScriptableObject
{
    public SceneObject StageScene;
    public string StageName;
}