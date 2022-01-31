using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoadScript : MonoBehaviour
{
	
	[SerializeField] private Unit[] units;
	private int id;

	[SerializeField] private SpriteRenderer playerSprite;
	[SerializeField] private SpriteRenderer enemySprite;

	public battleHudScript playerHUD;
	public battleHudScript enemyHUD;
	public Animator animator;

	

	private void Awake()
	{
		id = PlayerPrefs.GetInt("EnemyThatCollidingID");
	}

	private void Start()
	{
		playerHUD.setHUD(units[0]);
		playerHUD.SetStats(units[0]);
		enemyHUD.setHUD(units[1]);
		
		enemySprite.sprite = units[1].getSprite();
		enemySprite.transform.localScale = new Vector3(35, 35, 0);
	}



}
