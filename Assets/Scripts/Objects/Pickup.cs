using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item;

    public void Interact(InteractionContext ctx)
    {
        ctx.Inventory.Add(_item);
        Destroy(gameObject);
    }

    public string GetDescription(InteractionContext ctx)
    {
        return "[ЛКМ] Взять";
    }
}
