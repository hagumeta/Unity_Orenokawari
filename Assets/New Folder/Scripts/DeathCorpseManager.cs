using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InstanceCreationPlanner))]
public class DeathCorpseManager : MonoBehaviour
{

    [SerializeField]private GameObject[] CorpsePatturns;
    //[SerializeField]private AudioSource audio_namusanSound;

    protected virtual void Start()
    {
        int index = Mathf.FloorToInt(Random.Range(0, this.CorpsePatturns.Length));
        Instantiate(this.CorpsePatturns[index], this.transform);
    //    this.GetComponent<InstanceCreationPlanner>().callBackAtEnd = new InstanceCreationPlanner.CallBack(this.Namusan);
    }

    /*protected virtual void Namusan()
    {
        if (this.audio_namusanSound != null) {
            this.audio_namusanSound.Play();
        }
    }*/
}