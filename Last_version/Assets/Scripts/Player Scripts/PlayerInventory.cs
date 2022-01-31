using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
	public int maxCapacity = 8;

	// int - id of pickUp-able object, int - number of
	//public Dictionary<int, int> pickedUpItems = new Dictionary<int, int>();
	public List<int> pickedUpItemsID = new List<int>();
	public List<int> notSortedIdOfPickedUpItems = new List<int>();
	public List<int> listOfDeletedItems = new List<int>();
	public InventorySlot[] slots;
	public itemsHolder items;
	public int ID;
	//[SerializeField] private PickUp pickUp;
	[SerializeField] private Canvas inventory;

	public delegate void OnInventoryChange();
	//public Item item;
	
	public OnInventoryChange inventoryChange => InventoryUiUpdate;

	private void Start()
	{
		//SetItemBools();
		//inventoryChange += InventoryUiUpdate;
	}


	private void Update()
	{
		
		if (Input.GetKeyDown(KeyCode.I))
		{

			ShowInventory();
			for (int i = 0; i < pickedUpItemsID.Count; ++i)
			{
				Debug.Log(pickedUpItemsID[i]);
			}
		}
		
		
	}

	public bool Add(int id)
	{
		if (pickedUpItemsID.Count >= maxCapacity)
		{
			Debug.Log("Have no space");
			return false;
		}
		
		ID = id;
		notSortedIdOfPickedUpItems.Add(id);
		pickedUpItemsID.Add(id);
		if (inventoryChange != null)
			inventoryChange.Invoke();
		return true;

	}


	private void ShowInventory()
	{
		inventory.enabled = !inventory.enabled;
	}

	public void InventoryUiUpdate()
	{
		
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].isEmpty)
			{
				Debug.Log("UpdatingUI");
				slots[i].AddItem(items.items[ID].gameObject.GetComponent<PickUp>().item, i, ID);
				//SortSlots(i, pickedUpItemsID);
				break;
			}
		}
	}

	public void SortSlots(int where, List<int> list)
	{
		
		list[where] = slots[where].id;
		
	}

	private void SetItemBools()
	{
		for (int i = 0; i < slots.Length; i++)
        {
			//if(slots[i].item != null)
			//	slots[i].item.isUsed = false;
        }

	}



}
