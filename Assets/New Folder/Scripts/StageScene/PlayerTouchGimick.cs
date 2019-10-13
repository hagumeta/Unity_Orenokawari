using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Stage
{
    internal abstract class PlayerTouchGimick : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject other = collision.gameObject;
            if (other.tag == "Player")
            {
                var player = other.GetComponent<IPlayer>();
                if (player != null)
                {
                    this.OnPlayerTouched(other.transform);
                }
            }
        }

        protected abstract void OnPlayerTouched(Transform Player);
    }
}