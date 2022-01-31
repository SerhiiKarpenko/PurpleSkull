using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// подумать о том как загрузить лут который лежал на местах и который я не взял

public class InventorySlot : MonoBehaviour
{
	public int id;
	public int idWhereSetIsDeelted;
	public int itemid;
	public bool isEmpty;
    public Item item;
	public Image icon;
	public itemsHolder items;
	public PlayerInventory playerEQ;
	[SerializeField] private PlayerEqupment playerEqupment;
	[SerializeField] private Button removeButton;
	

	private void Start()
	{ 
		isEmpty = true;
		removeButton.enabled = false;
		removeButton.image.color = new Color(225, 255, 255, 0);

	}

    public void AddItem(Item newItem, int slotID, int itemID)
	{
		removeButton.enabled = true;
		removeButton.image.color = new Color(225, 255, 255, 1);
		itemid = itemID;
		PlayerPrefs.SetInt("AddedItemToInventory" + itemid, itemid);
		playerEQ.slots[slotID].isEmpty = false;
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;

		
	}

	public void AddToTheEquipmentUI(Item newItem, InventorySlot slot)
    {
		removeButton.enabled = true;
		removeButton.image.color = new Color(225, 255, 255, 1);
		slot.isEmpty = false;
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
    }

	public void RemoveItem(int slotID)
	{
		removeButton.enabled = false;
		removeButton.image.color = new Color(225, 255, 255, 0);
		items.items[itemid].GetComponent<PickUp>().isDeleted = true;
		items.items[itemid].GetComponent<PickUp>().isInInventory = false;
		playerEQ.slots[slotID].isEmpty = true;
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		PlayerPrefs.DeleteKey("AddedItemToInventory" + itemid);
		playerEQ.pickedUpItemsID.Remove(itemid);
		PlayerPrefs.SetInt("DeletedItem" + itemid, itemid);
		playerEQ.listOfDeletedItems.Add(itemid);
	}

	public void RemoveFromEquipmentUI()
    {
		//searching for item in equipment slot and unequip it
		if (playerEQ.pickedUpItemsID.Count < playerEQ.maxCapacity)
		{
			removeButton.enabled = false;
			removeButton.image.color = new Color(225, 255, 255, 0);
			string name = item.name;
			for (int i = 0; i < playerEqupment.equipment.Length; i++)
			{
				if (playerEqupment.equipment[i] != null)
				{
					if (playerEqupment.equipment[i].name == name)
					{
						playerEqupment.player.setforequip(-playerEqupment.equipment[i].damageModifire);
						playerEqupment.player.setforequipHP(-playerEqupment.equipment[i].hpModifire);
						playerEqupment.equipment[i].isEquiped = false;
						playerEqupment.equipment[i].isModifireAlreadyAdded = false;
						playerEqupment.equipment[i] = null;
						break;
					}
				}

			}
			//add item to inventory
			for (int i = 0; i < playerEQ.slots.Length; i++)
			{
				if (playerEQ.slots[i].isEmpty)
				{
					for (int j = 0; j < items.items.Count; j++)
					{
						if (items.items[j].GetComponent<PickUp>().item.name == name)
						{
							bool isAdded = playerEQ.Add(items.items[j].GetComponent<PickUp>().id);
							if (isAdded)
							{
								item = null;
								icon.sprite = null;
								icon.enabled = false;
								break;
							}
							else
							{
								Debug.Log("have no place");
							}
							break;
						}
					}
					break;
				}
			}
		}
		else
        {
			Debug.Log("have no place");
        }
    }


	public void UseItem()
    {
		if (item != null)
		{
			item.Use(id);
			RemoveItem(id);
			
		}
    }

	
}
