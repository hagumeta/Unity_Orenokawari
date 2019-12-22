using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

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