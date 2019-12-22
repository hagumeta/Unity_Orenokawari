using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Corpse
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Corpse : MonoBehaviour
    {
        protected abstract IEnumerator CorpseCoroutine();
        private AudioSource _audioSource;
        public bool IgnoreNamusan;

        public virtual void Namusan(GameObject namusanObj = null)
        {
            if (this.IgnoreNamusan) return;

            this.audioSource.clip = GameManager.PlayerCollection.GetPlayer(PlayerType.Stand2DAction).namusanSound;
            this.audioSource.Play();

            if (namusanObj == null) {
                namusanObj = 
                    GameManager.PlayerCollection.GetPlayer(PlayerType.Stand2DAction).namusanObject;
            }
            var obj = Instantiate(namusanObj);
            obj.transform.position = this.transform.position;
            obj.transform.localScale = this.transform.localScale;
        }

        protected AudioSource audioSource
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

        private void Start()
        {
            StartCoroutine(this.CorpseCoroutine());
        }
    }
}