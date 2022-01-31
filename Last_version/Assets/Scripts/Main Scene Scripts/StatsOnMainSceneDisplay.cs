using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatsOnMainSceneDisplay : MonoBehaviour
{
	[SerializeField] private Text dmgPointsText;
	[SerializeField] private Text HpPointsText;
	[SerializeField] private Text lvlText;
	[SerializeField] private Unit playerUnit;




	void Start()
	{

		playerUnit.onDmgChange.AddListener((dmg) =>
		{
			dmgPointsText.text = dmg.ToString();
		});

		playerUnit.onHpChange.AddListener((hp) =>
		{
			HpPointsText.text = hp.ToString();
		});

		playerUnit.onLvlChange.AddListener((lvl) =>
        {
			lvlText.text = lvl.ToString();
        });


		dmgPointsText.text = playerUnit.currentDmg.ToString();
		HpPointsText.text = playerUnit.currentHp.ToString();
		lvlText.text = playerUnit.currentLvl.ToString();

	}



}
