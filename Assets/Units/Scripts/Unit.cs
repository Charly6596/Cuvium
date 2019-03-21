using UnityEngine;
using Cuvium.Entities;

[CreateAssetMenu(menuName = "Cuvium/Unit")]
public class Unit : MapObject
{
    public int Experience;
    public int Energy;
    public int Speed;
    public bool AI = false;
    public bool NeedsFood = true;
    public Texture2D Icon;
    public Item[] Inventory = new Item[4];

    void OnValidate()
    {
        if(Inventory.Length != 4)
        {
            var newInv = new Item[4];
            for(var i = 0; i < newInv.Length && i < Inventory.Length; i++)
            {
                newInv[i] = Inventory[i];
            }

            Inventory = newInv;
        }
    }
}

