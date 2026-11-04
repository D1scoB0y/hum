using System;
using UnityEngine;
using UnityEngine.Assertions;

enum NodeType
{
    And,
    Or,
    Xor,
}

public class Node : MonoBehaviour
{
    [SerializeField] private Input[] _inputs;
    [SerializeField] private Output _output;
    [SerializeField] private NodeType _type;

    private void Awake()
    {
        Assert.IsNotNull(_inputs);
        Assert.IsTrue(_inputs.Length > 0);
        Assert.IsNotNull(_output);
    }

    public void SetPlaced()
    {
        foreach (var input in _inputs)
        {
            input.SetVisible();
        }

        _output.SetVisible();
    }

    public SignalType Evaluate()
    {
       return _type switch
        {
            NodeType.And => EvaluateAnd(),
            NodeType.Or  => EvaluateOr(),
            NodeType.Xor => EvaluateXor(),
            _            => throw new ArgumentOutOfRangeException(nameof(_type), _type, "Unknown node type")
        };
    }

    private SignalType EvaluateAnd()
    {
        foreach (var input in _inputs)
        {
            if (input.Evaluate() == SignalType.Low)
            {
                return SignalType.Low;
            }
        }

        return SignalType.High;
    }

    private SignalType EvaluateOr()
    {
        foreach (var input in _inputs)
        {
            if (input.Evaluate() == SignalType.High)
            {
                return SignalType.High;
            }
        }

        return SignalType.Low;
    }

    private SignalType EvaluateXor()
    {
        if (_inputs[0].Evaluate() == SignalType.High && _inputs[1].Evaluate() == SignalType.Low
            || _inputs[0].Evaluate() == SignalType.Low && _inputs[1].Evaluate() == SignalType.High)
        {
            return SignalType.High;
        }

        return SignalType.Low;
    }

    private void OnDestroy()
    {
        foreach (var input in _inputs)
        {
            if (input != null && input.IncomingWire != null)
                input.IncomingWire.DestroyWire();
        }

        foreach (var wire in _output.OutgoingWires.ToArray())
        {
            if (wire != null)
            {
                wire.DestroyWire();
            }
        }
    }

    public void ResetInputs()
    {
        foreach (var input in _inputs)
        {
            input.ResetSignalRecursive();
        }
    }
}
