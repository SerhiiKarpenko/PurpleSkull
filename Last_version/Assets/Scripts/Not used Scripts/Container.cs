using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
	[SerializeField] private PlayerScript playerScript;

	private void Update()
	{
		if(playerScript.isColiding() && playerScript.colliderForEnemy!= null && playerScript.colliderForEnemy.name == "Enemy")
		{
			int playerLD = playerScript.getLookDir();
			int ID = playerScript.colliderForEnemy.GetComponent<Unit>().getId();
			Vector3 playerPos = playerScript.getPlayerPosition();


			PlayerPrefs.SetInt("EnemyThatCollidingID", ID);
			PlayerPrefs.SetInt("PlayerLookingDirection", playerLD);
			PlayerPrefs.SetFloat("PlayerPositionX", playerPos.x);
			PlayerPrefs.SetFloat("PlayerPositionY", playerPos.y);
			PlayerPrefs.SetFloat("PlayerPositionZ", playerPos.z);

			//Debug.Log(PlayerPrefs.GetInt("EnemyThatCollidingID"));
		}
	}  
}
