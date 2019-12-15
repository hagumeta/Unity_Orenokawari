using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.GameEvents;
using Game.Stage.Displayer;
using InputUtil;


namespace Game.Stage.Manager
{
    [RequireComponent(typeof(StageManager))]
    public class PlayerStageClearManager : MonoBehaviour, IManager, IStageClearEventListener
    {
        public StageManager stageManager
            => StageManager.Instance;
        public bool IsCleared { get; private set; }
        public bool Locked { get; private set; }

        public void Init(int stageID)
        {
            this.IsCleared = false;
            this.Locked = true;
        }

        public virtual void StageClear(IPlayer player = null)
        {
            if (this.IsCleared) return;
            this.IsCleared = true;
            if (player != null)
            {
                player.DoStageClearedAction();
            }

            StageClearDisplayer.Create(this.Unlock);
            this.stageManager.Store();
        }

        protected void Unlock()
        {
            this.Locked = false;
        }

        private void Update()
        {
            if (!this.Locked)
            {
                if (ButtonOperation.Submit.IsPressed)
                {
                    StageManager.ReturnToSelectScene();
                    this.Locked = true;
                }
            }
        }

        public void OnEventRaised(IPlayer player)
        {
            this.StageClear(player);
        }

        public void OnEventRaised()
        {
            this.StageClear();
        }

    }
}