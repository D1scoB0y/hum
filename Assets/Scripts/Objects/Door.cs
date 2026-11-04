using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isOpen;

    private readonly int _hashedIsOpenAnimatorParam = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _animator.SetBool(_hashedIsOpenAnimatorParam, _isOpen);
    }

    public override void Interact(InteractionContext ctx)
    {
        _isOpen = !_isOpen;
        _animator.SetBool(_hashedIsOpenAnimatorParam, _isOpen);
    }

    public override string GetDescription(InteractionContext ctx)
    {
        if (_isOpen) return "[ЛКМ] Закрыть";
        return "[ЛКМ] Открыть";
    }
}
