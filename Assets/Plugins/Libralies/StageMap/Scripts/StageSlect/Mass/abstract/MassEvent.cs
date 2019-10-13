using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class MassEvent : MonoBehaviour {
    private bool _isSelected;
    public bool IsSelected
    {
        get => this._isSelected;
        set
        {
            if (value)
            {
                this.actionSelected();
            }
            else
            {
                this.actionUnSelected();
            }
            this._isSelected = value;
        }
    }

    private void Update()
    {
        if (this.IsSelected) {
            this.actionOnSelected();
        }
    }


    abstract protected void actionSelected();
    abstract protected void actionUnSelected();
    abstract protected void actionOnSelected();
}