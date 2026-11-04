using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class WireGhost : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private Pin _source;

    public void Initialize(Pin source)
    {
        _source = source;
        _lineRenderer.positionCount = 2;
        UpdateEndPoint();
    }

    public void UpdateEndPoint()
    {
        if (_source == null)
            return;

        _lineRenderer.SetPosition(0, _source.transform.position);
        _lineRenderer.SetPosition(1, Camera.main.transform.position +
            Camera.main.transform.forward * 1f);
    }

    private void Update()
    {
        UpdateEndPoint();
    }
}
