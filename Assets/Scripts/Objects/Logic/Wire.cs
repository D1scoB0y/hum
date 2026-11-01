using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private Output _source;
    private Input _target;

    public Output Source => _source;
    public Input Target => _target;

    public void Initialize(Output source, Input target)
    {
        _source = source;
        _target = target;

        // Регистрируем провод у обоих пинов
        _source.AddOutgoingWire(this);
        _target.SetIncomingWire(this);

        UpdateVisuals();
    }

    // Полное удаление провода (например, при клике на него или сбросе)
    public void DestroyWire()
    {
        if (_source != null) 
            _source.RemoveOutgoingWire(this);
            
        if (_target != null) 
            _target.ClearIncomingWire();

        Destroy(gameObject);
    }

    private void UpdateVisuals()
    {
        if (_lineRenderer != null && _source != null && _target != null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _source.transform.position);
            _lineRenderer.SetPosition(1, _target.transform.position);
        }
    }

    private void LateUpdate()
    {
        // Обновляем позиции линии, если пины двигаются
        UpdateVisuals();
    }
}