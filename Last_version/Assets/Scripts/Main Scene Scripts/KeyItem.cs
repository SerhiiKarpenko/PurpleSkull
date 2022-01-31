using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
	[SerializeField] private Unit enemy;
	private Vector3 vector = new Vector3(0, 2, 0);
	private void Start()
	{
		int i = enemy.getId()-1;
		if (PlayerPrefs.HasKey("MustBeDeleted" + i))
		{
			transform.position += vector;
		}
	}
}