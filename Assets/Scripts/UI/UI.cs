using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Interaction _interaction;
    [SerializeField] private Placement _placement;
    [SerializeField] private SlotUI[] _slotsUIs;
    [SerializeField] private Image _key;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private TextMeshProUGUI _selectedSlotText;
    [SerializeField] private FocusFrame _focusFrame;

    private Bounds _interactionTargetBounds;

    private void OnEnable()
    {
        _inventory.OnChanged += UpdateUI;
        _interaction.OnTargetChanged += UpdateUI;
        _placement.OnSlotChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _inventory.OnChanged -= UpdateUI;
        _interaction.OnTargetChanged -= UpdateUI;
        _placement.OnSlotChanged -= UpdateUI;
    }

    private void Start()
    {
        _focusFrame.Hide();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _key.enabled = _inventory.HasKey();

        for (int i = 0; i < _inventory.Slots.Length; ++i)
        {
            _slotsUIs[i].UpdateUI(_inventory.Slots[i]);
        }

        _interactionText.text = _interaction.Target != null
            ? _interaction.Target.GetDescription(_interaction.Context)
            : string.Empty;

        if (_placement.SelectedSlot == -1)
        {
            _selectedSlotText.text = string.Empty;
        } else
        {
            _selectedSlotText.text = _inventory.Slots[_placement.SelectedSlot].item.Name();
        }

        if (_interaction.Target is Component component)
        {
            _interactionTargetBounds = component.GetComponent<Renderer>().bounds;
        }
    }

    private void Update()
    {
        if (_interaction.Target != null)
        {
            _focusFrame.Show(_interactionTargetBounds);
        } else
        {
            _focusFrame.Hide();
        }
    }
}
