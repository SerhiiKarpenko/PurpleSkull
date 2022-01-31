using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class catSceneScript : MonoBehaviour
{
	public Canvas catSceneCanvas;
	[SerializeField] private mapChanger mapChanger;
	[SerializeField] private PlayerScript player;
	[SerializeField] private Canvas equipedItemCanvas;
	[SerializeField] private GameObject[] cutScenes;
	[SerializeField] private int cutSceneIndex;

	//and don't forget you have map in your back pick." + "To open it press TAB

	private string[] catSceneTexts = { 
		"Wake up my dear, it's me, your mother. Monsters attacked our village. " +
		"I don't know who else is still alive, but try to find them and save them. I will help you on your way, and don't forget you have map in your backpack." +
		"To open it press TAB.",

		"You defeated the first monsters! After the death of enemies, their bones appear at the place of their death. " +
		"In them you can often find something useful, to open the inventory press I.",

		"This horrible worm won't let you pass this bridge, and he doesn't feel motivated to fight." +
		"Maybe he's hungry?" +
		"Look out for monster wolves wandering nearby and try to get a head from one of it, this meal should motivate the worm to fight!" +
		"Equip the head by placing it in a Key slot in your inventory.",

		"This is bloodface, he's the guardian of this bridge."+
		"He guards the island of Toxic Blue Bugs, he's their protector."+
		"I heard that one of the bugs escaped from the island and wanders around this island."+
		"Slay it and bring it's corpse, maybe this will motivate the Bloodface to start a fight!",

		"Watch out, my dear! This is not a loot! This is Mimic!"+
		"These thing usually stays immobile and don't attack, but this one seems to block your patch, so you must find a way to get rid of it."+
		"Mimics can be provoked with something that emits strong magic energy, there was an energy gem on an island nearby, but it was eaten by a worm."+
		"Search for the worm and get the gem out of it! With it you should be able to evoke Mimic.",

		"Our village is behind this bridge, but we're hold hostage by this terrifying beast known as The Mask."+
		"This thing is immortal, it has an enrgy shield! No weapon can harm it."+
		"It has only one weakness - The Heart of the Desert! It can disable it's immortality."+
		"The only Heart I've heard about was hidden near the shaman's hut in the deeper part of the desert."+
		"It's a long way, but I know you can do it."+
		"Good luck my dear, I believe in you!.",

		"You've made it." +
		"I'm so proud of you, my sweet daughter." +
		"And all of these monsters you've managed to slay, that's impressive." +
		"Yeah, hahaha." +
		"But i'm afraid this is where your story ends." +
		"Hahahahaha." +
		"Did you really believed in everything that happened today?" +
		"This is not a hell, there are no monsters, and i'm not your mother!" +
		"You were my puppet all along, and i used you to kill everyone in this village!" +
		"Yes, all these monsters were peacefull animals and innocent people just enjoying their life." +
		"And you killed them, ALL of them." +
		"Oh, you wanna fight me? Fair enough." +
		"Come back to where we've met for the first time, i'll be waiting, my dear, hahahaha."

	};

	[SerializeField] private Text textWhereWillDisplayedSpeackersText;
	[SerializeField] private Text toSkipText;
	public bool showedCutScenes = false;
	private bool checker;
	private bool tmp = false;

	public bool isCatSceneShowing { get; set; }

	



	void Start()
	{
		isCatSceneShowing = false;
		//sentences = new Queue<string>();
		

		/*showedCutScenes[0] = false;
		showedCutScenes[1] = false;
		showedCutScenes[2] = false;
		showedCutScenes[3] = false;
		showedCutScenes[4] = false;*?

	

		/*catSceneTexts[0] = "Wake up my dear, it's me, your mother. Monsters attacked our village. I don't know who else is still alive, but try to find them and save them, I will help you on your way".ToString();
		catSceneTexts[1] = "You defeated the first monsters! After the death of enemiesHolder, their bones appear at the place of their death. In them you can often find something useful, to open the inventory press I to open it";
		catSceneTexts[2] = "a";
		catSceneTexts[3] = "a";
		catSceneTexts[4] = "a";*/
	}

	// how to do it not always in update
	void Update()
	{ 
		
		if (Input.GetKeyDown(KeyCode.Return) && catSceneCanvas.enabled && checker)
		{ 
			catSceneCanvas.enabled = false;
			equipedItemCanvas.enabled = true;
			isCatSceneShowing = false;
			checker = false;
			toSkipText.enabled = false;
			tmp = false;
			player.isStandingOnCutScene = false;
			// here instead of tmp = false we can delete object with this cut scene

		}


		
			if (tmp)
			{
				// show cut scene
				if (!showedCutScenes)
				{
					player.isStandingOnCutScene = true;
					isCatSceneShowing = true;
					catSceneCanvas.enabled = true;
					equipedItemCanvas.enabled = false;
					showedCutScenes = true;
					StartCoroutine(ShowText(cutSceneIndex));
				}
			}
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !showedCutScenes)
        {
			tmp = true;
			player.forCutSceneIndex = getCutSceneIndex();
        }
    }



    private IEnumerator ShowText(int whichText)
	{
		//textWhereWillDisplayedSpeackersText.text = catSceneTexts[whichText];
		string text = catSceneTexts[whichText];
		PlayerPrefs.SetInt("CatSceneWasShowed" + whichText, whichText);
		textWhereWillDisplayedSpeackersText.text = "";
		int lengthOfAlreadyReaded = 0;
		Debug.Log(catSceneTexts[whichText].Length);
		foreach (char ch in text)
		{
			if(player.forCutSceneIndex == 6 && lengthOfAlreadyReaded == 180)
            {
				mapChanger.mapChangeToSimple();
            }
			
			if (ch == '.' || ch == '!' || ch == '?')
			{
				textWhereWillDisplayedSpeackersText.text += ch;
				lengthOfAlreadyReaded += textWhereWillDisplayedSpeackersText.text.Length;
				yield return new WaitForSeconds(1);

				if(lengthOfAlreadyReaded != catSceneTexts[whichText].Length)
					textWhereWillDisplayedSpeackersText.text = "";

				Debug.Log(lengthOfAlreadyReaded);
			}
			else
			{
				textWhereWillDisplayedSpeackersText.text += ch;
				yield return new WaitForSeconds(0.1f);

			}

			if (lengthOfAlreadyReaded == catSceneTexts[whichText].Length)
			{
				checker = true;
				toSkipText.enabled = true;
				break;

			}
		}
	}


	public int getCutSceneIndex()
    {
		return cutSceneIndex;
    }
}
