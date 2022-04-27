using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // Start is called before the first frame update
    private List<Item> itemList;

    public Inventory() {
        itemList = new List<Item>();
        addItem(new Item {itemType = Item.ItemType.Pirate_Sword, amount = 1} );
        Debug.Log("hit");
    }

    public void addItem(Item item) {
        itemList.Add(item);
    }
    
}
