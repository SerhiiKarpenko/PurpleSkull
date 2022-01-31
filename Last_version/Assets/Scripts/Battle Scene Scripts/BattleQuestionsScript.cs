using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class BattleQuestionsScript : MonoBehaviour
{
	[SerializeField] private PlayerScript playerScript;
	[SerializeField] private EnemyHolderScript holderScript;
	[SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private itemsHolder itemsHolder;
	[SerializeField] private PlayerEqupment playerEqupment;
	[SerializeField] private LutDropSystemScript lut;
	[SerializeField] private Unit player;
	private int direction;
	int time = 0;
	private int countt;
	private string wasInBattle = "WasInBattle";
	

    public void Accept()
   {
		
		int playerLD = playerScript.getLookDir();
		int ID = playerScript.colliderForEnemy.GetComponent<Unit>().getId();
		int idOnScene = playerScript.colliderForEnemy.GetComponent<Unit>().idOnScene;

		Vector3 playerPos = playerScript.getPlayerPosition();
		Vector3 enemyPos = playerScript.GetEnemyObject().transform.position;

		SaveForLut();

		for (int i = 0;  i < itemsHolder.items.Count; i++)
        {
			// clear all the equiped items from player prefs
			if(PlayerPrefs.HasKey("NameOfTheItem" + itemsHolder.items[i].GetComponent<PickUp>().item.name))
            {
				PlayerPrefs.DeleteKey("NameOfTheItem" + itemsHolder.items[i].GetComponent<PickUp>().item.name);
            }
        }			

		for(int i = 0; i < playerEqupment.equipment.Length; i++)
        {
			// set names of all the items that currently equiped
			if(playerEqupment.equipment[i] != null)
				PlayerPrefs.SetString("NameOfTheItem" + playerEqupment.equipment[i].name, playerEqupment.equipment[i].name);
        }

		PlayerPrefs.SetInt("PlayerDamage", player.currentDmg);
		PlayerPrefs.SetInt("PlayerHp", player.currentHp);

		PlayerPrefs.SetInt("EnemyThatCollidingID",  ID);
		PlayerPrefs.SetInt("IdForBattleScene",  idOnScene);
		PlayerPrefs.SetInt("PlayerLookingDirection", playerLD);
		PlayerPrefs.SetFloat("PlayerPositionX", playerPos.x);
		PlayerPrefs.SetFloat("PlayerPositionY", playerPos.y);
		PlayerPrefs.SetFloat("PlayerPositionZ", playerPos.z);
		PlayerPrefs.SetFloat("EnemyPositionX", enemyPos.x);
		PlayerPrefs.SetFloat("EnemyPositionY", enemyPos.y);
		PlayerPrefs.SetFloat("EnemyPositionZ", enemyPos.z);
		PlayerPrefs.SetString("WasInBattle", wasInBattle);

		SceneManager.LoadScene("BattleScene");
   }

	public void Decline()
	{
		direction = playerScript.getLookDir();
		if (direction == 3)
		{
			direction = 1;
			playerScript.gameObject.transform.position += playerScript.runDir[direction];
		}
		else if(direction == 0)
		{
			direction = 2;
			playerScript.gameObject.transform.position += playerScript.runDir[direction];
		}
		else if(direction == 1)
		{
			direction = 3;
			playerScript.gameObject.transform.position += playerScript.runDir[direction];
		}
		else
		{
			direction = 0;
			playerScript.gameObject.transform.position += playerScript.runDir[direction];
		}
		playerScript.setLookDir(direction);
		
		//if(playerScript.colliderForEnemy.gameObject.transform.position.z > playerScript.gameObject.transform.position.z)
        //{
		//	Vector3 tmp = playerScript.getPlayerPosition();
		//	tmp.z -= 1;
		//	playerScript.gameObject.transform.position = tmp;
		//}
		//else
        //{
		//	Vector3 tmp = playerScript.getPlayerPosition();
		//	tmp.z -= 1;
		//	playerScript.gameObject.transform.position = tmp;
		//}
		//
		//if (playerScript.colliderForEnemy.gameObject.transform.position.x > playerScript.gameObject.transform.position.x)
		//{
		//	Vector3 tmp = playerScript.getPlayerPosition();
		//	tmp.x -= 1;
		//	playerScript.gameObject.transform.position = tmp;
		//}
		//else
		//{
		//	Vector3 tmp = playerScript.getPlayerPosition();
		//	tmp.x -= 1;
		//	playerScript.gameObject.transform.position = tmp;
		//}





		//
		//Vector3 tmp = playerScript.getPlayerPosition();
		//tmp.z -= 1;
		//playerScript.gameObject.transform.position = tmp;

	}
	private void SaveForLut()
	{

		
		if (PlayerPrefs.HasKey("Times"))
			time = PlayerPrefs.GetInt("Times");

		Vector3 playerPositionForItem;
		playerPositionForItem.x = playerScript.gameObject.transform.transform.position.x;
		playerPositionForItem.y = playerScript.gameObject.transform.transform.position.y;
		playerPositionForItem.z = playerScript.gameObject.transform.transform.position.z;

		Debug.Log(playerScript.gameObject.transform.position);

		PlayerPrefs.SetFloat("PlayerPositionForItemX" + time, playerPositionForItem.x);
		PlayerPrefs.SetFloat("PlayerPositionForItemY" + time, playerPositionForItem.y);
		PlayerPrefs.SetFloat("PlayerPositionForItemZ" + time, playerPositionForItem.z);
		PlayerPrefs.SetInt("PlayerLookingDirectionForItem" + time, playerScript.getLookDir());
		time++;
		PlayerPrefs.SetInt("Times", time);
	}
}
		//countt = lut.itemsOnScene.Count;
		//PlayerPrefs.SetInt("Count", countt);
		//
		// first variant of saving the items(lut) that currently on scene
		//for (int i = 0; i < countt; i++)
		//{
		//
		//	Debug.Log("Saving items");
		//	PlayerPrefs.SetInt("ItemOnSceneWithID" + lut.itemsOnScene[i].GetComponent<PickUp>().id + "_" + i, lut.itemsOnScene[i].GetComponent<PickUp>().id);
		//
		//}



		// second variant of saving the items(lut) that currently on scene, save the items(lut) that now on scene with their number 
		//for(int i = 0; i < count; i++)
		//      {
		//	int number = 0;
		//	if (PlayerPrefs.HasKey("NumberOfItemWithID" + lut.itemsOnScene[i].GetComponent<PickUp>().id))
		//	{
		//		Debug.Log("NumberOfItemWithID" + lut.itemsOnScene[i].GetComponent<PickUp>().id);
		//	}
		//	else 
		//	{

		//		for (int j = i; j < count; j++)
		//		{
		//			if (lut.itemsOnScene[i].GetComponent<PickUp>().id == lut.itemsOnScene[j].GetComponent<PickUp>().id)
		//			{
		//				Vector3 itemPos;
		//				itemPos.x = lut.itemsOnScene[j].transform.position.x;
		//				itemPos.y = lut.itemsOnScene[j].transform.position.y;
		//				itemPos.z = lut.itemsOnScene[j].transform.position.z;

		//				PlayerPrefs.SetFloat("ItemPositionX" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.x);
		//				PlayerPrefs.SetFloat("ItemPositionY" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.y);
		//				PlayerPrefs.SetFloat("ItemPositionZ" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.z);

		//				number++;
		//				PlayerPrefs.SetInt("NumberOfItemWithID" + lut.itemsOnScene[i].GetComponent<PickUp>().id, number);
		//			}

		//			if (number == 1 && j == count - 1 && lut.itemsOnScene[i].GetComponent<PickUp>().id != lut.itemsOnScene[j].GetComponent<PickUp>().id)
		//			{
		//				Vector3 itemPos;
		//				itemPos.x = lut.itemsOnScene[j].transform.position.x;
		//				itemPos.y = lut.itemsOnScene[j].transform.position.y;
		//				itemPos.z = lut.itemsOnScene[j].transform.position.z;

		//				PlayerPrefs.SetFloat("ItemPositionX" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.x);
		//				PlayerPrefs.SetFloat("ItemPositionY" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.y);
		//				PlayerPrefs.SetFloat("ItemPositionZ" + lut.itemsOnScene[j].GetComponent<PickUp>().id + "_" + j, itemPos.z);
		//				PlayerPrefs.SetInt("NumberOfItemWithID" + lut.itemsOnScene[i].GetComponent<PickUp>().id, number);
		//			}
		//		}
		//	}

		//}