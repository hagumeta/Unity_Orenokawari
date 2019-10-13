using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Game.Stage.Corpse
{
    public class DeathOfFireManager : DeathCorpseManager
    {
        [SerializeField] Transform FireTransform;
        [SerializeField] Transform StickmanTransform;

        protected void Start()
        {
            StartCoroutine(this.IEBurning());
        }
        
        private IEnumerator IEBurning()
        {
            var collider = this.StickmanTransform.GetComponent<Collider2D>();
            var anim = this.StickmanTransform.GetComponent<Animator>();
            var sprite = this.StickmanTransform.GetComponent<SpriteRenderer>();
            anim.speed = Random.RandomRange(0.8f, 1.2f);
            collider.enabled = false;
            this.FireTransform.DOScale(new Vector3(1.6f, 2.8f, 0.01f), 0.5f);

            yield return new WaitForSeconds(1f);

            DOTween.To(
                () => sprite.color,
                color => sprite.color = color,
                Color.black,
                1f
            );
            DOTween.To(
                () => anim.speed,
                speed => anim.speed = speed,
                0f,
                1f
                );

            yield return new WaitForSeconds(1.2f);
            collider.enabled = true;

            this.FireTransform.DOScale(new Vector3(0f, 0f), 0.2f);
            yield return new WaitForSeconds(0.5f);

            this.Namusan();
            this.StickmanTransform.GetComponent<Rigidbody2D>().isKinematic = false;

            yield return new WaitForSeconds(5f);
            Destroy(this.StickmanTransform.gameObject);

            Destroy(this.gameObject, 10f);
        }
    }
}