using UnityEngine;

public class ElectricalSwitch : Interactable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Light[] _lights;
    [SerializeField] private Light _redLight;

    private readonly int _hashedIsOnAnimatorParam = Animator.StringToHash("isOn");

    private bool isOn = false;

    public override void Interact(InteractionContext _)
    {
        if (!isOn)
        {
            isOn = true;
            _animator.SetBool(_hashedIsOnAnimatorParam, true);

            foreach (Light l in _lights)
            {
                l.enabled = true;
            }

            _redLight.enabled = false;
        }
    }

    public override string GetDescription(InteractionContext _)
    {
        if (!isOn)
        {
            return "[ЛКМ] Активировать";
        }

        return "";
    }
}
