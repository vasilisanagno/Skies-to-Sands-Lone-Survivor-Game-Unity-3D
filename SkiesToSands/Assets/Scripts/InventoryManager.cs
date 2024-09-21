using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager Instance;
    public List<Item> items  = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject health;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Add(Item item){
        items.Add(item);
    }

    public void Remove(Item item){
        items.Remove(item);
    }

    public void ListItems(){
        foreach(Transform item in ItemContent){
            Destroy(item.gameObject);
        }
        foreach(var item in items){
            Debug.Log(item.itemName);
                GameObject newObj = Instantiate(InventoryItem, ItemContent);

                // Assign the correct Item reference to the InventoryItemAction component
                var inventoryItemAction = newObj.GetComponent<InventoryItemAction>();
                if (inventoryItemAction != null)
                {
                    inventoryItemAction.item = item;  // Set the specific item for this instance
                    Debug.Log("created" + item.itemName);
                }
                else
                {
                    Debug.LogError("InventoryItemAction component not found on instantiated object.");
                }
            //GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = newObj.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            var itemIcon = newObj.transform.Find("Image")?.GetComponent<Image>();

            foreach (Transform child in newObj.transform)
            {
                Debug.Log("Child name: " + child.name);
            }

            if (itemName != null)
                itemName.text = item.itemName;

            if (itemIcon != null)
                itemIcon.sprite = item.icon;
        }
    }
}
