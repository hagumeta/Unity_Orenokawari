using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace TestScripts
{
    public class Test_GameSave : MonoBehaviour
    {
        public void Save()
        {
            GameController.SaveGame();
        }
    }
}