using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DeathCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int deathCount {
        private set { if (this.text != null) {this.text.text = value.ToString(); } }
        get { if (this.text != null) { return int.Parse(this.text.text); } else { return 0; } }
    }

    private static DeathCounter instance;

    private void Start()
    {
        this.deathCount = 0 ;
        instance = this     ;
    }

    public static void AddCount()
    {
        if (instance != null) {
            instance.deathCount++;
        }
    }

    public static int GetCount() {
        if (instance != null)
        {
            return instance.deathCount;
        }
        else
        {
            return 0;
        }
    }
}