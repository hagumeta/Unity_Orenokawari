using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class Anim_BlinkText : MonoBehaviour
{
    [SerializeField]private float durationSeconds;
    [SerializeField] private Ease easeType;

    private Text text;
    void Start()
    {
        this.text = this.GetComponent<Text>();
        this.text.DOFade(0.0f, this.durationSeconds).SetEase(this.easeType).SetLoops(-1, LoopType.Yoyo);
    }
}