using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public PlayerEquipmentSlot playerEquipmentSlot;
    public int hpModifire;
    public int damageModifire;
    public string whatisit;
    public bool isModifireAlreadyAdded = false;
    public bool isEquiped = false;
    [SerializeField] private PlayerInventory playerInventory;

    public override void Use(int id)
    {
        PlayerEqupment.instance.Equip(this);
        base.Use(id);
    }

}

public enum PlayerEquipmentSlot { Head, Chest, Hands, Feet, Key, Weapon }