using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSlot> inventorySlots;
    public PlayerManager playerManager;
    public int activeSlot;
    public GameObject activeSlotIndicator;

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
        activeSlotIndicator.SetActive(false);
    }

    public void PickUpItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == null)
            {
                Debug.Log("add at " +  i);
                inventorySlots[i].AddItem(item);
                if(CheckForNumberOfItemsInInventory() == 1) 
                { 
                    activeSlot = i; 
                    SelectItem(0); 
                }
                return;
            }
        }
    }

    public void DropItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item)
            {
                Debug.Log("remove at " + i);
                inventorySlots[i].RemoveItem();
                item.Drop();
                activeSlotIndicator.SetActive(false);
                CollapseInventory();
                if(i == inventorySlots.Count - 1 || i == CheckForNumberOfItemsInInventory()) {SelectItem(-1);}
                else {SelectItem(0);}
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

    public bool CheckForItemInInventory(Item item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item == item) { return true; };
        }
        return false;
    }

    public int CheckForNumberOfItemsInInventory()
    {
        int number = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null) { number++; };
        }
        return number;
    }

    public void CollapseInventory()
    {
        //Item moveItem;
        for(int i = inventorySlots.Count - 1; i > 0; i--)
        {
            if ((inventorySlots[i].item != null) && inventorySlots[i - 1].item == null)
            {
                inventorySlots[i - 1].AddItem(inventorySlots[i].item);
                inventorySlots[i].RemoveItem();
                if(i < inventorySlots.Count - 1) { i = inventorySlots.Count; }
            }
        }
    }

    public void SelectItem(int i)
    {
        do
        {
            activeSlot += i;
            if (activeSlot >= inventorySlots.Count) { activeSlot = 0; }
            else if (activeSlot < 0) { activeSlot = inventorySlots.Count - 1; }
        } while (inventorySlots[activeSlot].item == null && playerManager.heldItem != null && inventorySlots[activeSlot].item != playerManager.heldItem);

        activeSlotIndicator.SetActive(false);
        if (playerManager.heldItem != null)
        {
            playerManager.heldItem.gameObject.SetActive(false);
            playerManager.heldItem = null;
        }

        Item item = inventorySlots[activeSlot].item;
        if ( item != null) { 
            playerManager.heldItem = item; 
            item.gameObject.SetActive(true);
            activeSlotIndicator.transform.position = inventorySlots[activeSlot].transform.position;
            activeSlotIndicator.SetActive(true);
        }
        else
        {

        }
    }
}
