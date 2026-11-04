using System;
using TMPro;

public enum PinType { In, Out }

public enum SignalType { None, Low, High }

public abstract class Pin : Interactable
{
    public SignalType Signal { get; private set; } = SignalType.None;
    public bool Selected { get; private set; } = false;
    public bool Visible { get; private set; } = false;
    public bool Connected { get; private set; } = false;

    public static event Action<Pin> OnClicked;
    public event Action OnChanged;
    public event Action OnDestroyed;

    public abstract PinType GetPinType();

    public override void Interact(InteractionContext _)
    {
        OnClicked?.Invoke(this);
    }

    public override string GetDescription(InteractionContext _)
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

    public void SetConnected()
    {
        Connected = true;
        OnChanged?.Invoke();
    }

    public void ResetConnected()
    {
        Connected = false;
        OnChanged?.Invoke();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
