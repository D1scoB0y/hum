using UnityEngine;

public class Wiring : MonoBehaviour
{
    [SerializeField] private GameObject _wirePrefab;

    private Pin _startPin;

    private void OnEnable()
    {
        Pin.OnClicked += HandlePinClick;
    }

    private void OnDisable()
    {
        Pin.OnClicked -= HandlePinClick;
    }

    public void HandlePinClick(Pin clickedPin)
    {
        if (_startPin == null)
        {
            _startPin = clickedPin;
            _startPin.SetSelected();
            return;
        }

        if (_startPin == clickedPin)
        {
            _startPin.ResetSelected();
            ResetSelection();
            return;
        }

        if (_startPin.GetPinType() == clickedPin.GetPinType())
            return;

        CreateWire(_startPin, clickedPin);
        _startPin.ResetSelected();
        ResetSelection();
    }

    private void CreateWire(Pin pinA, Pin pinB)
    {
        // Определяем, где Output, а где Input
        Output output = pinA as Output ?? pinB as Output;
        Input input = pinA as Input ?? pinB as Input;

        if (output != null && input != null && _wirePrefab != null)
        {
            GameObject wireObj = Instantiate(_wirePrefab);
            Wire wire = wireObj.GetComponent<Wire>();
            wire.Initialize(output, input);
        }
    }

    private void ResetSelection()
    {
        _startPin = null;
    }
}
