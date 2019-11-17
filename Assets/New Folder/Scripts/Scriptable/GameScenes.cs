using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Static
{
    [CreateAssetMenu(menuName = "Option/GameScenes", fileName = "ParameterTable")]
    public class GameScenes : ScriptableObject
    {
        [SerializeField] private SceneObject titleScene;
        [SerializeField] private SceneObject stageSelectScene;
        [SerializeField] private SceneObject freeStageSelectScene;


        public SceneObject TitleScene => this.titleScene;
        public SceneObject StageSelectScene => this.stageSelectScene;
        public SceneObject FreeStageSelectScene => this.freeStageSelectScene;

    }
}