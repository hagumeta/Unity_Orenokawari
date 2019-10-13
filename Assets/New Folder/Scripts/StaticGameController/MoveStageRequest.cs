using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data.Stage;
using Game.StageSelect;

namespace Game
{
    public class MoveStageRequest
    {
        public SceneObject ReturnToScene { get; private set; }
        public StageInformation StageInformation { get; private set; }

        public MoveStageRequest(StageInformation information, SceneObject ReturnToScene)
        {
            this.ReturnToScene = ReturnToScene;
            this.StageInformation = information;
        }
    }
}