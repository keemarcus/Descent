using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text itemCountText;


    private void Awake()
    {
        itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        if (item != null)
        {
            //itemNameText.text = item.name;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        itemNameText = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        if (item != null)
        {
            itemNameText.text = item.name;
        }
        else{
            itemNameText.text = null;
        }
        
        itemCountText = gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        //itemCountText.text = itemCount.ToString();
        itemCountText.text = null;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemIcon.sprite = item.icon;
        itemIcon.enabled = true;
        itemNameText.text = item.name;

        // handle quantity later
    }
}
