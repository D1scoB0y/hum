using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact(InteractionContext ctx);
    string GetDescription(InteractionContext ctx);
}

public class InteractionContext
{
    public Inventory Inventory { get; }

    public InteractionContext(Inventory inventory)
    {
        Inventory = inventory;
    }
}

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _interactionDistance = 1.5f;
    [SerializeField] private LayerMask _interactionMask;

    private InputAction _interactAction;
    public IInteractable Target { get; private set; }
    public InteractionContext Context { get; private set; }

    public event Action OnTargetChanged;

    private void Awake()
    {
        _interactAction = _playerInput.actions.FindAction("Interact", true);
        Context = new InteractionContext(_playerInventory);
    }

    private void OnEnable()
    {
        _interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        _interactAction.performed -= OnInteract;
    }

    private void Update()
    {
        var target = FindTarget();

        if (!ReferenceEquals(target, Target))
            SetTarget(target);
    }

    private IInteractable FindTarget()
    {
        Ray ray = _playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (!Physics.Raycast(
                ray, out RaycastHit hit, _interactionDistance, _interactionMask))
            return null;

        return hit.collider.GetComponentInParent<IInteractable>();
    }

    private void SetTarget(IInteractable target)
    {
        Target = target;
        OnTargetChanged?.Invoke();
    }

    private void OnInteract(InputAction.CallbackContext _)
    {
        Target?.Interact(Context);
    }
}
