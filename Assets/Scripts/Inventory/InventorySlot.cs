using System;
 
 [Serializable]
 public class InventorySlot
 {
     //Inventory Item the Slot is containing
     public BaseInventoryItem item = default;
 
     //The Amount of how many items of this count we have
     public int amount = default;
 }