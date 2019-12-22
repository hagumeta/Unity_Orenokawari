using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Title
{
    public class TitleMenuEvents : MonoBehaviour
    {
        [SerializeField] private SceneObject stageSelectScene, freeStageSelectScene;

        public void GotoStageSelectScene()
        {
            GameManager.MoveScene(this.stageSelectScene);
        }
        public void GotoFreeStageSelectScene()
        {
            GameManager.MoveScene(this.freeStageSelectScene);
        }
        public void GameEnd()
        {
            GameController.EndGame();
        }
    }
}