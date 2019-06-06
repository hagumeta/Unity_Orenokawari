using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman_PlayerOperationablePlatformActor : PlayerOperationablePlatformActor
{

    public GameObject playerClearObj;
    private bool isCleared;

    private Vector3 startPosition;
    override protected void Start()
    {
        base.Start();
        this.startPosition = this.transform.position;
    }

    [SerializeField] private DueToDeath[] DeathCollections;

    [System.Serializable]
    class DueToDeath{
        public string DeathTag;
        [SerializeField]public GameObject CorpseObjects;
        public float rebornTime;

        public GameObject Call(string tag, Vector3 corpsePosition, Vector3 corpseScale)
        {
            if (this.CorpseObjects == null)
            {
                return null;
            }
            var corpse = Instantiate(this.CorpseObjects, corpsePosition, Quaternion.identity);
            corpse.transform.localScale = corpseScale;
            return corpse;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.IsFrozen)
        {
            GameObject other = collision.gameObject;
            foreach (var death in this.DeathCollections)
            {
                if (death.DeathTag == other.tag)
                {
                    var corpse = death.Call(other.tag, this.transform.position, this.transform.localScale);
                    this.Death(death.rebornTime);

                    //クリアしてたらフォーカスは死体に押し付ける
                    if (this.isCleared) {
                        var cam = Camera.main.GetComponent<CameraController>();
                        cam.FocusObject = corpse;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.IsFrozen) {
            GameObject other = collision.gameObject;
            foreach (var death in this.DeathCollections)
            {
                if (death.DeathTag == other.tag)
                {
                    death.Call(other.tag, this.transform.position, this.transform.localScale);
                    this.Death(death.rebornTime);
                }
            }
        }
    }


    public void Death(float rebornTime)
    {
        this.IsFrozen = true;
        this.transform.position = new Vector3(100000f, 100000f, this.transform.position.z);
        DeathCounter.AddCount();

        if (!this.isCleared)
        {
            Invoke("Reborn", rebornTime);
        }
    }


    private void Reborn()
    {
        if (!this.isCleared) {
            this.IsFrozen = false;
            this.transform.position = this.startPosition;
        }
    }


    public void StageCleard()
    {
        if (!this.isCleared) {
            StartCoroutine(this.CleardAction());
        }
    }

    /// <summary>
    /// クリア時のアクション
    /// 飛び上がってピース
    /// </summary>
    /// <returns></returns>
    private IEnumerator CleardAction()
    {
        var cam = Camera.main.GetComponent<CameraController>();
        if (cam != null) {
            cam.FocusOnObject(this.gameObject, 2f, new Vector2(0, 1.5f));
        }
        this.isCleared = true;
        this.Operation.Lock();


        yield return new WaitForSeconds(0.3f);
        while (!this.CurrentState.IsLanding) {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //ジャンプのつもり
        this.VerticalSpeed += this.ActionStatus.JumpSpeed*1.5f;

        StageClearController.Create();
        

        while (!this.CurrentState.IsLanding)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (this.CurrentState.IsFalling)
            {
                this.VerticalSpeed -= 0.5f;
            }
        }
        yield return new WaitForSeconds(0.5f);

        this.Animator.SetTrigger("ClearAction");
    }
}