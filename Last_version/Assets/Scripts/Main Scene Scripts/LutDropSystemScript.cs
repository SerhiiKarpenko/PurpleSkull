using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// правильный спавн лута не работает потому что оно берет элементы с таблицы и запоинмает адолжо те что на сцене


public class LutDropSystemScript : MonoBehaviour
{
	public List<GameObject> itemsOnScene;
	public List<GameObject> it;
	public List<int> aa;
	[SerializeField] private itemsHolder items;
	private Vector3 itemsStorage = new Vector3(0f, -5f, 0f);
	private string lastStateOfBattle;
	int count;

	[SerializeField] private PlayerScript player;

	private void Start()
	{
		lastStateOfBattle = PlayerPrefs.GetString("BattleState");
		ChekBattleState();
	}

    private void ChekBattleState()
	{
		if(lastStateOfBattle == "Won")
		{
			Debug.Log("U got teh key");
			// lut apperaing on the enemy place
			Vector3 positionOfItemAppearing;
			positionOfItemAppearing.x = PlayerPrefs.GetFloat("EnemyPositionX");
			positionOfItemAppearing.y = PlayerPrefs.GetFloat("EnemyPostiionY");
			positionOfItemAppearing.z = PlayerPrefs.GetFloat("EnemyPositionZ");
			int randomItem = Random.Range(0, 4);
			int key = PlayerPrefs.GetInt("PresetDrop");
			if (key==1)
            {
				Debug.Log("U got teh key");
				PlayerPrefs.SetInt("PresetDrop", 0);
			}
			//Instantiate(items.items[randomItem], positionOfItemAppearing, Quaternion.identity);
			itemsOnScene.Add(items.items[randomItem]);
			//it.Add(items.items[randomItem]);

			//PlayerPrefs.SetInt("ItemOnScene" + items.items[randomItem].GetComponent<PickUp>().id, items.items[randomItem].GetComponent<PickUp>().id);
			
			// probably can delete battle state here
		}
	}


}
