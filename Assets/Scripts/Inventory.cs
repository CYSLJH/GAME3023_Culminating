using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    string playerPrefsKey = "";

    [SerializeField]
    ItemTable itemTable;

    [SerializeField]
    GameObject inventoryPanel;

    List<ItemSlot> itemSlots = new List<ItemSlot>();

    // Start is called before the first frame update
    void Start()
    {
        playerPrefsKey = gameObject.name + "Inventory";

        itemTable.AssignItemIDs();
        GameSaver.OnSaveEvent.AddListener(SaveInventory);
        GameSaver.OnLoadEvent.AddListener(LoadInventory);

        //read all itemSlots
        int numItemSlots = inventoryPanel.transform.childCount;
        for(int i = 0; i < numItemSlots; i++)
        {
            itemSlots.Add(inventoryPanel.transform.GetChild(i).GetComponent<ItemSlot>());
        }
    }

    //go through all itemSlots, if they have an item, save their ID and count using PlayerPrefs
    void SaveInventory()
    {
        //pack all items and counts into one long string
        //ItemID1, ItemCount1, ItemID2, ItemCount2...
        string inventorySaveString = "";

        //Foreach item slot, encode it into two values to append to inventorySaveString
        for (int i = 0; i < itemSlots.Count; i++)
        {
            string id = "-1"; //-1 means no item
            string count = "0";

            ItemSlot slot = itemSlots[i];
            if(slot.itemInSlot != null)
            {
                id = slot.itemInSlot.Id.ToString();
                count = slot.ItemCount.ToString();
            }

            //append to the string with our new info
            inventorySaveString += id + "," + count + ",";
        }

        PlayerPrefs.SetString(playerPrefsKey, inventorySaveString);
        Debug.Log("Inventory Saved");
    }

    //go through all itemSlots, read their ID and count using PlayerPrefs
    void LoadInventory()
    {
        if(!PlayerPrefs.HasKey(playerPrefsKey))
        {
            Debug.Log("No save data?");
            return;
        }

        string loadedString = PlayerPrefs.GetString(playerPrefsKey, "");
        //break the string down into pairs

        //parse string into ints
        char[] delimiters = { ',' };
        string[] itemDataStrings = loadedString.Split(delimiters);

        for (int i = 0; i < itemSlots.Count; i++)
        {

            int id = int.Parse(itemDataStrings[(2* i) + 0]);
            int count = int.Parse(itemDataStrings[(2 * i) + 1]);

            if(id >= 0)
            {
                itemSlots[i].itemInSlot = itemTable.GetItemFromID(id);
                itemSlots[i].ItemCount = count;
            }
            else
            {
                itemSlots[i].itemInSlot = null;
                itemSlots[i].ItemCount = 0;
            }
        }


        Debug.Log(loadedString);
        Debug.Log("Inventory Loaded");
    }
}
