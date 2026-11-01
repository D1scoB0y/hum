using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact(InteractionContext ctx)
    {
        ctx.Inventory.AddKey();
        Destroy(gameObject);
    }

    public string GetDescription(InteractionContext _)
    {
        return "[ЛКМ] Взять";
    }
}
