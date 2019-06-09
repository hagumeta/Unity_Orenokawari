using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stickman_PlayerOperationablePlatformActor : PlayerOperationablePlatformActor
{
    [System.NonSerialized]public Transform CheckPoint;
    Vector3 startPosition;

    private bool isCleared;

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
            this.CreateDeathCorpse(other.tag);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.IsFrozen) {
            if (!collision.isTrigger) { return; }
            GameObject other = collision.gameObject;
            this.CreateDeathCorpse(other.tag);
        }
    }


    protected void CreateDeathCorpse(string deathTag)
    {
        foreach (var death in this.DeathCollections)
        {
            if (death.DeathTag == deathTag)
            {
                var corpse = death.Call(deathTag, this.transform.position, this.transform.localScale);
                this.Death(death.rebornTime);


                var cam = Camera.main.GetComponent<CameraController>();
                //生き返るまでカメラを固定する
                cam.Lock(death.rebornTime);
                
                //クリアしてたらフォーカスは死体に押し付ける
                if (this.isCleared)
                {
                    cam.FocusOnObject(corpse.transform);
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
            if (this.CheckPoint != null)
            {
                this.transform.position = this.CheckPoint.position;
            }
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
            cam.FocusOnObject(this.transform, 2f, new Vector2(0, 1.5f));
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