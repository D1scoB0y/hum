using System;
using UnityEngine;

public enum PinType { In, Out }

public enum SignalType { None, Low, High }

public abstract class Pin : MonoBehaviour, IInteractable
{
    public SignalType Signal { get; private set; } = SignalType.None;
    public bool Selected { get; private set; } = false;
    public bool Visible { get; private set; } = false;

    public static event Action<Pin> OnClicked;
    public event Action OnChanged;

    public abstract PinType GetPinType();

    public void Interact(InteractionContext _)
    {
        OnClicked?.Invoke(this);
    }

    public string GetDescription(InteractionContext _)
    {
        return "[ЛКМ] Проложить";
    }

    public void SetSignal(SignalType signal)
    {
        Signal = signal;
        OnChanged?.Invoke();
    }

    public void ResetSignal()
    {
        Signal = SignalType.None;
        OnChanged?.Invoke();
    }

    public void SetSelected()
    {
        Selected = true;
        OnChanged?.Invoke();
    }

    public void ResetSelected()
    {
        Selected = false;
        OnChanged?.Invoke();
    }

    public void SetVisible()
    {
        Visible = true;
        OnChanged?.Invoke();
    }
}
