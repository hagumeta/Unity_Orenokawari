using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MassMapCreator : MonoBehaviour{


    public Vector2Int MassMap;
    public float MassDist;
    public GameObject MassObject;
    protected Mass[,] Masses;

    public virtual void MapCreate()
    {
        if (this.MassObject == null || this.MassMap.x == 0 || this.MassMap.y == 0)
        {
            return;
        }
        if (MassObject.GetComponent<Mass>() == null)
        {
            Debug.LogError("マップ生成する為のマスにはMassコンポーネントをつけておく必要があります！！");
            return;
        }


        this.Masses = new Mass[MassMap.x, MassMap.y];


        for (int i=0; i<MassMap.x; i++)
        {
            for (int j=0; j<MassMap.y; j++)
            {
                Vector3 dist = new Vector3(i - (this.MassMap.x-1) / 2f, j - (this.MassMap.y-1) / 2f, 0) * this.MassDist;
                var obj = Instantiate(this.MassObject, this.transform);
                obj.transform.position = this.transform.position + dist;

                this.Masses[i, j] = obj.GetComponent<Mass>();
            }
        }

        for (int i=0; i<this.MassMap.x; i++)
        {
            for (int j=0; j<this.MassMap.y; j++)
            {
                var up = j - 1;
                var down = j + 1;
                var left = i - 1;
                var right = j + 1;

                if (i != 0) { Masses[i, j].next.left = Masses[i - 1, j]; }
                if (j+1 != MassMap.y) { Masses[i, j].next.up = Masses[i, j + 1]; }
                if (j != 0) { Masses[i, j].next.down = Masses[i, j - 1]; }
                if (i+1 != MassMap.x) { Masses[i, j].next.right = Masses[i + 1, j]; }
            }
        }
    }

    public void MapDestroy()
    {
        foreach (var mass in this.GetComponentsInChildren<Mass>())
        {
            DestroyImmediate(mass.gameObject);
        }
    }


    public void ResolveConnections()
    {
        foreach (var mass in this.GetComponentsInChildren<Mass>()) {
            mass.ResolveMassBidirectionalConnection();
        }
    }


    private void OnDrawGizmosSelected()
    {
        foreach (var mass in this.GetComponentsInChildren<Mass>())
        {
            mass.drawWebGizmos(Color.cyan);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MassMapCreator))]
public class CustomMapEditor : Editor
{



    public override void OnInspectorGUI()
    {
        Color def = GUI.backgroundColor;
        MassMapCreator map = target as MassMapCreator;

        GUI.backgroundColor = Color.grey;
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new GUILayout.VerticalScope(EditorStyles.toolbar))
            {
                GUI.backgroundColor = Color.white;
                GUILayout.Label("マップステータス");
            }

            map.MassMap = EditorGUILayout.Vector2IntField("生成するマスの数", map.MassMap);
            map.MassDist = EditorGUILayout.FloatField("マス間の距離", map.MassDist);
            map.MassObject = EditorGUILayout.ObjectField("マスのオブジェクト", map.MassObject, typeof(GameObject)) as GameObject;
        }


        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("上記のマップを生成"))
            {
                map.MapCreate();
            }

            GUILayout.Space(10);


            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("マス全削除"))
            {
                map.MapDestroy();
            }
        }



        GUI.backgroundColor = def;

        if (GUILayout.Button("マップマス間の接続を最適化"))
        {
            map.ResolveConnections();
        }
    }
}
#endif