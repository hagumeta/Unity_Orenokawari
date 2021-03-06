﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Corpse;
using Extends.CameraControlls;
using Game.Stage.Actor.Stickman;

namespace Game.Story
{
    public class PlayerCorpseForcuser : MonoBehaviour
    {

        public Corpse FindCorpse()
        {
            return GameObject.FindObjectOfType<Corpse>();
        }

        public void FocusOnCamera()
        {
            var camera = FindObjectOfType<CameraController>();
            if (camera != null)
            {
                camera.FocusOnObject(this.FindCorpse().transform, 1.5f, new Vector2(0, -0.2f));
            }
        }

        public void IgnoreNamusan()
        {
            this.FindCorpse().IgnoreNamusan = true;
        }


        public void CallNamusan()
        {
            var corpse = this.FindCorpse();
            corpse.IgnoreNamusan = false;
            corpse.Namusan();
        }

        public void LockPlayer()
        {
            var a = GameObject.FindObjectOfType<Stickman_PlayerOperationablePlatformActor>();
            a.Operation.Lock();
        }
    }
}