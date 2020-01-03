using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestScripts
{

    public class TestAudio : MonoBehaviour
    {

        public AudioClip sound1;
        public AudioClip sound2;
        public AudioClip sound3;
        public AudioClip sound4;

        AudioSource audioSource;

        void Start()
        {
            //Componentを取得
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // 左
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                audioSource.PlayOneShot(sound1);
            }
            // 右
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                audioSource.PlayOneShot(sound2);
            }
            // 上
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                audioSource.PlayOneShot(sound3);
            }
            // 下
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(sound4);
            }
        }

    }
}