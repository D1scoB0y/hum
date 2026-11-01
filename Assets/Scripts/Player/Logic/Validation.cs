using System.Collections;
using UnityEngine;

public class Validation : MonoBehaviour
{
    [SerializeField] private Output[] _inputs;
    [SerializeField] private Input _output;

    private void Start()
    {
        foreach (var input in _inputs)
        {
            input.SetVisible();
        }

        _output.SetVisible();
    }

    public void Test()
    {
        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        foreach (var input in _inputs)
        {
            input.SetSignal(SignalType.Low);
        }

        Debug.Log("Validated 1: " + _output.Evaluate());

        yield return new WaitForSeconds(3f);

        _output.ResetSignalRecursive();

        foreach (var input in _inputs)
        {
            input.SetSignal(SignalType.High);
        }

        Debug.Log("Validated 2: " + _output.Evaluate());
    }
}
