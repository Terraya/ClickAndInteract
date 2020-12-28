using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] InventoryItemIcon icon = null;

    private int _index; //position index in the array
    private BaseInventoryItem _item;
    private Inventory _inventory;

    public void Setup(Inventory inventory, int index)
    {
        _inventory = inventory;
        _index = index;
        icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
    }

    public int GetNumber()
        => _inventory.GetNumberInSlot(_index);
    
    public void RemoveItems(int number)
        => _inventory.RemoveFromSlot(_index, number);
    
    public BaseInventoryItem GetItem()
        => _inventory.GetItemInSlot(_index);
    
    public int MaxAcceptable(BaseInventoryItem item)
        => _inventory.HasSpaceFor(item) 
            ? int.MaxValue 
            : 0;
    
    public void AddItems(BaseInventoryItem item, int number)
        => _inventory.AddItemToSlot(_index, item, number);
}