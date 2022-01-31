using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEqupment : MonoBehaviour
{
	#region Singleton
	public static PlayerEqupment instance;
	private void Awake()
	{
		instance = this; 
	}
	#endregion

	public Equipment[] equipment;
	public PlayerEquipmentSlot PlayerEquipmentSlot;
	public InventorySlot[] slotsForEquipment;
	public Unit player;
	public int key = 0;
	[SerializeField] private PlayerInventory inventory;
	[SerializeField] private itemsHolder itemHolder;

	void Start()
	{
		int numberOfSlots = System.Enum.GetNames(typeof(PlayerEquipmentSlot)).Length;
		equipment = new Equipment[numberOfSlots];
	}


	public void Equip(Equipment item)
	{
		if (item.playerEquipmentSlot == PlayerEquipmentSlot.Head)
		{
			EquipForSlotWhereMustItemBe(0, item);
		}
		else if(item.playerEquipmentSlot == PlayerEquipmentSlot.Chest)
        {
			EquipForSlotWhereMustItemBe(1, item);
		}
		else if(item.playerEquipmentSlot == PlayerEquipmentSlot.Hands)
        {
			EquipForSlotWhereMustItemBe(2, item);
		}
		else if (item.playerEquipmentSlot == PlayerEquipmentSlot.Feet)
		{
			EquipForSlotWhereMustItemBe(3, item);
		}
		else if (item.playerEquipmentSlot == PlayerEquipmentSlot.Key)
		{
			EquipForSlotWhereMustItemBe(4, item);
		}
		else
        {
			EquipForSlotWhereMustItemBe(5, item);
		}

		int slotIndex = (int)item.playerEquipmentSlot;
		equipment[slotIndex] = item;
		EquipedItemsModifires();
		equipment[slotIndex].isEquiped = true;
		//PlayerPrefs.SetString("NameOfTheItem" + equipment[slotInex].name, equipment[slotInex].name);
		

		
	}

	

	public void EquipedItemsModifires()
	{
		for(int i = 0; i < equipment.Length; i++)
		{
			if(equipment[i] != null)
			{
				if (!equipment[i].isModifireAlreadyAdded)
				{
					player.setforequipHP(equipment[i].hpModifire);
					player.setforequip(equipment[i].damageModifire);
					equipment[i].isModifireAlreadyAdded = true;
					if(i==4)
                    {
						key = 1;
                    }
				}
			}
		}
	}

	private void EquipForSlotWhereMustItemBe(int idOfSlot, Equipment item)
    {
		if (slotsForEquipment[idOfSlot].item != null)
		{
			Debug.Log("Somethin was there");
			Equipment eq = (Equipment)slotsForEquipment[idOfSlot].item;
			eq.isEquiped = false;
			player.setforequip(-eq.damageModifire);
			player.setforequipHP(-eq.hpModifire);
			slotsForEquipment[idOfSlot].AddToTheEquipmentUI(item, slotsForEquipment[idOfSlot]);
			if (idOfSlot == 4)
			{
				key = 0;
			}

		}
		else
		{
			Debug.Log("Nothing Was There");
			slotsForEquipment[idOfSlot].AddToTheEquipmentUI(item, slotsForEquipment[idOfSlot]);
			if (idOfSlot == 4)
			{
				key = 1;
			}
		}
	}

	public void UnEquip(Equipment item)
    {
		int slotIndex = (int)item.playerEquipmentSlot;
		equipment[slotIndex] = null;
    }
}
