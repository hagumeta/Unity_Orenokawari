using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathOfNeedleManager : DeathCorpseManager
{

    [SerializeField] private AudioSource audio_needleSticked;
    protected override void Start()
    {
        base.Start();
    }
}