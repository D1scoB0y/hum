using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _requireKey;

    private readonly int _hashedIsOpenAnimatorParam = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _animator.SetBool(_hashedIsOpenAnimatorParam, _isOpen);
    }

    public void Interact(InteractionContext ctx)
    {
        if (_requireKey && !ctx.Inventory.HasKey()) return;

        _isOpen = !_isOpen;
        _animator.SetBool(_hashedIsOpenAnimatorParam, _isOpen);
    }

    public string GetDescription(InteractionContext ctx)
    {
        if (_requireKey && !ctx.Inventory.HasKey())
        {
            return "Требуется ключ";
        }

        if (_isOpen)
        {
            return "[ЛКМ] Закрыть дверь";
        }

        return "[ЛКМ] Открыть дверь";
    }
}
