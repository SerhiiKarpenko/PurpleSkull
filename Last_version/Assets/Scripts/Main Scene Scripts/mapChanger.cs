using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapChanger : MonoBehaviour
{
	public bool isMapHell { get; set; }
	[SerializeField] private GameObject[] bones;
	[SerializeField] private EnemyHolderScript enemiesHolder;
	[SerializeField] private GameObject[] cutScens;


	[Header("Mom")]
	[SerializeField] private Image momsImageOnDialogCanvas;
	[SerializeField] private Sprite momSimple;
	[SerializeField] private Sprite momHell;

	[Header("Materials")]
	[SerializeField] private Material grass;
	[SerializeField] private Material water;
	[SerializeField] private Material stone;
	[SerializeField] private Material clifSide;
	[SerializeField] private Material ground;
	[SerializeField] private Material sand;
	[SerializeField] private Material appleMaterial1;
	[SerializeField] private Material appleMaterial2;
	[SerializeField] private Material firMaterial1;
	[SerializeField] private Material firMaterial2;
	[SerializeField] private Material palmMaterial1;
	[SerializeField] private Material palmMaterial2;
	[SerializeField] private Material willowMaterial1;
	[SerializeField] private Material willowMaterial2;




	[Header("Hell textures")]
	[SerializeField] private Texture grassHell;
	[SerializeField] private Texture waterHell;
	[SerializeField] private Texture stoneHell;
	[SerializeField] private Texture clifSideHell;
	[SerializeField] private Texture groudHell;
	[SerializeField] private Texture sandHell;
	[SerializeField] private Texture applePart1Hell;
	[SerializeField] private Texture applePart2Hell;
	[SerializeField] private Texture firPart1Hell;
	[SerializeField] private Texture firPart2Hell;
	[SerializeField] private Texture palmPart1Hell;
	[SerializeField] private Texture palmPart2Hell;
	[SerializeField] private Texture willowPart1Hell;
	[SerializeField] private Texture willowPart2Hell;

	[Header("Simple textures")]
	[SerializeField] private Texture grassSimple;
	[SerializeField] private Texture waterSimple;
	[SerializeField] private Texture stoneSimple;
	[SerializeField] private Texture clifSideSimple;
	[SerializeField] private Texture groudSimple;
	[SerializeField] private Texture sandSimple;
	[SerializeField] private Texture applePart1Simple;
	[SerializeField] private Texture applePart2Simple;
	[SerializeField] private Texture firPart1Simple;
	[SerializeField] private Texture firPart2Simple;
	[SerializeField] private Texture palmPart1Simple;
	[SerializeField] private Texture palmPart2Simple;
	[SerializeField] private Texture willowPart1Simple;
	[SerializeField] private Texture willowPart2Simple;







	public void mapChangeToHell()
	{
		isMapHell = true;
		grass.SetTexture("_MainTex", grassHell);
		water.SetTexture("_MainTex", waterHell);
		stone.SetTexture("_MainTex", stoneHell);
		clifSide.SetTexture("_MainTex", clifSideHell);
		ground.SetTexture("_MainTex", groudHell);
		sand.SetTexture("_MainTex", sandHell);
		appleMaterial1.SetTexture("_MainTex", applePart1Hell);
		appleMaterial2.SetTexture("_MainTex", applePart2Hell);
		firMaterial1.SetTexture("_MainTex", firPart1Hell);
		firMaterial2.SetTexture("_MainTex", firPart2Hell);
		palmMaterial1.SetTexture("_MainTex", palmPart1Hell);
		palmMaterial2.SetTexture("_MainTex", palmPart2Hell);
		willowMaterial1.SetTexture("_MainTex", willowPart1Hell);
		willowMaterial2.SetTexture("_MainTex", willowPart2Hell);
		momsImageOnDialogCanvas.sprite = momSimple;
		EnemiesLoadForHell();

	}

	public void mapChangeToSimple()
    {
		isMapHell = false;
		grass.SetTexture("_MainTex", grassSimple);
		water.SetTexture("_MainTex", waterSimple);
		stone.SetTexture("_MainTex", stoneSimple);
		clifSide.SetTexture("_MainTex", clifSideSimple);
		ground.SetTexture("_MainTex", groudSimple);
		sand.SetTexture("_MainTex", sandSimple);
		appleMaterial1.SetTexture("_MainTex", applePart1Simple);
		appleMaterial2.SetTexture("_MainTex", applePart2Simple);
		firMaterial1.SetTexture("_MainTex", firPart1Simple);
		firMaterial2.SetTexture("_MainTex", firPart2Simple);
		palmMaterial1.SetTexture("_MainTex", palmPart1Simple);
		palmMaterial2.SetTexture("_MainTex", palmPart2Simple);
		willowMaterial1.SetTexture("_MainTex", willowPart1Simple);
		willowMaterial2.SetTexture("_MainTex", willowPart2Simple);
		momsImageOnDialogCanvas.sprite = momHell;
		EnemiesLoadForSimple();

	}


	public void EnemiesLoadForSimple()
    {

		for (int i = 0; i < cutScens.Length; i++)
			cutScens[i].SetActive(false);
		

		for (int i = 0; i < bones.Length; i++)
			bones[i].SetActive(true);

		if (!PlayerPrefs.HasKey("MotherWasDefeated"))
		{
			for (int i = 0; i < enemiesHolder.enemies.Count - 1; i++)
			{
				if (enemiesHolder.enemies[i].gameObject != null)
					enemiesHolder.enemies[i].SetActive(false);
			}
		}
		else
        {
			for (int i = 0; i < enemiesHolder.enemies.Count; i++)
			{
				if (enemiesHolder.enemies[i].gameObject != null)
					enemiesHolder.enemies[i].SetActive(false);
			}
		}

		enemiesHolder.enemies[enemiesHolder.enemies.Count - 1].SetActive(true);

		if(!PlayerPrefs.HasKey("MapWasChangedToLastState"))
			PlayerPrefs.SetString("MapWasChangedToLastState", "Yes");


		
		
    }

	public void EnemiesLoadForHell()
    {
		for (int i = 0; i < bones.Length; i++)
			bones[i].SetActive(false);

		for (int i = 0; i < enemiesHolder.enemies.Count - 2; i++) 
		{
			if(enemiesHolder.enemies[i].gameObject != null)
				enemiesHolder.enemies[i].SetActive(true);
		}

		enemiesHolder.enemies[enemiesHolder.enemies.Count - 1].SetActive(false);
	}


}
