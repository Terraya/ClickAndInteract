using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //To create initial Slots, set the List Length to a certain Number in the Inspector which suits you
    [SerializeField] private List<InventorySlot> slots;
    public List<InventorySlot> Slots => slots;

    //Event which get initialized whenever a new Item gets added or removed from the Inventory
    public event Action OnInventoryUpdate;

    //Function to get Player Inventory
    public static Inventory GetPlayerInventory()
        => GameObject.FindWithTag("Player").GetComponent<Inventory>();

    public bool HasSpaceFor(BaseInventoryItem item)
    {
        return FindSlot(item) >= 0;
    }

    public int GetSize()
    {
        return slots.Count;
    }

    public bool AddToFirstEmptySlot(BaseInventoryItem item, int number)
    {
        var i = FindSlot(item);

        if (i < 0)
        {
            return false;
        }

        slots[i].item = item;
        slots[i].amount += number;
        OnInventoryUpdate?.Invoke();

        return true;
    }

    public bool HasItem(BaseInventoryItem item)
        => slots.Any(slot => slot.item == item);


    public BaseInventoryItem GetItemInSlot(int slot)
    {
        return slots[slot].item;
    }

    public int GetNumberInSlot(int slot)
    {
        return slots[slot].amount;
    }

    public void RemoveFromSlot(int slot, int number)
    {
        slots[slot].amount -= number;
        if (slots[slot].amount <= 0)
        {
            slots[slot].amount = 0;
            slots[slot].item = null;
        }

        OnInventoryUpdate?.Invoke();
    }

    public bool AddItemToSlot(int slot, BaseInventoryItem item, int number)
    {
        if (slots[slot].item != null)
        {
            return AddToFirstEmptySlot(item, number);
        }

        var i = FindStack(item);
        if (i >= 0)
        {
            slot = i;
        }

        slots[slot].item = item;
        slots[slot].amount += number;
        OnInventoryUpdate?.Invoke();

        return true;
    }

    private int FindSlot(BaseInventoryItem item)
    {
        var i = FindStack(item);
        if (i < 0)
        {
            i = FindEmptySlot();
        }

        return i;
    }

    private int FindEmptySlot()
    {
        for (var i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return i;
            }
        }

        return -1;
    }

    private int FindStack(BaseInventoryItem item)
    {
        if (!item.IsStackable)
        {
            return -1;
        }

        for (var i = 0; i < slots.Count; i++)
        {
            if (ReferenceEquals(slots[i].item, item))
            {
                return i;
            }
        }

        return -1;
    }
}