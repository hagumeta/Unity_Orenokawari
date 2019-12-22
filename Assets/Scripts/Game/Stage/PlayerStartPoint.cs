using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Stage
{
    public class PlayerStartPoint : CheckPoint
    {
        public new PlayerType playerType;

        private void Start()
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}