using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Game.StageSelect
{
    public class DeathCountDisplayer : MonoBehaviour
    {
        public Sprite Icon
        {
            get => this.GetComponentInChildren<Image>().sprite;
            set => this.GetComponentInChildren<Image>().sprite = value;
        }
        public int Count
        {
            set => GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
            get => int.Parse(GetComponentInChildren<TextMeshProUGUI>().text);
        }
    }
}