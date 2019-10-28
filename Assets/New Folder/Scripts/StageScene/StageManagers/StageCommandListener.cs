using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputUtil;
using Game.Stage.Manager;


namespace Game.Stage
{
    public class StageCommandListener : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StageManager.StageReset();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StageManager.ReturnToSelectScene();
            }
        }
    }
}