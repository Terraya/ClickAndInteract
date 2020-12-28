using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //InventorySlotUI Prefab which gets instantiated foreach slot 
    [SerializeField] private InventorySlotUI inventoryItemPrefab = null;
    
    private Inventory _inventory;
    
    private void Awake()
    {
        SetInventory(Inventory.GetPlayerInventory());
        _inventory.OnInventoryUpdate += Redraw;
    }

    private void Start()
        => Redraw();
    
    public void SetInventory(Inventory inventory)
        => _inventory = inventory;

    public virtual void Redraw()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (var i = 0; i < _inventory.GetSize(); i++)
        {
            var itemUi = Instantiate(inventoryItemPrefab, transform);
            itemUi.Setup(_inventory, i);
        }
    }
}