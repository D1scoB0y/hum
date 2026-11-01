using UnityEngine;

using System.Collections.Generic;

public class Output : Pin
{
    [SerializeField] private Node _owner;
    private readonly List<Wire> _outgoingWires = new();

    public List<Wire> OutgoingWires => _outgoingWires;

    public override PinType GetPinType() => PinType.Out;

    public void AddOutgoingWire(Wire wire)
    {
        if (!_outgoingWires.Contains(wire))
        {
            _outgoingWires.Add(wire);
        }
    }

    public void RemoveOutgoingWire(Wire wire)
    {
        _outgoingWires.Remove(wire);
    }

    public SignalType Evaluate()
    {
        if (Signal == SignalType.None && _owner != null)
        {
            SetSignal(_owner.Evaluate());
        }
        return Signal;
    }

    public void ResetSignalRecursive()
    {
        ResetSignal();

        if (_owner != null)
            _owner.ResetInputs();
    }
}
