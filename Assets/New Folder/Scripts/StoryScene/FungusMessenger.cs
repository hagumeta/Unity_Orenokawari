using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

namespace Extends.FungusSupporter{
    public class FungusMessenger : MonoBehaviour
    {
        [SerializeField] protected Flowchart flowchart;
        [SerializeField] protected string message;

        protected virtual void SendMessage()
        {
            flowchart.SendFungusMessage(this.message);
        }
    }
}