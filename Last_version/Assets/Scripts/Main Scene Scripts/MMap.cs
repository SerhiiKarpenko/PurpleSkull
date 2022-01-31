using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMap : MonoBehaviour
{
	[SerializeField] private PlayerScript playerScript;
	[SerializeField] private EnemyHolderScript holderScript;
	[SerializeField] private Unit playerUnit;
	private Vector3 loc;
	private Vector3 temp;
	private void Start()
	{
		//		player.z -= 1;
	}
	void Update()
	{
		temp = playerScript.getPlayerPosition();
		loc.x = (temp.x*10) + 400;
		loc.y = (temp.z * 10) + 42;
		transform.position = loc;
	}
}