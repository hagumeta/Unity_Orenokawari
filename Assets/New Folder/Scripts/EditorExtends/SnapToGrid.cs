using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 参考
/// https://qiita.com/keroxp/items/97d375786617c9eca783
/// オブジェクトのグリッドを自動でできるエディタ限定スクリプト
/// </summary>


[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SnapToGrid : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector2 cellSize = new Vector3(1, 1);
    [SerializeField] private float width = 1;
    [SerializeField] private float height = 1;
    [SerializeField] private Vector2 offset;
    [SerializeField] private bool isOdd = true;

    private enum HorizontalPivot
    {
        Left = -1,
        Middle = 0,
        Right = 1
    }

    private enum VerticalPivot
    {
        Top = 1,
        Midle = 0,
        Bottom = -1
    }

    [SerializeField] private HorizontalPivot _horizontalPivot = HorizontalPivot.Middle;
    [SerializeField] private VerticalPivot _verticalPivot = VerticalPivot.Midle;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
    }

    private Vector3 pivotCenter
    {
        get
        {
            var v = new Vector3(
                ((int)_horizontalPivot + 1) / 2f * this.cellSize.x,
                ((int)_verticalPivot + 1) / 2f * this.cellSize.x,
                0
            );
            return new Vector3(minX, minY, 0) + Vector3.Scale(new Vector3(width, height, 0), v);
        }
    }

    private Bounds bounds
    {
        get { return _renderer.bounds; }
    }

    private float minX
    {
        get
        {
            switch (_horizontalPivot)
            {
                case HorizontalPivot.Left:
                    return bounds.min.x + offset.x;
                case HorizontalPivot.Middle:
                    return bounds.center.x - (cellSize.x * width) / 2 + offset.x;
                case HorizontalPivot.Right:
                    return bounds.max.x - cellSize.x * width + offset.x;
            }
            return 0;
        }
    }

    private float minY
    {
        get
        {

            switch (_verticalPivot)
            {
                case VerticalPivot.Top:
                    return bounds.max.y - cellSize.y * height + offset.y;
                case VerticalPivot.Midle:
                    return bounds.center.y - (cellSize.y * height) / 2 + offset.y;
                case VerticalPivot.Bottom:
                    return bounds.min.y + offset.y;
            }
            return 0;
        }
    }

    private void DoSnap()
    {
        var x = minX + cellSize.x * width;
        var y = minY + cellSize.y * height;

        var x0 = Mathf.Floor(center.x + cellSize.x * (float)(x / cellSize.x));

        if (isOdd) x0 -= cellSize.x / 2f;
        var y0 = Mathf.Floor(center.y + cellSize.y * (float)(y / cellSize.y));
        if (isOdd) y0 -= cellSize.y / 2f;
        var dx = x - x0 > 0.5f ? x0 + cellSize.x - x : x0 - x;
        var dy = y - y0 > 0.5f ? y0 + cellSize.y - y : y0 - y;

        Debug.Log(dx + "  " + dy);

        //transform.position += new Vector3(dx, dy, 0);
        
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DoSnap();
        }
    }

    void OnDrawGizmos()
    {
        // ギズモのアイコンは自分で設定してね！
        Gizmos.DrawIcon(pivotCenter, "MyGizmoCircleOrange");
        Gizmos.color = Color.blue;
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var l = minX + i * cellSize.x;
                var t = minY + j * cellSize.y;
                var r = l + cellSize.x;
                var b = t + cellSize.y;
                DrawQuad(l, t, r, b);
            }
        }
    }

    private static void DrawQuad(float l, float t, float r, float b)
    {
        DrawLine2D(l, t, r, t);
        DrawLine2D(r, t, r, b);
        DrawLine2D(r, b, l, b);
        DrawLine2D(l, b, l, t);
    }

    private static void DrawLine2D(float sx, float sy, float ex, float ey)
    {
        Gizmos.DrawLine(new Vector3(sx, sy, 0), new Vector3(ex, ey, 0));
    }
}

