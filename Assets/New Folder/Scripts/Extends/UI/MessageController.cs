using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



namespace Extends.UI
{
    public class MessageController : MonoBehaviour
    {
        [SerializeField] private Text message_text;
        public Text Message
            => this.message_text;

        public void SetMessage(string message, float messageSpeed = 0.1f)
        {
            this.message_text.text = "";
            var _time = message.Length * messageSpeed;
            this.message_text.DOText(message, _time);
        }

        public void AddMessage(string message, float messageSpeed = 0.1f)
        {
            var _time = message.Length * messageSpeed;
            var _message = this.message_text.text + message;
            this.message_text.DOText(_message, _time);
        }
    }
}