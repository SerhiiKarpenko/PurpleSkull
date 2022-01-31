using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHendler : MonoBehaviour
{
	[SerializeField] private GameObject playerGameObject;
	private PlayerScript playerScript;

	private void Awake()
	{
		playerScript = playerGameObject.GetComponent<PlayerScript>();
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.S))
        {
			Save();
        }
	}

	private void Save()
	{
		// Save
		Vector3 playerPostion = playerScript.getPlayerPosition();

		PlayerPrefs.SetFloat("playerPostionX", playerPostion.x);
		PlayerPrefs.SetFloat("playerPostionY", playerPostion.y);
		PlayerPrefs.SetFloat("playerPostionZ", playerPostion.z);
		PlayerPrefs.Save();
		Debug.Log("Saved");
	}

}
