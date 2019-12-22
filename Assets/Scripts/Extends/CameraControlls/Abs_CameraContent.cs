using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.CameraControlls
{
    public class Abs_CameraContent : MonoBehaviour
    {
        /// <summary>
        /// 自身のカメラを取得するプロパティ
        /// </summary>
        private new Camera camera;
        protected Camera Camera
        {
            get
            {
                if (this.camera == null)
                {
                    this.camera = this.GetComponent<Camera>();
                }
                return this.camera;
            }
        }

        /// <summary>
        /// 現在の自身のカメラサイズを取得するプロパティ
        /// </summary>
        protected Vector2 CameraSizeInWorld
        {
            get => new Vector2(this.Camera.orthographicSize * 2 * this.Camera.aspect, this.Camera.orthographicSize * 2);
        }

    }
}