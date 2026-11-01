public class Input : Pin
{
    public Wire IncomingWire { get; private set; }

    public override PinType GetPinType() => PinType.In;

    public void SetIncomingWire(Wire wire)
    {
        if (IncomingWire != null && IncomingWire != wire)
        {
            IncomingWire.DestroyWire();
        }

        IncomingWire = wire;
        ResetSignal();
    }

    public void ClearIncomingWire()
    {
        IncomingWire = null;
        ResetSignal();
    }

    public SignalType Evaluate()
    {
        if (Signal == SignalType.None && IncomingWire != null)
        {
            SetSignal(IncomingWire.Source.Evaluate());
        }
        return Signal;
    }

    public void ResetSignalRecursive()
    {
        ResetSignal();

        if (IncomingWire != null && IncomingWire.Source != null)
        {
            IncomingWire.Source.ResetSignalRecursive();
        }
    }
}
