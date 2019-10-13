using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Mass : MonoBehaviour {

    public bool IsActive = true;
    public bool IsEnableLine = false;

    [SerializeField]
    public NextToMass next;
    
    private ConnectLineRenderer[] connectLines;

    void Start()
    {
        this.UpdateLinesConnection();
    }


    //上下左右に接続したマスの参照 をまとめた構造体
    //それぞれの値がnullの場合，接続無と判別
    [System.Serializable]
    public struct NextToMass
    {
        public Mass right, left, up, down;
    }
    

    private void OnDrawGizmosSelected()
    {
        this.drawWebGizmos(Color.red);
    }

    public void drawWebGizmos(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        if (this.next.right != null)
        {
            Gizmos.DrawLine(this.transform.position, this.next.right.transform.position);
        }
        if (this.next.left != null)
        {
            Gizmos.DrawLine(this.transform.position, this.next.left.transform.position);
        }
        if (this.next.up != null)
        {
            Gizmos.DrawLine(this.transform.position, this.next.up.transform.position);
        }
        if (this.next.down != null)
        {
            Gizmos.DrawLine(this.transform.position, this.next.down.transform.position);
        }
    }


    /// <summary>
    /// 上下左右に位置するMassと線でつなぐ
    /// </summary>
    public void UpdateLinesConnection()
    {
        this.connectLines = this.GetComponentsInChildren<ConnectLineRenderer>(true);

        foreach (ConnectLineRenderer line in this.connectLines)
        {
            line.gameObject.SetActive(true);
            line.points = new List<Transform>();
            line.points.Add(null);
            line.points.Add(null);
        }

        if (this.IsEnableLine) {
            if (this.next.right != null)
            {

                this.connectLines[0].points[0] = this.transform;
                this.connectLines[0].points[1] = this.next.right.transform;
            }
            else
            {
                this.connectLines[0].gameObject.SetActive(false);
            }

            if (this.next.left != null)
            {
                this.connectLines[1].points[0] = this.transform;
                this.connectLines[1].points[1] = this.next.left.transform;
            }
            else
            {
                this.connectLines[1].gameObject.SetActive(false);
            }

            if (this.next.up != null)
            {
                this.connectLines[2].points[0] = this.transform;
                this.connectLines[2].points[1] = this.next.up.transform;
            }
            else
            {
                this.connectLines[2].gameObject.SetActive(false);
            }

            if (this.next.down != null)
            {
                this.connectLines[3].points[0] = this.transform;
                this.connectLines[3].points[1] = this.next.down.transform;
            }
            else
            {
                this.connectLines[3].gameObject.SetActive(false);
            }
        }
    }


    public enum ConnectDirections
    {
        right, left, up, down
    }
    public void CreateMass(ConnectDirections direction, float distance, GameObject massGameObject) {
        this.CreateMassObject = massGameObject;
        this.CreateMass(direction, distance);
    }

    public GameObject CreateMassObject; //デフォルトの massGameObject
    public void CreateMass(ConnectDirections direction, float distance)
    {
        if (distance == 0) distance += 0.4f;
        var inst = Instantiate(this.CreateMassObject, this.transform.parent).GetComponent<Mass>();
        inst.transform.position = this.transform.position;
        inst.CreateMassObject = this.CreateMassObject;
        switch (direction)
        {
            case ConnectDirections.up:
                inst.transform.position += new Vector3(0, distance, 0);
                this.next.up = inst;
                inst.next.down = this;
                break;

            case ConnectDirections.left:
                inst.transform.position += new Vector3(-distance, 0, 0);
                this.next.left = inst;
                inst.next.right = this;
                break;

            case ConnectDirections.down:
                inst.transform.position += new Vector3(0, -distance, 0);
                this.next.down = inst;
                inst.next.up = this;
                break;


            case ConnectDirections.right:
                inst.transform.position += new Vector3(distance, 0, 0);
                this.next.right = inst;
                inst.next.left = this;
                break;
        }
        this.UpdateLinesConnection();
    }


    /// <summary>
    /// 設定した隣り合うMassオブジェクトに対して，自分に向くように設定する．
    /// </summary>
    public void ResolveMassBidirectionalConnection()
    {
#if UNITY_EDITOR
        if (this.next.up != null){ this.next.up.next.down = this; EditorUtility.SetDirty(this.next.up); }
        if (this.next.down != null) { this.next.down.next.up = this; EditorUtility.SetDirty(this.next.down); }
        if (this.next.right != null) { this.next.right.next.left = this; EditorUtility.SetDirty(this.next.right); }
        if (this.next.left != null) { this.next.left.next.right = this; EditorUtility.SetDirty(this.next.left); }
#endif

    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Mass))]
public class MassEditor : Editor
{
    public float CreateDistance;
    public Mass.ConnectDirections CreateDirection;


    SerializedProperty isActiveProp, isNeedLinesProp, nextUpProp, nextDownProp, nextRightProp, nextLeftProp;

    void OnEnable()
    {
        this.isNeedLinesProp = serializedObject.FindProperty("IsEnableLine");
        this.isActiveProp = serializedObject.FindProperty("IsActive");
        this.nextUpProp = serializedObject.FindProperty("next.up");
        this.nextDownProp = serializedObject.FindProperty("next.down");
        this.nextLeftProp = serializedObject.FindProperty("next.left");
        this.nextRightProp = serializedObject.FindProperty("next.right");
    }
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Mass mass = target as Mass;

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("このマスのパラメータ");
            }
            this.isNeedLinesProp.boolValue = EditorGUILayout.Toggle("Need Lines", this.isNeedLinesProp.boolValue);
            this.isActiveProp.boolValue = EditorGUILayout.Toggle("Is Active", this.isActiveProp.boolValue);

            using (new GUILayout.HorizontalScope(EditorStyles.label))
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(100)))
                {
                    EditorGUILayout.LabelField("お隣のマス", GUILayout.Width(60f));
                }
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUIUtility.labelWidth = 30;
                    
                    this.nextUpProp.objectReferenceValue = EditorGUILayout.ObjectField("上", this.nextUpProp.objectReferenceValue, typeof(Mass));
                    this.nextDownProp.objectReferenceValue = EditorGUILayout.ObjectField("下", this.nextDownProp.objectReferenceValue, typeof(Mass));
                    this.nextLeftProp.objectReferenceValue = EditorGUILayout.ObjectField("左", this.nextLeftProp.objectReferenceValue, typeof(Mass));
                    this.nextRightProp.objectReferenceValue = EditorGUILayout.ObjectField("右", this.nextRightProp.objectReferenceValue, typeof(Mass));

                    if (GUILayout.Button("相手側の接続も確立する"))
                    {
                        mass.ResolveMassBidirectionalConnection();
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    public void OnSceneGUI()
    {
        Mass mass = target as Mass;
        var sceneCamera = SceneView.currentDrawingSceneView.camera;
        var pos = sceneCamera.WorldToScreenPoint(mass.transform.position);


        var countButtonRect = new Rect(
        pos.x - 3f,
        SceneView.currentDrawingSceneView.position.height - pos.y - 3f,
        300f,
        120f);

        Mass nextMass;






        Handles.BeginGUI();

        GUILayout.BeginArea(countButtonRect);
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("マスの追加生成");
            }

            mass.CreateMassObject = (GameObject)EditorGUILayout.ObjectField("生成するマスオブジェクト", mass.CreateMassObject, typeof(GameObject)) as GameObject;
            this.CreateDistance = EditorGUILayout.FloatField("生成するマスの間隔", this.CreateDistance);
            this.CreateDirection = (Mass.ConnectDirections)EditorGUILayout.EnumPopup("生成するマスの方向", this.CreateDirection);


            switch (this.CreateDirection)
            {
                case Mass.ConnectDirections.up:
                    nextMass = mass.next.up;
                    break;

                case Mass.ConnectDirections.down:
                    nextMass = mass.next.down;
                    break;

                case Mass.ConnectDirections.left:
                    nextMass = mass.next.left;
                    break;

                case Mass.ConnectDirections.right:
                    nextMass = mass.next.right;
                    break;

                default:
                    nextMass = null;
                    break;
            }

            if (nextMass != null)
            {
                EditorGUI.BeginDisabledGroup(true);
            }

            if (GUILayout.Button("マス生成"))
            {
                mass.CreateMass(this.CreateDirection, this.CreateDistance);
            }
            EditorGUI.EndDisabledGroup();
        }
        GUILayout.EndArea();

        this.DrawNextMassIcon(sceneCamera, mass.next.up, "上");
        this.DrawNextMassIcon(sceneCamera, mass.next.left, "左");
        this.DrawNextMassIcon(sceneCamera, mass.next.right, "右");
        this.DrawNextMassIcon(sceneCamera, mass.next.down, "下");



        Handles.EndGUI();
    }


    private void DrawNextMassIcon(Camera sceneCamera, Mass nextMass, string text)
    {
        if (nextMass != null)
        {
            var nextPos = sceneCamera.WorldToScreenPoint(nextMass.transform.position);
            Rect rect = new Rect(nextPos.x, SceneView.currentDrawingSceneView.position.height - nextPos.y, 17, 17);

            GUI.TextArea(rect, text);
        }
    }
}
#endif