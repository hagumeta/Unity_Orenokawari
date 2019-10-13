using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.StageSelect
{

    public interface ISelectController
    {
        Information[] InformationList { get; }

        Information GetInformation(int index);
        void InvokeSelectedAction(int index);
        void InvokeEnteredAction(int index);
    }

    public abstract class Information
    {
    }

    public abstract class ImformationDisplayer
    {
    }
}