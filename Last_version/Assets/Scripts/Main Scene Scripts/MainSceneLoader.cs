using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{
	private int playerLD;
	int count;
	int time = 0;
	int lostCount = 0;
//	public int firstplay = 1;
	private Vector3 playerPOS;
	private string battleState;

	[SerializeField] private mapChanger mapChanger;
	[SerializeField] private PlayerScript playerScript;
	[SerializeField] private EnemyHolderScript holderScript;
	[SerializeField] private PlayerInventory playerInventory;
	[SerializeField] private catSceneScript[] cutScens;
	[SerializeField] private itemsHolder itemsHolder;
	[SerializeField] private Unit playerUnit;
	//[SerializeField] private PlayerEqupment equipment;
	[SerializeField] private InventorySlot[] slots;
	[SerializeField] private PlayerEqupment equipment;
	[SerializeField] private PlayerEqupment keySlot;
	public List<int> pickedUP = new List<int>();
	public List<int> deletedItems = new List<int>();
	public List<Vector3> itemsOnScenePositions = new List<Vector3>();
	public List<Vector3> positionWhereFightWasLost = new List<Vector3>();
	public List<GameObject> itemsOnScene = new List<GameObject>();
	private void Start()
	{
		PlayerPrefs.SetString("Started", "Started");
		if (GetSavesForPlayer())
		{
			SetSavesForPlayer();
		}

	}


	private bool GetSavesForPlayer()
	{
/*		int firstplayd=PlayerPrefs.GetInt("NoReload");
		if (firstplayd!=1)
        {
			Debug.Log("Delete");
			PlayerPrefs.DeleteAll();
		}*/
		if (PlayerPrefs.HasKey("WasInBattle"))
		{
			if (PlayerPrefs.HasKey("PlayerLookingDirection"))
			{
				for (int i = 0; i < itemsHolder.items.Count; i++)
				{
					if (PlayerPrefs.HasKey("AddedItemToInventory" + i))
					{
						pickedUP.Add(itemsHolder.items[i].GetComponent<PickUp>().id);

					}
				}

				if (PlayerPrefs.HasKey("Times"))
					time = PlayerPrefs.GetInt("Times");

				for (int i = 0; i < time; i++)
				{
					Vector3 positions = new Vector3(
						PlayerPrefs.GetFloat("PlayerPositionForItemX" + i),
						PlayerPrefs.GetFloat("PlayerPositionForItemY" + i),
						PlayerPrefs.GetFloat("PlayerPositionForItemZ" + i)
						);

					if (positions != Vector3.right && positions != Vector3.left && positions != Vector3.forward && positions != Vector3.back)
						itemsOnScenePositions.Add(positions + playerScript.walkDir[PlayerPrefs.GetInt("PlayerLookingDirectionForItem" + i)]);
				}



				// searching and deleting all the same positions of item spawning 
				for (int i = 0; i < itemsOnScenePositions.Count; i++)
				{
					for (int j = i + 1; j < itemsOnScenePositions.Count; j++)
					{
						if (itemsOnScenePositions[i] == itemsOnScenePositions[j])
						{
							itemsOnScenePositions.Remove(itemsOnScenePositions[j]);
						}
					}
				}



				for (int i = 0; i < itemsHolder.items.Count; i++)
				{
					if (PlayerPrefs.HasKey("DeletedItem" + i))
					{
						deletedItems.Add(itemsHolder.items[i].GetComponent<PickUp>().id);
					}
				}

				for (int i = 0; i < itemsHolder.items.Count; i++)
				{
					if (PlayerPrefs.HasKey("NameOfTheItem" + itemsHolder.items[i].GetComponent<PickUp>().item.name))
					{

						//Debug.Log("Found");
						//if (PlayerPrefs.HasKey("DeletedItem" + i))
						//{
						// deleting from list where only items that must be deleted after laoding the scene
						//PlayerPrefs.DeleteKey("DeletedItem" + i);

						//}
						playerInventory.Add(itemsHolder.items[i].GetComponent<PickUp>().id);
					}
				}


				playerLD = PlayerPrefs.GetInt("PlayerLookingDirection");
				playerPOS.x = PlayerPrefs.GetFloat("PlayerPositionX");
				playerPOS.y = PlayerPrefs.GetFloat("PlayerPositionY");
				playerPOS.z = PlayerPrefs.GetFloat("PlayerPositionZ");
				battleState = PlayerPrefs.GetString("BattleState");


				if (battleState == "Lost")
				{
					time = PlayerPrefs.GetInt("Times");
					int hlp = time -= 1;
					positionWhereFightWasLost.Add(itemsOnScenePositions[itemsOnScenePositions.Count - 1]);
					PlayerPrefs.DeleteKey("PlayerPositionForItemX" + hlp);
					PlayerPrefs.DeleteKey("PlayerPositionForItemY" + hlp);
					PlayerPrefs.DeleteKey("PlayerPositionForItemZ" + hlp);
					PlayerPrefs.DeleteKey("layerLookingDirectionForItem" + hlp);
				}

				PlayerPrefs.Save();
				PlayerPrefs.DeleteKey("WasInBattle");
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
		for (int i = 0; i < holderScript.enemies.Count; ++i)
		{
					if (PlayerPrefs.HasKey("MustBeDeleted" + i))
							{
								PlayerPrefs.DeleteAll();
					}
		}/*
			if (!PlayerPrefs.HasKey("NoReload"))
			{
				Debug.Log("Delete");
				PlayerPrefs.DeleteAll();
			}*/
			return false;
		}

	}

	private void SetSavesForPlayer()
	{
		if (battleState == "Won")
		{
			//playerUnit.currentDmg = PlayerPrefs.GetInt("PlayerDamage");
			//playerUnit.currentHp = PlayerPrefs.GetInt("PlayerHp");

			playerScript.gameObject.transform.position = playerPOS;
			playerScript.setLookDir(PlayerPrefs.GetInt("PlayerLookingDirection"));
			//Destroy(holderScript.enemiesHolder[PlayerPrefs.GetInt("MustBeDeleted" + holderScript.enemiesHolder[PlayerPrefs.GetInt("OdOnSceneOfEnemies")])]);
			if (holderScript.enemies.Count > 0)
			{
				for (int i = 0; i < holderScript.enemies.Count; ++i)
				{
					if (PlayerPrefs.HasKey("MustBeDeleted" + i))
					{
						Destroy(holderScript.enemies[PlayerPrefs.GetInt("MustBeDeleted" + i)]);
					}
				}
			}


			//one step forward after winning the fight
			//playerScript.gameObject.transform.position += playerScript.walkDir[PlayerPrefs.GetInt("PlayerLookingDirection")];
			//string a = PlayerPrefs.GetString("BattleState");

			for (int i = 0; i < positionWhereFightWasLost.Count; i++)
			{
				for (int j = 0; j < itemsOnScenePositions.Count; j++)
				{
					if (itemsOnScenePositions[j] == positionWhereFightWasLost[i])
					{
						itemsOnScenePositions.Remove(itemsOnScenePositions[j]);
					}
				}
			}


			for (int i = 0; i < itemsOnScenePositions.Count; i++)
			{
//				int rnd = Random.Range(0, itemsHolder.items.Count);
				int rnd = Random.Range(0, 3);
				int key = PlayerPrefs.GetInt("PresetDrop");
/*				if (key == 1)
				{
					Debug.Log("U got teh key");
					Instantiate(itemsHolder.items[4], itemsOnScenePositions[i], Quaternion.identity);
				}
				else
                {*/
					Instantiate(itemsHolder.items[rnd], itemsOnScenePositions[i], Quaternion.identity);
				//}
			}
			PlayerPrefs.SetInt("PresetDrop", 0);


			for (int i = 0; i < slots.Length; i++)
			{
				if (!slots[i].isEmpty)
					slots[i].UseItem();
			}

			playerInventory.listOfDeletedItems = deletedItems;
			for (int i = 0; i < deletedItems.Count; i++)
			{
				itemsHolder.items[deletedItems[i]].SetActive(false);
			}

			for (int i = 0; i < pickedUP.Count; i++)
			{
				bool isAdded = playerInventory.Add(pickedUP[i]);
				if (isAdded)
					itemsHolder.items[pickedUP[i]].SetActive(false);
			}


			for(int i = 0; i < cutScens.Length; i++)
            {
				if(PlayerPrefs.HasKey("CatSceneWasShowed" + i))
                {
					cutScens[i].showedCutScenes = true;
                }
            }

			if (PlayerPrefs.HasKey("BossWasDefeated"))
			{
				if (!keySlot.slotsForEquipment[4].isEmpty)
				{
					keySlot.slotsForEquipment[4].RemoveItem(4);
					keySlot.slotsForEquipment[4].isEmpty = true;
					equipment.equipment[4] = null;
					PlayerPrefs.DeleteKey("BossWasDefeated");
				}

			}

			if(PlayerPrefs.HasKey("MapWasChangedToLastState"))
            {
				mapChanger.EnemiesLoadForSimple();
            }


			playerUnit.setLvl(1);

			Debug.Log("Loaded");

		}
		else if (battleState == "Lost")
		{
			playerUnit.currentDmg = PlayerPrefs.GetInt("PlayerDamage");
			playerUnit.currentHp = PlayerPrefs.GetInt("PlayerHp");

			playerScript.gameObject.transform.position = playerPOS;
			playerScript.setLookDir(PlayerPrefs.GetInt("PlayerLookingDirection"));
			//Destroy(holderScript.enemiesHolder[PlayerPrefs.GetInt("MustBeDeleted" + holderScript.enemiesHolder[PlayerPrefs.GetInt("OdOnSceneOfEnemies")])]);
			if (holderScript.enemies.Count > 0)
			{
				for (int i = 0; i < holderScript.enemies.Count; ++i)
				{
					if (PlayerPrefs.HasKey("MustBeDeleted" + i))
					{
						Destroy(holderScript.enemies[PlayerPrefs.GetInt("MustBeDeleted" + i)]);
					}
				}
			}


			//positionWhereFightWasLost.Add(itemsOnScenePositions[itemsOnScenePositions.Count - 1]);
			//for (int i = 0; i < positionWhereFightWasLost.Count; i++)
			//{
			//	for (int j = 0; j < itemsOnScenePositions.Count; i++)
			//	{
			//		if (itemsOnScenePositions[j] == positionWhereFightWasLost[i])
			//		{
			//			itemsOnScenePositions.Remove(itemsOnScenePositions[j]);
			//		}
			//	}
			//}


			for (int i = 0; i < positionWhereFightWasLost.Count; i++)
			{
				for (int j = 0; j < itemsOnScenePositions.Count; j++)
				{
					if (itemsOnScenePositions[j] == positionWhereFightWasLost[i])
					{
						itemsOnScenePositions.Remove(itemsOnScenePositions[j]);
					}
				}
			}




			for (int i = 0; i < itemsOnScenePositions.Count; i++)
			{

				int rnd = Random.Range(0, itemsHolder.items.Count);
				Instantiate(itemsHolder.items[rnd], itemsOnScenePositions[i], Quaternion.identity);
			}


			playerScript.gameObject.transform.position = playerPOS;
			if (playerLD == 0)
			{
				playerScript.setLookDir(2);
			}
			else if (playerLD == 1)
			{
				playerScript.setLookDir(3);
			}
			else if (playerLD == 2)
			{
				playerScript.setLookDir(0);
			}
			else if (playerLD == 3)
			{
				playerScript.setLookDir(1);
			}

			playerInventory.listOfDeletedItems = deletedItems;
			for (int i = 0; i < deletedItems.Count; i++)
			{
				itemsHolder.items[deletedItems[i]].SetActive(false);
			}
			for (int i = 0; i < pickedUP.Count; i++)
			{
				bool isAdded = playerInventory.Add(pickedUP[i]);
				if (isAdded)
					itemsHolder.items[pickedUP[i]].SetActive(false);
			}

			for (int i = 0; i < cutScens.Length; i++)
			{
				if (PlayerPrefs.HasKey("CatSceneWasShowed" + i))
				{
					cutScens[i].showedCutScenes = true;
				}
			}


			if (PlayerPrefs.HasKey("MapWasChangedToLastState"))
			{
				mapChanger.EnemiesLoadForSimple();
			}

			

		}

	}

}
