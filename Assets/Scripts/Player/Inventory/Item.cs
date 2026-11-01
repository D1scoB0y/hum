using UnityEngine;

public enum ItemType
{
    LogicalGateOR,
    LogicalGateXOR,
    LogicalGateAND,
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public GameObject prefab;
    public GameObject ghostPrefab;
    public ItemType type;

    public string Name()
    {
        return type switch {
            ItemType.LogicalGateOR => "Логическое ИЛИ",
            ItemType.LogicalGateXOR => "Логическое исключающее ИЛИ",
            ItemType.LogicalGateAND => "Логическое И",
            _ => "Unknown",
        };
    }
}
