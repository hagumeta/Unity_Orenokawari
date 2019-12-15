using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Stage.Objects;

namespace Game.Stage.Objects
{
    public class SwitchTileMapController : MonoBehaviour
    {
        public Color ON_Color, OFF_Color;

        private Tilemap _tilemap;
        protected Tilemap tilemap
        {
            get
            {
                if (this._tilemap == null)
                {
                    this._tilemap = this.GetComponent<Tilemap>();
                }
                return this._tilemap;
            }
        }
        private TilemapCollider2D _tilemapCollider;
        protected TilemapCollider2D tilemapCollider
        {
            get
            {
                if (_tilemapCollider == null)
                {
                    this._tilemapCollider = this.GetComponent<TilemapCollider2D>();
                }
                return this._tilemapCollider;
            }
        }

        [System.Serializable]
        public enum SW
        {
            ON, OFF
        }

        public void Switch(SW sw)
        {
            if (sw == SW.ON)
            {
                this.tilemapCollider.enabled = true;
                this.tilemap.color = ON_Color;
            }
            else
            {
                this.tilemapCollider.enabled = false;
                this.tilemap.color = OFF_Color;
            }
        }

        public void Switch_ON()
        {
            this.Switch(SW.ON);
        }

        public void Switch_OFF()
        {
            this.Switch(SW.OFF);
        }
    }
    
}