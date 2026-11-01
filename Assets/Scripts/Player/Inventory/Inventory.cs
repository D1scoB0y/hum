using System;
using UnityEngine;

[Serializable]
public class Slot
{
    public Item item;
    public int count;

    public bool IsEmpty => item == null || count <= 0;
}

public class Inventory : MonoBehaviour
{
    private readonly Slot[] _slots = new Slot[5];
    private bool _hasKey = false;

    public event Action OnChanged;

    public Slot[] Slots => _slots;

    private void Awake()
    {
        for (int i = 0; i < _slots.Length; ++i)
        {
            _slots[i] ??= new Slot();
        }
    }

    public void Add(Item item)
    {
        if (item == null) return;

        for (int i = 0; i < _slots.Length; ++i)
        {
            if (_slots[i].item == item)
            {
                _slots[i].count++;
                OnChanged?.Invoke();
                return;
            }
        }

        for (int i = 0; i < _slots.Length; ++i)
        {
            if (_slots[i].IsEmpty)
            {
                _slots[i].item = item;
                _slots[i].count = 1;
                OnChanged?.Invoke();
                return;
            }
        }
    }

    public bool Consume(int i)
    {
        if (i >= _slots.Length || _slots[i].count == 0) return false;
        _slots[i].count--;
        OnChanged?.Invoke();
        return true;
    }

    public void AddKey()
    {
        _hasKey = true;
        OnChanged?.Invoke();
    }

    public bool HasKey()
    {
        return _hasKey;
    }
}
