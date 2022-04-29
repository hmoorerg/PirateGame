using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//health 
//damage
//speed


public class Item 
{
    public enum ItemType  //All items in the game that can go into inventory.
    {
        Grappling_Hook, //4 damage melee, short range rappling hook
        Heavy_Axe, // 22 damage slow melee, high damage
        Pirate_Sword, // 8 Fast Melee
        Pistol, // Ranged attack slow moving bullet
        Health_Potion,
        Key,
        Coin,
        Magical_Boots, // + 10 speed , can double jump
        Sandals, // + 10 damage 
        Tennis_Shoes, //  + 10 speed , can dash
        Helmet,
        Pirate_Cap,
        Metal_Armor, // -5 speed, +10 health
        Cloak, // +15 speed, 
        Light_Armor, // +5 armor
        Captain_Coat, // + 10 damage
    }

    public ItemType itemType;
    public int amount;
}
