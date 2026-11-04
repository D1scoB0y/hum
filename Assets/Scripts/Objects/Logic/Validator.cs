using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Validator : Interactable
{
    [SerializeField] private Output[] _inputs;
    [SerializeField] private Input[] _outputs;

    private static readonly SignalType[,] _inputSignals =
    {
        { SignalType.Low,  SignalType.Low,  SignalType.Low,  SignalType.Low  },
        { SignalType.Low,  SignalType.Low,  SignalType.Low,  SignalType.High },
        { SignalType.Low,  SignalType.Low,  SignalType.High, SignalType.Low  },
        { SignalType.Low,  SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.Low,  SignalType.High, SignalType.Low,  SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.Low,  SignalType.High },
        { SignalType.Low,  SignalType.High, SignalType.High, SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.High, SignalType.High },
        { SignalType.High, SignalType.Low,  SignalType.Low,  SignalType.Low  },
        { SignalType.High, SignalType.Low,  SignalType.Low,  SignalType.High },
        { SignalType.High, SignalType.Low,  SignalType.High, SignalType.Low  },
        { SignalType.High, SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.High, SignalType.High, SignalType.Low,  SignalType.Low  },
        { SignalType.High, SignalType.High, SignalType.Low,  SignalType.High },
        { SignalType.High, SignalType.High, SignalType.High, SignalType.Low  },
        { SignalType.High, SignalType.High, SignalType.High, SignalType.High }
    };

    private static readonly SignalType[,] _outputSignals =
    {
        { SignalType.Low,  SignalType.Low,  SignalType.Low  },
        { SignalType.Low,  SignalType.Low,  SignalType.High },
        { SignalType.Low,  SignalType.High, SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.Low,  SignalType.Low,  SignalType.High },
        { SignalType.Low,  SignalType.High, SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.High, SignalType.Low,  SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.Low  },
        { SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.High, SignalType.Low,  SignalType.Low  },
        { SignalType.High, SignalType.Low,  SignalType.High },
        { SignalType.Low,  SignalType.High, SignalType.High },
        { SignalType.High, SignalType.Low,  SignalType.Low  },
        { SignalType.High, SignalType.Low,  SignalType.High },
        { SignalType.High, SignalType.High, SignalType.Low  }
    };

    private void Awake()
    {
        Assert.AreEqual(4, _inputs.Length);
        Assert.AreEqual(3, _outputs.Length);
        // Assert.AreEqual(_inputSignals.Length, _outputSignals.Length);
    }

    private void Start()
    {
        foreach (var input in _inputs)
            input.SetVisible();

        foreach (var output in _outputs)
            output.SetVisible();
    }

    public override void Interact(InteractionContext _)
    {
        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        for (int i = 0; i < _inputSignals.GetLength(0); ++i)
        {
            for (int j = 0; j < _inputs.Length; ++j)
                _inputs[j].SetSignal(_inputSignals[i, j]);
            
            for (int j = 0; j < _outputs.Length; ++j)
                _outputs[j].Evaluate();
            
            // Debug.Log("Validated " + i + ":" + _output.Evaluate());

            yield return new WaitForSeconds(1f);

            for (int j = 0; j < _outputs.Length; ++j)
                _outputs[j].ResetSignalRecursive();
        }
    }

    public override string GetDescription(InteractionContext _)
    {
        return "[ЛКМ] Использовать";
    }
}
