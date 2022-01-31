using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public string whatIsIt;

    public virtual void Use(int id)
    {
        Debug.Log("Use " + name);
    }

    public void Remove ()
    {
        
    }
}




