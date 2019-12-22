using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;

namespace Game.Story
{
    public class StorySceneMoveController : MonoBehaviour
    {
        [SerializeField] private SceneObject nextScene;

        public void SceneMove()
        {
            GameManager.MoveScene(this.nextScene);
        }

    }
}