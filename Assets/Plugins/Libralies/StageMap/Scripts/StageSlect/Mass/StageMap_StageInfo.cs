using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StageMap_StageInfo : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI TMPro_stageID;

    private static StageMap_StageInfo instance;
    private int stageID;
    private bool isLesteningInput = false;

    public delegate void CallBackOnClosed();
    private CallBackOnClosed callback;


    void Start () {
        instance = this;
        this.gameObject.SetActive(false);
	}


    private void Update()
    {
        if (this.isLesteningInput) {
            if (Input.GetButtonDown("Shot"))
            {
                //SwitchStageManager.MoveToStage(this.stageID);
            }
            if (Input.GetButtonDown("Shift"))
            {
                this.close();
            }
        }
    }



    private void open(int stageID, CallBackOnClosed callback)
    {
        this.stageID = stageID;
        this.TMPro_stageID.text = stageID.ToString();
        DOTween.Kill(this.GetComponent<RectTransform>());
        this.GetComponent<RectTransform>().DOLocalMoveX(0, 0.2f);

        this.callback = callback;
        Invoke("enableMe", 0.2f);
    }
    private void enableMe()
    {
        this.isLesteningInput = true;
    }

    private void close()
    {
        DOTween.Kill(this.GetComponent<RectTransform>());
        this.GetComponent<RectTransform>().DOLocalMoveX(400, 0.2f);
        Invoke("disableMe", 0.51f);
        this.isLesteningInput = false;
    }
    private void disableMe()
    {
        callback();
        this.gameObject.SetActive(false);
    }


    public static void Open(int stageID, CallBackOnClosed callback)
    {
        if (!instance.gameObject.activeSelf)
        {
            instance.gameObject.SetActive(true);
            instance.open(stageID, callback);
        }
    }

    public static void Close()
    {
        if (instance.gameObject.activeSelf)
        {
            instance.close();
        }
    }

}
