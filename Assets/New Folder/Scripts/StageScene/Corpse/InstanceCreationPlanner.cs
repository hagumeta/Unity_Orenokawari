using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCreationPlanner : MonoBehaviour
{

    [SerializeField]
    private CreationPlan[] creationPlans;
    [SerializeField]
    private bool isLoop, isDestroyAtEnd;


    public delegate void CallBack();
    public CallBack callBackAtEnd;

    void Start()
    {
        this.StartCoroutine(this.Creation());       
    }

    private IEnumerator Creation()
    {
        while (true) {
            foreach (var creation in this.creationPlans)
            {
                if (!creation.isSkipThis) {
                    if (!creation.gameObject.activeSelf)
                    {
                        creation.gameObject.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(creation.time);
            }
            if (!this.isLoop)
            {
                break;
            }
        }

        if (this.callBackAtEnd != null) {
            this.callBackAtEnd();
        }
        if (this.isDestroyAtEnd) {
            Destroy(this.gameObject);
        }
    }

    [System.Serializable]
    struct CreationPlan
    {
        public GameObject gameObject;
        public float time;
        public bool isSkipThis;
    }
}