using System;
using UnityEngine;

[RequireComponent(typeof (Renderer))]
public abstract class Interactable : MonoBehaviour
{
    private Renderer _renderer;

    public event Action<InteractionContext> OnInteracted;

    public virtual Bounds GetBounds()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        return _renderer.bounds;
    }

    public virtual void Interact(InteractionContext ctx)
    {
        OnInteracted?.Invoke(ctx);
    }

    public abstract string GetDescription(InteractionContext ctx);
}
