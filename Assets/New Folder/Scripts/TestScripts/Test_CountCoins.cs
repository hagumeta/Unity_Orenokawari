using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Gimicks;
using UnityEditor;
using System.Linq;

public class Test_CountCoins : MonoBehaviour
{
    public void CountCoin()
    {
        var count = GameObject.FindObjectsOfType<Coin>().Except<Coin>(GameObject.FindObjectsOfType<MasterCoin>()).ToArray().Length;
        Debug.Log(count);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Test_CountCoins))]
public class Test_CountCoinsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        var obj = target as Test_CountCoins;

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("ステージ上のコインをカウント"))
            {
                obj.CountCoin();
            }
        }
    }
}
#endif