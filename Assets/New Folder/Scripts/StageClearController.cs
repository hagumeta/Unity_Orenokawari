using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StageClearController : MonoBehaviour
{
    public TextMeshProUGUI DeathCountText;
    public Transform ClearTextObj, DeathCountTextObj;


    private static StageClearController instance;
    void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }

    private void Init()
    {
        //this.transform.position = new Vector3(-500, 130);
        if (this.DeathCountText != null)
        {
            this.DeathCountText.text = DeathCounter.GetCount().ToString();
        }
        StartCoroutine(this.action());
    }

    private IEnumerator action()
    {
        this.ClearTextObj.DOLocalMoveX(500, 1f);
        yield return new WaitForSeconds(1.2f);
        this.DeathCountTextObj.DOLocalMoveX(500, 1f);

        yield return new WaitForSeconds(2f);
    }


    public static void Create()
    {
        if (instance != null) {
            instance.gameObject.SetActive(true);
            instance.Init();
        }
    }
}