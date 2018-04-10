using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    void Awake() {
        if(instance != null)
        {
            Debug.Log("Found more than one instance of inventory");
            return;
        }
        instance = this;
    }


    public List<Item> items = new List<Item>();

    public void addItem(Item item)
    {
        items.Add(item);
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
    }
}
