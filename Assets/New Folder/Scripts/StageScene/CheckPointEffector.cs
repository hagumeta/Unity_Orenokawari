using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Stage {
    [RequireComponent(typeof(CheckPoint))]
    public class CheckPointEffector : MonoBehaviour
    {
        private CheckPoint checkPoint;
        [SerializeField] private GameObject effectOfCheckedObject;

        void Start()
        {
            this.checkPoint = this.GetComponent<CheckPoint>();

            if (this.checkPoint == null || this.effectOfCheckedObject == null) {
                this.enabled = false;
            }
        }

        void Update()
        {
            if (this.effectOfCheckedObject.activeSelf != this.checkPoint.IsLatest)
            {
                this.effectOfCheckedObject.SetActive(this.checkPoint.IsLatest);
                this.transform.DOPunchScale(
                    new Vector3(0.2f, 0.7f, 1f),
                    1f
                    );
            }
        }
    }
}