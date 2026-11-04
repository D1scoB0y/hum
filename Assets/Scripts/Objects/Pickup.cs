using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField] private Item _item;

    public override void Interact(InteractionContext ctx)
    {
        ctx.Inventory.Add(_item);
        Destroy(gameObject);
    }

    public override string GetDescription(InteractionContext ctx)
    {
        return "[ЛКМ] Взять";
    }
}
