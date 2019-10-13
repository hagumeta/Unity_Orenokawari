using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_forMassEvent : Cursor {

    private Mass _tmpCurrentMass;
    private MassEvent _tmpMassEvent;

    private MassEvent massEvent
    {
        get {
            if (this.currentMass != this._tmpCurrentMass)
            {
                this._tmpCurrentMass = this.currentMass;
                this._tmpMassEvent = this.currentMass.GetComponent<MassEvent>();
            }
            return this._tmpMassEvent;
        }
    }

    private bool massEventIsSelected
    {
        get
        {
            if (this.massEvent != null){
                return this.massEvent.IsSelected;
            }else{
                return false;
            }
        }

        set
        {
            if (this.massEvent != null){
                this.massEvent.IsSelected = value;
            }
        }
    }
    
    public void MoveToMass(MassEvent massEvent, bool immediate = false)
    {
        var mass = massEvent.GetComponent<Mass>();
        if (mass != null)
        {
            this.MoveToMass(mass, immediate);
        }
    }

    protected override void BeforeMoveAction()
    {
        this.massEventIsSelected = false;
    }

    protected override void EndMoveAction(){
        this.massEventIsSelected = true;
    }
    protected override void WaitInput(){}
}