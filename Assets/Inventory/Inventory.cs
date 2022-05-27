using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public string name;
    public string description;
    public int count;
    
    
}


public class Inventory
{
    public List<Item> itemList = new List<Item>();

    public void start() { //starts new inventory
        itemList.Add( new Item {name= "Basic_Sword", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Heavy_Sword", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Pistol", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Axe", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Magic_Boots", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Food", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Coin", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Light Armor", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Heavy Armor", description= "Sword!", count= 0} );
        itemList.Add( new Item {name= "Tennis Shoes", description= "Sword!", count= 0} );
        Debug.Log("Started inventory");
    }

    public void printInventory() {
        //Debug.Log("Item / Quantity");
        foreach (Item a in itemList) {
            Debug.Log(a.name + " " + a.count);
            //Debug.Log(a.description);
        }
    }

    public void addItem(string item) {
        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].name == item) {
                itemList[i].count++;
                Debug.Log("Added Item");
                return;
            }
        }
        Debug.Log("Couldn't find item to add.");
    }

    public void dropItem(string item){
        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].name == item) {
                itemList[i].count--;
                Debug.Log("Dropped Item");
                return;
            }
        }
    }

    public int getCount(string item) {
        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].name == item) {
                return itemList[i].count;
            }
        }
    }

    public string getDescription(string item) {
        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].name == item) {
                return itemList[i].description;
            }
        }
    }
        Debug.Log("Couldn't find item to drop.");
    }
}
