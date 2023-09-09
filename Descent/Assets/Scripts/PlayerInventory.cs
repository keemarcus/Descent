using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSlot> inventorySlots;
    public PlayerManager playerManager;
    public int activeSlot;

    private void Awake()
    {
        /*ItemSlot[] itemSlots = FindObjectsOfType<ItemSlot>();
        for(int i = 0; i < itemSlots.Length; i++)
        {
            inventorySlots.Add(itemSlots[i]);
            inventorySlots.Reverse();
        }*/

        playerManager = GetComponent<PlayerManager>();
        activeSlot = 0;
    }

    public void PickUpItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == null)
            {
                Debug.Log("add at " +  i);
                inventorySlots[i].AddItem(item);
                return;
            }
        }
    }

    public bool CheckForOpenSlot()
    {
        foreach (var slot in inventorySlots)
        {
            if(slot.item == null) { return true; };
        }
        return false;
    }

    public void SelectItem(int i)
    {
        playerManager.heldItem = null;
        activeSlot += i;
        if(activeSlot >= inventorySlots.Count) { activeSlot = 0; }
        else if(activeSlot < 0) { activeSlot = inventorySlots.Count - 1; }

        Item item = inventorySlots[activeSlot].item;
        if ( item != null) { 
            playerManager.heldItem = item; 
            item.gameObject.SetActive(true);
        }
    }
}
