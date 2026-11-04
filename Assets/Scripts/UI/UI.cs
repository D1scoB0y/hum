using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Inventory), typeof (Interaction), typeof (Placement))]
public class UI : MonoBehaviour
{
    [SerializeField] private SlotUI[] _slotsUIs;
    [SerializeField] private Image _key;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private TextMeshProUGUI _selectedSlotText;
    [SerializeField] private FocusFrame _focusFrame;
    [SerializeField] private TextMeshProUGUI _stageTitleText;
    [SerializeField] private TextMeshProUGUI _stageDescriptionText;

    private Inventory _inventory;
    private Interaction _interaction;
    private Placement _placement;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _interaction = GetComponent<Interaction>();
        _placement = GetComponent<Placement>();
    }

    private void OnEnable()
    {
        _inventory.OnChanged += UpdateUI;
        _interaction.OnTargetChanged += UpdateUI;
        _placement.OnSlotChanged += UpdateUI;

        ObjectiveManager.Instance.OnObjectiveChanged += UpdateObjectiveUI;
    }

    private void OnDisable()
    {
        _inventory.OnChanged -= UpdateUI;
        _interaction.OnTargetChanged -= UpdateUI;
        _placement.OnSlotChanged -= UpdateUI;

        ObjectiveManager.Instance.OnObjectiveChanged -= UpdateObjectiveUI;
    }

    private void Start()
    {
        _focusFrame.Hide();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_key != null)
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
    }

    private void UpdateObjectiveUI()
    {
        _stageTitleText.text = ObjectiveManager.Instance.CurrentObjective.Title;
        _stageDescriptionText.text = ObjectiveManager.Instance.CurrentObjective.Description;
    }

    private void Update()
    {
        if (_interaction.Target != null)
        {
            _focusFrame.Show(_interaction.Target.GetBounds());
        } else
        {
            _focusFrame.Hide();
        }
    }
}
