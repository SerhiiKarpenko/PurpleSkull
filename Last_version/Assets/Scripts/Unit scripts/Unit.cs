using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
	public UnitTemplate unitTemplate;
	public int currentHp;
	public int currentDmg;
	public int currentLvl;
	public int idOnScene;
	public bool isDefeated = false;
	public UnityEvent<int> onDmgChange;
	public UnityEvent<int> onHpChange;
	public UnityEvent<int> onLvlChange;
	

	[Header("Only for enemiesHolder")]
	public int whichEnenmy;



	public void Start()
	{
		currentHp = unitTemplate.maxHp;
		currentDmg = unitTemplate.damage;
		currentLvl = unitTemplate.lvl;
		whichEnenmy = unitTemplate.whichEnemy;
	}
	
	public void setIdOnScene(int id)
	{
		idOnScene = id;
	}

	public int getId()
	{
		return unitTemplate.id;
	}

	public int getLvl()
	{
		return unitTemplate.lvl;
	}

	public void setLvl(int lvl)
	{
		unitTemplate.lvl += lvl;
		currentLvl = unitTemplate.lvl;
		SetDamage(1);
		SetMaxHP(1);
		onLvlChange.Invoke(currentLvl);
		unitTemplate.healPower += 1;

	}

	public int getMAXHP()
	{
		return unitTemplate.maxHp;
	}

	public string getName()
	{
		return unitTemplate.unitName;
	}

	public Sprite getSprite()
	{
		return unitTemplate.sprite;
	}

	public int getDamage()
	{
		return unitTemplate.damage;
	}

	public void SetDamage(int dmg)
    {
		unitTemplate.damage += dmg;
		currentDmg = unitTemplate.damage;
		onDmgChange.Invoke(currentDmg);
	}

	public void setforequip(int dmg)
    {
		currentDmg += dmg;
		onDmgChange.Invoke(currentDmg);
	}

	public int getHealPower()
	{
		return unitTemplate.healPower;
	}

	public int getMaxHealCountPerBattle()
	{
		return unitTemplate.MaxHealCountPerBattle;
	}

	public void SetMaxHP(int hp)
    {
		unitTemplate.maxHp += hp;
		currentHp = unitTemplate.maxHp;
		onHpChange.Invoke(currentHp);
	}

	public void setforequipHP(int hp)
	{
		currentHp += hp;
		onHpChange.Invoke(currentHp);
	}


	public bool TakeDamage(int damage)
	{
		currentHp -= damage;

		if(currentHp <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}    
	}

	public void Heal(int hpCount)
	{
		currentHp += hpCount;
		if(currentHp > unitTemplate.maxHp)
		{
			currentHp = unitTemplate.maxHp;
		}
		
	}

   
}
