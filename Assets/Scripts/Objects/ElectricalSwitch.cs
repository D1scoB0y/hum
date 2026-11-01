using UnityEngine;

public class ElectricalSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Light[] _lights;
    [SerializeField] private Light _redLight;

    [SerializeField] private Validation _validation;

    private readonly int _hashedIsOnAnimatorParam = Animator.StringToHash("isOn");

    private bool isOn = false;

    public void Interact(InteractionContext _)
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

            _validation.Test();
        }
    }

    public string GetDescription(InteractionContext _)
    {
        if (!isOn)
        {
            return "[ЛКМ] Активировать";
        }

        return "";
    }
}
