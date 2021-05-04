using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Physics2DExtension
{
    /// <summary>
    /// Raycast2Dの拡張クラス
    /// 
    /// 参照：RayCastその4、Raycastを2Dで使う【Unity】 - kan_kikuchi様
    /// https://kan-kikuchi.hatenablog.com/entry/RayCast4
    /// </summary>
    public static class RaycastExtension
    {
        /// <summary>
        /// Rayの表示時間
        /// </summary>
        private const float RAY_DISPLAY_TIME = 2f;

        /// <summary>
        /// Raycast + 参照線を描画
        /// </summary>
        public static RaycastHit2D RaycastAndDraw(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

#if UNITY_EDITOR
            //衝突時のRayを画面に表示
            if (hit.collider)
            {
                Debug.DrawRay(origin, hit.point - origin, Color.blue, RAY_DISPLAY_TIME, false);
            }
            //非衝突時のRayを画面に表示
            else
            {
                Debug.DrawRay(origin, direction * maxDistance, Color.green, RAY_DISPLAY_TIME, false);
            }
#endif
            return hit;
        }
    }
}