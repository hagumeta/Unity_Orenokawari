using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Corpse
{


    [RequireComponent(typeof(AudioSource))]
    public class Corpse : MonoBehaviour
    {
        private AudioSource _audioSource;
        private AudioSource audioSource
        {
            get
            {
                if (this._audioSource == null)
                {
                    this._audioSource = this.GetComponent<AudioSource>();
                }
                return this._audioSource;
            }
        }
       
        protected virtual void Namusan(GameObject namusanObj = null)
        {
            this.audioSource.clip = 
                GameManager.PlayerCollection.GetPlayer(PlayerType.Stand2DAction).namusanSound;
            this.audioSource.Play();

            if (namusanObj == null) {
                namusanObj = 
                    GameManager.PlayerCollection.GetPlayer(PlayerType.Stand2DAction).namusanObject;
            }
            var obj = Instantiate(namusanObj);
            obj.transform.position = this.transform.position;
            obj.transform.localScale = this.transform.localScale;
        }
    }


}