using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Dictionary<string, int> itemList = new Dictionary<string, int>();
    int listLength = 0;
    

    public void start() { //starts new inventory
        itemList.Add( "Basic_Sword", 0 );
        itemList.Add( "Heavy_Sword", 0 );
        itemList.Add( "Pistol", 0 );
        itemList.Add( "Axe", 0 );
        itemList.Add( "Magic_Boots", 0 );
        itemList.Add( "Food", 0 );
        itemList.Add( "Coin", 0 );
        itemList.Add( "Light Armor", 0 );
        itemList.Add( "Heavy Armor", 0 );
        Debug.Log("Started inventory");
    }

    public void printInventory() {
        Debug.Log("Item    Quantity");
        foreach (KeyValuePair<string, int> kvp in itemList) {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }
    }

    public void getItem(string item) {
        itemList[item]++;
    }

    public void dropItem(){
        itemList[item]--;
    }
}
