using System.Collections;
using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField] private BaseInventoryItem item = default;
    
    public override IEnumerator StartInteractWithPlayer()
    {
        //Add to Inventory or just Delete and do anything
        yield return new WaitForSeconds(0.3f);
        Inventory.GetPlayerInventory().AddToFirstEmptySlot(item, item.Amount);
        Destroy(gameObject);
    }

    public override IEnumerator StartInteractWithAI()
    {
        //No implementation yet
        throw new System.NotImplementedException();
    }

    public override IEnumerator StopInteractWithPlayer()
    {
        print("Pickable is already destroyed, this will not get called");
        yield break;
    }

    public override IEnumerator StopInteractWithAI()
    {
        //No Implementation yet
        throw new System.NotImplementedException();
    }
}