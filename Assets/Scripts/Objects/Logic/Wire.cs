using UnityEngine;

[RequireComponent(typeof (MeshCollider))]
public class Wire : Interactable
{
    [SerializeField] private LineRenderer _lineRenderer;

    private Output _source;
    private Input _target;

    public Output Source => _source;
    public Input Target => _target;

    public override void Interact(InteractionContext _)
    {
        DestroyWire();
    }

    public override string GetDescription(InteractionContext ctx)
    {
        return "[ЛКМ] Убрать провод";
    }

    public override Bounds GetBounds()
    {
        Vector3 start = _lineRenderer.GetPosition(0);
        Vector3 end = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);

        Bounds bounds = new(start, Vector3.zero);
        bounds.Encapsulate(end);

        bounds.Expand(_lineRenderer.startWidth);

        return bounds;
    }

    public void Initialize(Output source, Input target)
    {
        _source = source;
        _target = target;

        _source.AddOutgoingWire(this);
        _target.SetIncomingWire(this);

        _source.SetConnected();
        _target.SetConnected();

        if (_lineRenderer != null && _source != null && _target != null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _source.transform.position);
            _lineRenderer.SetPosition(1, _target.transform.position);
        }

        GenerateMesh();
    }

    public void DestroyWire()
    {
        if (_source != null)
        {
            _source.RemoveOutgoingWire(this);
            if (_source.OutgoingWires.Count == 0)
                _source.ResetConnected();
        }

        if (_target != null)
        {
            _target.ClearIncomingWire();
            _target.ResetConnected();
        }

        Destroy(gameObject);
    }

    private void GenerateMesh()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        Mesh mesh = new();
        _lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }
}
