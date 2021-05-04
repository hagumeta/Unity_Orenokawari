using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Physics2DExtension
{
    /// <summary>
    /// Raycast2D�̊g���N���X
    /// 
    /// �Q�ƁFRayCast����4�ARaycast��2D�Ŏg���yUnity�z - kan_kikuchi�l
    /// https://kan-kikuchi.hatenablog.com/entry/RayCast4
    /// </summary>
    public static class RaycastExtension
    {
        /// <summary>
        /// Ray�̕\������
        /// </summary>
        private const float RAY_DISPLAY_TIME = 2f;

        /// <summary>
        /// Raycast + �Q�Ɛ���`��
        /// </summary>
        public static RaycastHit2D RaycastAndDraw(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

#if UNITY_EDITOR
            //�Փˎ���Ray����ʂɕ\��
            if (hit.collider)
            {
                Debug.DrawRay(origin, hit.point - origin, Color.blue, RAY_DISPLAY_TIME, false);
            }
            //��Փˎ���Ray����ʂɕ\��
            else
            {
                Debug.DrawRay(origin, direction * maxDistance, Color.green, RAY_DISPLAY_TIME, false);
            }
#endif
            return hit;
        }
    }
}