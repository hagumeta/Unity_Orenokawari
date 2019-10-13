using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Stage {
    /// <summary>
    /// 可燃性なステージオブジェクトにつける．
    /// 他のFireオブジェクトに当たると自身も燃え上がる
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class StageObject_Burnable : MonoBehaviour
    {
        public GameObject FireStageObject;
        protected bool isBurnning;
        public float toBurnTime = 0.3f;
        public float BurningTime = 1f;
        private Transform fire;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!this.isBurnning)
            {
                var killer = collision.GetComponent<PlayerKiller>();
                if (killer != null)
                {
                    if (killer.deathType == DeathType.BurningDeath)
                    {
                        StartCoroutine(this.Burn());
                    }
                }
            }
        }


        private IEnumerator Burn()
        {
            if (!this.isBurnning)
            {
                this.isBurnning = true;

                this.fire = Instantiate(this.FireStageObject, this.transform).transform;
                this.fire.transform.localPosition = Vector3.zero;

                var sprite = this.GetComponent<SpriteRenderer>();

                this.fire.transform.DOScale(new Vector3(2.5f, 3f, 0.1f), 1f);

                yield return new WaitForSeconds(1.3f);

                DOTween.To(
                    () => sprite.color,
                    color => sprite.color = color,
                    Color.black,
                    0.5f
                );

                yield return new WaitForSeconds(this.BurningTime);

                this.fire.DOScale(new Vector3(0f, 0f), 0.3f);

                yield return new WaitForSeconds(0.5f);

                Destroy(this.gameObject);
            }
        }
        public void Update()
        {
            if (this.isBurnning && this.fire != null)
            {
                this.fire.rotation = Quaternion.identity;
            }
        }
    }
}