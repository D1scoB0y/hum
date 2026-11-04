public class Key : Interactable
{
    public override void Interact(InteractionContext ctx)
    {
        ctx.Inventory.AddKey();
        Destroy(gameObject);

        if (ObjectiveManager.Instance.CurrentObjective.Objective_ == Objective.EnterTheTransformerSubstation1)
        {
            ObjectiveManager.Instance.SetObjective(Objective.EnterTheTransformerSubstation2);
        }
    }

    public override string GetDescription(InteractionContext _)
    {
        return "[ЛКМ] Взять";
    }
}
