using UnityEngine;

[RequireComponent(typeof (Pin), typeof (Renderer), typeof (BoxCollider))]
public class PinView : MonoBehaviour
{
    [SerializeField] private Material _none;
    [SerializeField] private Material _high;
    [SerializeField] private Material _low;

    private Pin _pin;
    private Renderer _renderer;
    private Collider _collider;

    private void Awake()
    {        
        _pin = GetComponent<Pin>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        SetVisible(_pin.Visible);
    }

    private void OnEnable()
    {
        _pin.OnChanged += UpdateView;
    }

    private void OnDisable()
    {
        _pin.OnChanged -= UpdateView;
    }

    public void SetVisible(bool visible)
    {
       _renderer.enabled = visible;
       _collider.enabled = visible;
    }

    private void UpdateView()
    {
        SetVisible(_pin.Visible);

        switch (_pin.Signal)
        {
            case SignalType.None:
            _renderer.material = _none;
            break;
            case SignalType.Low:
            _renderer.material = _low;
            break;
            case SignalType.High:
            _renderer.material = _high;
            break;
        }

        if (_pin.Selected)
            _renderer.material = _none;
    }
}
