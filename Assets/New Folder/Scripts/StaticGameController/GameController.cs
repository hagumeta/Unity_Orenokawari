using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InputUtil;
using Data.Stage;

namespace Game
{
    public class GameSceneController
    {
        public SceneObject
            TitleScene,
            InitializeScene;

        public void GoToScene(Scene scene)
        {
            SceneTransitionManager.GotoScene(scene.name, 1, 0.5f);
        }
    }


    public class GameController : MonoBehaviour
    {
        public static void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
        }

        public void ResetGame()
        {

        }

        protected void InitGame()
        {
            new ButtonOperation();
            
            
        }

        public static void SaveGame()
        {
            GameManager.StageSaveData.Save();
        }

        public static void LoadGame()
        {
            GameManager.StageSaveData.Load();
        }



        private void Start()
        {
            this.InitGame();
        }
    }






    public class GameInitializer
    {
        private const string InitializeSceneName = "InitializeScene";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RuntimeInitializeApplication()
        {

            if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
            {
                SceneManager.LoadScene(InitializeSceneName, LoadSceneMode.Additive);
            }
        }
    }
}