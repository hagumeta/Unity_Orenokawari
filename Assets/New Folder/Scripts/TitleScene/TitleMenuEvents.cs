using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Title
{
    public class TitleMenuEvents : MonoBehaviour
    {
        public void GotoStageSelectScene()
        {
            GameManager.MoveStageSelectScene();
        }
        public void GotoFreeStageSelectScene()
        {
            GameManager.MoveFreeStageSelectScene();
        }
        public void GameEnd()
        {
            GameController.EndGame();
        }
    }
}