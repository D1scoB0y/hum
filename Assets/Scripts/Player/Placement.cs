using System;
using UnityEngine;
using UnityEngine.InputSystem;
using KbKey = UnityEngine.InputSystem.Key;

public class Placement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LayerMask _placementMask;
    [SerializeField] private float _ghostDistance = 2.5f;
    [SerializeField] private float _gridStep = 0.1f;
    [SerializeField] private Material _allowedLocationMaterial;
    [SerializeField] private Material _disallowedLocationMaterial;
    [SerializeField] private GameObject _grid;
    [SerializeField] private Interaction _interaction;

    private GameObject _ghost;
    private Renderer _ghostRenderer;
    private int _selectedSlot = -1;
    private bool _snapped;
    private InputAction _placeAction;

    public int SelectedSlot => _selectedSlot;
    public event Action OnSlotChanged;

    private static readonly KbKey[] SlotKeys = { KbKey.Digit1, KbKey.Digit2, KbKey.Digit3 };

    private void Awake()
    {
        _placeAction = _playerInput.actions.FindAction("Interact");
        _grid.SetActive(false);
    }

    private void OnEnable()
    {
        _placeAction.Enable();
        _placeAction.performed += OnPlace;
    }

    private void OnDisable()
    {
        _placeAction.performed -= OnPlace;
        _placeAction.Disable();
    }

    private void Update()
    {
        for (int i = 0; i < SlotKeys.Length; i++)
        {
            if (Keyboard.current[SlotKeys[i]].wasPressedThisFrame)
            {
                SelectSlot(i);
                break;
            }
        }

        UpdateGhostPosition();
    }

    private void SelectSlot(int index)
    {
        if (index >= _inventory.Slots.Length)
            return;

        if (_selectedSlot == index)
        {
            ClearSelection();
            return;
        }

        var slot = _inventory.Slots[index];
        if (slot == null || slot.count <= 0 || slot.item == null || slot.item.ghostPrefab == null)
            return;

        DestroyGhost();

        _ghost = Instantiate(slot.item.ghostPrefab);
        _ghostRenderer = _ghost.GetComponent<Renderer>();
        _selectedSlot = index;
        _grid.SetActive(true);
        _interaction.enabled = false;
        OnSlotChanged?.Invoke();
    }

    private void UpdateGhostPosition()
    {
        _snapped = false;

        if (_ghost == null)
            return;

        Transform cam = _camera.transform;
        Vector3 position = cam.position + cam.forward * _ghostDistance;
        Quaternion rotation = Quaternion.Euler(0f, cam.eulerAngles.y + 90f, -90f);
        Material mat = _disallowedLocationMaterial;

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, _ghostDistance, _placementMask))
        {
            position = hit.point;
            position.x = Mathf.Round(position.x / _gridStep) * _gridStep;
            position.y = Mathf.Round(position.y / _gridStep) * _gridStep;

            rotation = Quaternion.LookRotation(-hit.normal) * Quaternion.Euler(0f, 90f, -90f);
            mat = _allowedLocationMaterial;
            _snapped = true;
        }

        _ghost.transform.SetPositionAndRotation(position, rotation);

        if (_ghostRenderer != null)
            _ghostRenderer.material = mat;
    }

    private void OnPlace(InputAction.CallbackContext _)
    {
        if (!_snapped || _selectedSlot < 0)
            return;

        var slot = _inventory.Slots[_selectedSlot];
        if (slot == null || slot.item == null)
            return;

        GameObject prefab = slot.item.prefab;
        if (prefab == null)
            return;

        if (!_inventory.Consume(_selectedSlot))
            return;

        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetPositionAndRotation(_ghost.transform.position, _ghost.transform.rotation);
        newObj.GetComponent<Node>().SetPlaced();

        if (_inventory.Slots[_selectedSlot].count <= 0)
            ClearSelection();
    }

    private void ClearSelection()
    {
        _selectedSlot = -1;
        _snapped = false;
        _grid.SetActive(false);
        DestroyGhost();
        _interaction.enabled = true;
        OnSlotChanged?.Invoke();
    }

    private void DestroyGhost()
    {
        if (_ghost != null)
            Destroy(_ghost);

        _ghost = null;
        _ghostRenderer = null;
    }
}
