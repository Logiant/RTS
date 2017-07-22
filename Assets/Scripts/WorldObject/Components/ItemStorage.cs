using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage  {

    List<Item> items;

    public ItemStorage() {
        items = new List<Item>();
    }

    //TODO RequestItem() -- increments demand by 1, Take decrements demand by 1, CancelOrder() decrements command by 1
    //if demmand <=0, turn crafting job on!

    //TODO
    //getTool(enum type);
    //getWeapon(enum type);
    public void Add(Item i) {
        items.Add(i);
    }

    public Item Take() {
        Item i = null;
        if (items.Count > 0) {
            i = items[0];
            items.RemoveAt(0);
        }
        return i;
    }

    public bool HasItems() {
        return items.Count > 0;
    }

    public int GetNumTools() {
        return items.Count;
    }
}
