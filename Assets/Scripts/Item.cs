using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemModifiedException : System.Exception
{
    public ItemModifiedException(string message) : base(message)
    {

    }
}

//Attribute that allows us to right click->create
[CreateAssetMenu(fileName = "NewItem", menuName = "ItemSystem/Item")]
public class Item : ScriptableObject
{
    public string name = "item";
    public string description = "this is an item";
    public string category = "misc";
    public int maxStack = 64;
    public Sprite icon;
    public bool isConsumable = true;


    private int id = -1;

    public int Id
    {
        get { return id; }
        set
        {
            id = value;
            throw new ItemModifiedException("ID was modified! for item: " + name);
        }
    }

    //returns whether or not the Item was successfully used
    public bool Use()
    {
        Debug.Log("Used item: " + name);
        return true;
    }
}