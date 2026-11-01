using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _count;

    public void UpdateUI(Slot slot)
    {
        _image.sprite = slot.IsEmpty ? null : slot.item.icon;
        _image.enabled = !slot.IsEmpty;

        _count.text = slot.count < 2 ? null : slot.count.ToString();
        _count.enabled = slot.count > 1;
    }
}
