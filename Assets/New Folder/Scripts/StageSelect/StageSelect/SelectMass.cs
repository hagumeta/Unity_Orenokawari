using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputUtil;

namespace Game.StageSelect
{
    public abstract class SelectMass : MassEvent
    {

        public ISelectController controller;
        public int id;

        protected override void actionSelected()
        {
            this.controller.InvokeSelectedAction(this.id);
        }

        protected override void actionUnSelected()
        {
            //
        }

        protected override void actionOnSelected()
        {
            //
        }

        protected void EnterSelect()
        {
            this.controller.InvokeEnteredAction(this.id);
        }
    }
}