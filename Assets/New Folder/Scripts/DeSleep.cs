using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSleep : MonoBehaviour
{
    [SerializeField] private float TimeUntilSleepTime;
    private void OnEnable()
    {
        this.Invoke("sleep", this.TimeUntilSleepTime);
    }

    private void sleep()
    {
        this.gameObject.SetActive(false);
    }
}