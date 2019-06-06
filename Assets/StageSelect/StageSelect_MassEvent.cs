using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelect_MassEvent : MassEvent{

    public enum State
    {
        Cleared, Locked, UnCleared
    }
    public State state;
    public int StageID;
    public Scene stageScene;

    public GameObject UnclearedStageIcon, LockedStageIcon, ClearedStageIcon;
    private Vector3 defaultScale;

    void Start () {
        
        this.defaultScale = this.transform.localScale;
        TextMeshPro tmpro;
        switch (this.state)
        {
            case State.UnCleared:
                this.UnclearedStageIcon.SetActive(true);
                tmpro = this.UnclearedStageIcon.GetComponentInChildren<TextMeshPro>();
                if (tmpro != null)
                {
                    tmpro.text = this.StageID.ToString();
                }
                break;
            case State.Cleared:
                this.ClearedStageIcon.SetActive(true);
                tmpro = this.ClearedStageIcon.GetComponentInChildren<TextMeshPro>();
                if (tmpro != null)
                {
                    tmpro.text = this.StageID.ToString();
                }
                break;
            case State.Locked:
                this.LockedStageIcon.SetActive(true);
                tmpro = this.LockedStageIcon.GetComponentInChildren<TextMeshPro>();
                if (tmpro != null)
                {
                    tmpro.text = this.StageID.ToString();
                }
                break;
        }

        if (this.state == State.Locked)
        {
            this.GetComponent<Mass>().IsActive = false;
        }
    }

    private void Update()
    {
        if (this.IsSelected)
        {
            if (Input.GetButtonDown("Jump"))
            {
                foreach (var a in (Cursor[])GameObject.FindObjectsOfType(typeof(Cursor)))
                {
                    a.isActivate = false;
                }

                StageSelect_IdController.MoveStageScene(this.StageID);
            }
        }
    }


    private void CallBackEventInfoClosed()
    {
        foreach (var obj in FindObjectsOfType<Cursor>())
        {
            obj.isActivate = true;
        }
    }


    protected override void actionSelected()
    {
        if (DOTween.IsTweening(this.transform))
        {
            DOTween.Kill(this.transform);
        }
        this.transform.DOScale(this.defaultScale * 1.3f, 0.4f);
    }

    protected override void actionUnSelected()
    {
        if (DOTween.IsTweening(this.transform))
        {
            DOTween.Kill(this.transform);
        }
        this.transform.DOScale(this.defaultScale, 0.2f);
    }
}