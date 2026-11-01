using UnityEngine;

public class FocusFrame : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private Camera _camera;

    private float _padding = 8f;

    public void Show(Bounds bounds)
    {
        gameObject.SetActive(true);

        Vector2 min = new(float.MaxValue, float.MaxValue);
        Vector2 max = new(float.MinValue, float.MinValue);

        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        for (int x = -1; x <= 1; x += 2)
        for (int y = -1; y <= 1; y += 2)
        for (int z = -1; z <= 1; z += 2)
        {
            Vector3 corner = center + Vector3.Scale(
                extents,
                new Vector3(x, y, z)
            );

            Vector3 screen = _camera.WorldToScreenPoint(corner);

            if (screen.z <= 0)
                continue;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                screen,
                null,
                out Vector2 local
            );

            min = Vector2.Min(min, local);
            max = Vector2.Max(max, local);
        }

        min -= Vector2.one * _padding;
        max += Vector2.one * _padding;

        _rect.anchoredPosition = (min + max) / 2;
        _rect.sizeDelta = max - min;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}