using UnityEngine;
using System.Linq;

public class DragManager : MonoBehaviour
{
    [SerializeField]
    private DragProperties properties;
    private Texture2D texture;
    private bool isDragging;
    private Vector3 mousePosition;

    void OnGUI()
    {
        if(!isDragging) { return; }
        var rect = GetRect(mousePosition, Input.mousePosition);
        DrawRect(rect, properties.SelectionColor);
        DrawRectBorder(rect);
    }

    void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    public void StartDragging(Vector3 startPoint)
    {
        mousePosition = startPoint;
        isDragging = true;
    }

    public void StopDragging()
    {
        isDragging = false;
    }

    public bool TryGrabUnits(UnitController[] units, out UnitController[] selectedUnits)
    {
        if(!isDragging)
        {
            selectedUnits = null;
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = GetViewportBounds(camera, mousePosition, Input.mousePosition);
        selectedUnits = units
            .Where(u =>
            {
                var vp = camera.WorldToViewportPoint(u.transform.position);
                return viewportBounds.Contains(vp);
            })
            .ToArray();
        StopDragging();
        return true;
    }

    private void DrawRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, texture);
    }

    private void DrawRectBorder(Rect rect)
    {
        if(properties.BorderWidth <= 0) { return; }
        var top = new Rect(rect.xMin, rect.yMin, rect.width, properties.BorderWidth);
        var bottom = new Rect(rect.xMin, rect.yMax - properties.BorderWidth, rect.width, properties.BorderWidth);
        var left = new Rect(rect.xMin, rect.yMin, properties.BorderWidth, rect.height);
        var right = new Rect(rect.xMax - properties.BorderWidth, rect.yMin, properties.BorderWidth, rect.height);

        DrawRect(top, properties.BorderColor);
        DrawRect(bottom, properties.BorderColor);
        DrawRect(left, properties.BorderColor);
        DrawRect(right, properties.BorderColor);
    }

    private Rect GetRect(Vector3 position1, Vector3 position2)
    {
        position1.y = Screen.height - position1.y;
        position2.y = Screen.height - position2.y;

        var topLeftCorner = Vector3.Min(position1, position2);
        var bottomRightCorner = Vector3.Max(position1, position2);
        return Rect.MinMaxRect(topLeftCorner.x, topLeftCorner.y, bottomRightCorner.x, bottomRightCorner.y);
    }

    private Bounds GetViewportBounds(Camera camera, Vector3 position1, Vector3 position2)
    {
        var vp1 = camera.ScreenToViewportPoint(position1);
        var vp2 = camera.ScreenToViewportPoint(position2);
        var min = Vector3.Min(vp1, vp2);
        var max = Vector3.Max(vp1, vp2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}

