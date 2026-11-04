using System;
using UnityEngine;

public class TransformerDoor : Interactable
{
    [SerializeField] private Animator _animator;

    private bool _triedToOpen = false;
    private bool _isOpen = false;

    public override void Interact(InteractionContext ctx)
    {
        if (!_triedToOpen && !ctx.Inventory.HasKey())
        {
            _triedToOpen = true;
            ObjectiveManager.Instance.SetObjective(Objective.EnterTheTransformerSubstation1);
            return;
        }

        if (ctx.Inventory.HasKey())
        {
            _isOpen = !_isOpen;
            _animator.SetBool("isOpen", _isOpen);
            ObjectiveManager.Instance.SetObjective(Objective.RestartTheMainPowerSource);
        }
    }

    public override string GetDescription(InteractionContext ctx)
    {
        if (!ctx.Inventory.HasKey() && _triedToOpen)
        {
            return "Заперто";
        }

        if (_isOpen)
        {
            return "[ЛКМ] Закрыть";
        }

        return "[ЛКМ] Открыть";
    }
}
