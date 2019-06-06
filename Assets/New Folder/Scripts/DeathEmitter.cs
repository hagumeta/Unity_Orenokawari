using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 自身下のパーティクルシステムを呼び出すスクリプト．
/// waitTimeあとに一斉に起動し，LiveTime後に全員自身ごと削除する．
/// </summary>
public class DeathEmitter : MonoBehaviour
{
    [SerializeField]
    private float waitTime, liveTime;

    [SerializeField]
    private bool isDestroyAfterEmit;

    ParticleSystem[] particleSystems;
    private void Start()
    {
        this.StartSystem();
    }

    private IEnumerator Action()
    {
        yield return new WaitForSeconds(this.waitTime);

        this.invokeMyPartivcle();

        yield return new WaitForSeconds(this.liveTime);

        if (this.isDestroyAfterEmit) {
            Destroy(this.gameObject);
        }
        else
        {
            this.stopMyParticle();
        }
    }


    private void stopMyParticle()
    {
        foreach (var part in this.particleSystems)
        {
            part.Stop();
        }
    }
    private void invokeMyPartivcle()
    {
        foreach (var part in this.particleSystems)
        {
            part.Play();
        }
    }


    public void StartSystem()
    {
        this.particleSystems = this.GetComponentsInChildren<ParticleSystem>();
        this.StartCoroutine(this.Action());
    }
}