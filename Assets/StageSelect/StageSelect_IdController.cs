using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect_IdController : MonoBehaviour
{
    [System.Serializable]
    public class ReferenceOfIdToStageScene
    {
        public SceneObject scene;
    }

    [SerializeField] private ReferenceOfIdToStageScene[] Stages;
    private static StageSelect_IdController instance;

    private void Start()
    {
        instance = this;
    }

    public static void MoveStageScene(int stageId)
    {
        if (stageId >= 0 && instance.Stages.Length > stageId)
        {
            var re = instance.Stages[stageId];
            if (re != null)
            {
                SceneTransitionManager.GotoScene(re.scene, 0.3f, 0.5f);
            }
        }
    }
}