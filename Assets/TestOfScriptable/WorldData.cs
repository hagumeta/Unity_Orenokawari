using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "StageDatas/Create WorldData", fileName = "ParameterTable")]
public class WorldData : ScriptableObject
{
    public StageData[] StageDatas;
    public string WorldName;
}