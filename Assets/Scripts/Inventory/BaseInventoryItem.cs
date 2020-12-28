using UnityEngine;

/*
 * NOTE!
 * That class could be an "abstract" class from which you inherit to
 * different types of Items like -> Armor, Equippable, Consumable, Usable etc..
 * that is just a "basic" view of what you can do
 */

[CreateAssetMenu(fileName = "BaseItem", menuName = "TDSGamer/Inventory/BaseItem")]
public class BaseInventoryItem : ScriptableObject
{
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    [SerializeField] private int amount;
    public int Amount => amount;
    [SerializeField] private string description;
    public string Description => description;

    [SerializeField] private bool isStackable;
    public bool IsStackable => isStackable;
}