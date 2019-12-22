using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Actors.Platformers
{
    [RequireComponent(typeof(Collider2D))]
    /// <summary>
    /// ＜参照用：PlatfomActor＞
    /// PlatformActorの足.
    /// 他オブジェクトと衝突している間，IsLandingをtrueにする
    /// </summary>
    public class FootOfPlatformActor : MonoBehaviour
    {
        public bool IsLanding = false;
        private bool blank = false;


        [SerializeField] private ContactFilter2D contactFilter;
        private new Collider2D collider;
        private Collider2D[] touchedColliders;

        void Start()
        {
            foreach (var collider in this.GetComponents<Collider2D>())
            {
                collider.isTrigger = true;
            }
            this.collider = this.GetComponent<Collider2D>();
            this.touchedColliders = new Collider2D[5];
        }

        private void Update()
        {

            this.IsLanding = (this.collider.OverlapCollider(this.contactFilter, this.touchedColliders) > 0);


            this.blank = false;
        }
    }
}