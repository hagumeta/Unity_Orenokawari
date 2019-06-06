using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// エディタ拡張（ウィンドウを作成する）
/// </summary>
public class MyChildhoodDreamEditor : EditorWindow
{
    [MenuItem("StageEditorTool/settingDefaultScene %t")]
    private static void Open()
    {
        EditorWindow.GetWindow<MyChildhoodDreamEditor>("ほげほげ");
    }

    SerializedProperty mergeScene;
    private void OnGUI()
    {
    }
}

public class MyChildhoodDreamEditor2 : EditorWindow
{
    [MenuItem("StageEditorTool/fugafuga %t")]
    private static void Open()
    {
        EditorWindow.GetWindow<MyChildhoodDreamEditor2>("ふがふが");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("マージ実行"))
        {
            SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }
    }
}


class SceneMergeManger
{
    public static SceneObject defaultScene;
}