using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Lost, Won, Leave }

public class BattleSystem : MonoBehaviour
{
	public Text dialogueText;
	public BattleState battleState;
	public BattleState previosPlayerState;
	public battleHudScript playerHUD;
	public battleHudScript enemyHUD;
	public GameObject player;
	public GameObject enemy;
	public GameObject playerButtons;
	Unit playerUnit;
	Unit enemyUnit;
	private int playerHealCount = 0;
	private int enemyhealCount = 0;
	private bool isPlayerProtecting;
	public Button runButton;
	public Animator animator;
	public Animator playerAnimatior;
	

	private void Start()
	{
		PlayerPrefs.SetInt("NoReload", 1);
		enemy.GetComponent<Unit>().setIdOnScene(PlayerPrefs.GetInt("IdForBattleScene"));
		battleState = BattleState.Start;
		StartCoroutine(BattleSetUp());
		isPlayerProtecting = false;
	}

	IEnumerator BattleSetUp()
	{
		playerUnit = player.GetComponent<Unit>();
		enemyUnit = enemy.GetComponent<Unit>();

		//animator.SetInteger("whichEnemy", enemyUnit.whichEnenmy);

		if(enemyUnit.getName() == "Wolf")
        {
			dialogueText.text = "A wild " + enemyUnit.getName() + " approaches ";
			//animator.SetInteger("whichEnemy", 0);

        }
		else
        {
			dialogueText.text = "A horrible " + enemyUnit.getName() + " approaches ";
			//animator.SetInteger("whichEnemy", 1);
			
		}

		

		Debug.Log(animator.GetInteger("whichEnemy")); //checking which enemy is now fighting
		yield return new WaitForSeconds(2f);

		battleState = BattleState.PlayerTurn;
		PlayerTurn();

	}


	IEnumerator PlayerAtack()
    {
		playerAnimatior.SetBool("isPlayerAttacking", true);
		bool isDead = enemyUnit.TakeDamage(playerUnit.getDamage());
		enemyHUD.setHP(enemyUnit.currentHp);
		dialogueText.text = "Atack was successful";
		yield return new WaitForSeconds(1f);
		playerAnimatior.SetBool("isPlayerAttacking", false);
		if (isDead)
        {
			battleState = BattleState.Won;
			StartCoroutine(EndBattle());
		}
		else
        {
			battleState = BattleState.EnemyTurn;
			StartCoroutine(EnemyTurn());
        }
    }

	IEnumerator PlayerHeal()
    {
		if (playerHealCount <= playerUnit.getMaxHealCountPerBattle() - 1)
		{
			playerHealCount++;
			playerUnit.Heal(playerUnit.getHealPower());
			playerHUD.setHP(playerUnit.currentHp);
			playerAnimatior.SetBool("isPlayerHealing", true);
			yield return new WaitForSeconds(1f);
			playerAnimatior.SetBool("isPlayerHealing", false);
			//battleState = BattleState.EnemyTurn;
			//StartCoroutine(EnemyTurn());
		}
		else
        {
			dialogueText.text = "You can not heal any more!";
			yield return new WaitForSeconds(1f);
			PlayerTurn();
        }
    }

	IEnumerator PlayerProtect()
    {
		dialogueText.text = playerUnit.getName() + " trying to protect";
		yield return new WaitForSeconds(1f);
		isPlayerProtecting = true;
		battleState = BattleState.EnemyTurn;
		StartCoroutine(EnemyTurn());
		
    }

	private bool EnemyHeal()
    {
		
		if (enemyhealCount <= enemyUnit.getMaxHealCountPerBattle() - 1)
		{
			enemyhealCount++;
			enemyUnit.Heal(enemyUnit.getHealPower());
			enemyHUD.setHP(enemyUnit.currentHp);
			return true;
		}
		else
		{
			return false;
		}

    }
	

	IEnumerator EnemyTurn()
    {
		if (enemyhealCount <= enemyUnit.getMaxHealCountPerBattle() - 1)
		{
			int random;
			if (enemyUnit.currentHp < enemyUnit.getMAXHP()) 
				random = Random.Range(0, 2);
			else 
				random = 1;
			
			
			if (random == 1)
			{
				dialogueText.text = enemyUnit.getName() + " atacks! ";
				animator.SetBool("isAttacking", true);
				yield return new WaitForSeconds(1f);

				bool isDead;

				if (isPlayerProtecting)
				{
					dialogueText.text = enemyUnit.getName() + " atacks, but " + playerUnit.getName() + " is protecting!";
					
					isDead = playerUnit.TakeDamage(enemyUnit.getDamage() / 2);
					playerHUD.setHP(playerUnit.currentHp);
				}
				else
				{
					//dialogueText.text = enemyUnit.getName() + " atacks! ";
					isDead = playerUnit.TakeDamage(enemyUnit.getDamage());
					
					playerHUD.setHP(playerUnit.currentHp);
				}

				isPlayerProtecting = false;
				animator.SetBool("isAttacking", false);
				yield return new WaitForSeconds(1f);


				if (isDead)
				{
					battleState = BattleState.Lost;
					
					StartCoroutine(EndBattle());
				}
				else
				{
					battleState = BattleState.PlayerTurn;
					PlayerTurn();
				}
			}
			else if (random == 0)
			{
				if (isPlayerProtecting) isPlayerProtecting = false;

				yield return new WaitForSeconds(1f);
				if (EnemyHeal())
				{
					animator.SetBool("isHealing", true);
					dialogueText.text = enemyUnit.getName() + "trying to heal up";
					yield return new WaitForSeconds(1f);
					animator.SetBool("isHealing", false);
					battleState = BattleState.PlayerTurn;
					PlayerTurn();

				}
				else
				{
					dialogueText.text = enemyUnit.getName() + "trying to heal but all potion are broken!";
					yield return new WaitForSeconds(1f);
					battleState = BattleState.PlayerTurn;
					PlayerTurn();
				}

			}
		}
		else
        {
			dialogueText.text = enemyUnit.getName() + " atacks! ";
			animator.SetBool("isAttacking", true);
			yield return new WaitForSeconds(1f);

			bool isDead;
			if (isPlayerProtecting)
			{
				dialogueText.text = enemyUnit.getName() + " atacks, but " + playerUnit.getName() + " is protecting!";
				isDead = playerUnit.TakeDamage(enemyUnit.getDamage() / 2);
				playerHUD.setHP(playerUnit.currentHp);
			}
			else
			{
				isDead = playerUnit.TakeDamage(enemyUnit.getDamage());
				playerHUD.setHP(playerUnit.currentHp);
			}

			isPlayerProtecting = false;
			animator.SetBool("isAttacking", false);
			yield return new WaitForSeconds(1f);

			if (isDead)
			{
				battleState = BattleState.Lost;
				
				StartCoroutine(EndBattle());
			}
			else
			{
				battleState = BattleState.PlayerTurn;
				
				PlayerTurn();
			}
		}

    }


	IEnumerator BattleRun()
    {
		if (playerUnit.currentHp >= playerUnit.getMAXHP() / 2)
		{
			dialogueText.text = playerUnit.getName() + " are u running ?!";
			yield return new WaitForSeconds(1f);
			battleState = BattleState.Lost;
			PlayerPrefs.SetString("BattleState", battleState.ToString());
			SceneManager.LoadScene("MainScene");
		}
		else
        {
			dialogueText.text = playerUnit.getName() + " you cant run, you are too hurt";
			runButton.interactable = false;
			yield return new WaitForSeconds(1f);
			PlayerTurn();

        }
	}



	private void PlayerTurn()
    {
		playerButtons.SetActive(true);
		dialogueText.text = playerUnit.getName() + " what are you going to do: ";
    }

	public void OnAtackButton()
    {
		if (battleState != BattleState.PlayerTurn)
			return;

		StartCoroutine(PlayerAtack());
    }

	public void OnHealButton()
    {
		if (battleState != BattleState.PlayerTurn)
			return;

		StartCoroutine(PlayerHeal());
    }

	public void OnProtectButton()
    {
		if (battleState != BattleState.PlayerTurn)
			return;

		StartCoroutine(PlayerProtect());
    }

	public void OnRunButton()
    {
		// load main scene
		if (battleState != BattleState.PlayerTurn)
			return;

		StartCoroutine(BattleRun());
		//BattleRun();
    }

	IEnumerator EndBattle()
    {
		if(battleState == BattleState.Won)
        {
			dialogueText.text = "You won the battle";
			PlayerPrefs.SetString("BattleState", battleState.ToString());
			PlayerPrefs.SetInt("MustBeDeleted" + enemyUnit.idOnScene.ToString(), enemyUnit.idOnScene);
			if(enemyUnit.getId() == 3)
            {
				PlayerPrefs.SetInt("PresetDrop", 1);
			}
			if (enemyUnit.getId() == 4 || enemyUnit.getId() == 7 || enemyUnit.getId() == 8 || enemyUnit.getId() == 9)
			{
				PlayerPrefs.SetString("BossWasDefeated", "Yes");
			}
			if(enemyUnit.getId() == 12)
            {
				PlayerPrefs.SetInt("MotherWasDefeated", 1);
            }
			PlayerPrefs.Save();
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("MainScene");
        }
		else if(battleState == BattleState.Lost)
        {
			dialogueText.text = "You lost, go back and become stronger losser";
			PlayerPrefs.SetString("BattleState", battleState.ToString());
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("MainScene");
		}
    }

}
