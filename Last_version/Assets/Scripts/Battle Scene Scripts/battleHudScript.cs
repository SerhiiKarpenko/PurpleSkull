using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHudScript : MonoBehaviour
{
	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Text damageStat;
	public Text hpStat;

	public void setHUD(Unit unit)
	{
		nameText.text = unit.getName();
		levelText.text = "Lvl: " + unit.getLvl();
		hpSlider.maxValue = unit.getMAXHP();
		if (unit.currentHp != hpSlider.maxValue) 
		{
			unit.currentHp = unit.getMAXHP();
			hpSlider.value = unit.currentHp;
		}
		
		
	}

	public void setHP(int hp)
	{
		hpSlider.value = hp;
	}

	public void SetStats(Unit unit)
    {
		damageStat.text = "Damage: " + unit.getDamage().ToString();
		hpStat.text = "Max heat points: " + unit.getMAXHP().ToString();
    }
}
