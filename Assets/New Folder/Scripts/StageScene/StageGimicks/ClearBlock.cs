using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

namespace Game.Stage.Objects
{
    [RequireComponent(typeof(Tilemap))]
    public class ClearBlock : MonoBehaviour, IPlayerTouchGimick
    {
        protected Tilemap Tilemap;

        private void Start()
        {
            this.Tilemap = this.GetComponent<Tilemap>();
        }

        public void OnPlayerReleased(Transform player)
        {
            this.FadeTilemap(1, 3f);
        }

        public void OnPlayerTouched(Transform player)
        {
            this.FadeTilemap(0f, 1f);
        }

        protected void FadeTilemap(float alphaTo, float time)
        {
            if (this.Tilemap != null)
            {
                DOTween.ToAlpha(() => this.Tilemap.color, (n) => this.Tilemap.color = n, alphaTo, 1f);
            }
        }
    }
}