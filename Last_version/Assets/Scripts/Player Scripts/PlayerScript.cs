using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// every get and set metods can be re-writed on {get; (modifire) set}
// selectionBase pomaga od razu wybrac caly object 
[SelectionBase]
public class PlayerScript : MonoBehaviour
{
	public int forCutSceneIndex;
	

	[Header("Delay's")]
	[SerializeField] private float movementDelay = 0.2f;
	[SerializeField] private float animationDelay = 0.5f;


	[Header("Animations")]
	[SerializeField] private Sprite[] forIdle;
	[SerializeField] private Sprite[] playerIdleAnimation;
	[SerializeField] private Sprite[] playerForwardMoveAnimation;
	[SerializeField] private Sprite[] playerRightMoveAnimation;
	[SerializeField] private Sprite[] playerLeftMoveAnimation;
	[SerializeField] private Sprite[] playerBackMoveAnimation;
	[SerializeField] private SpriteRenderer gfx;
	private Sprite[][] animationsArrays = new Sprite[5][];

	[Header("Canvas")]
	[SerializeField] private Canvas battleQuestion;
	//	public Equipment[] equipment;
	[SerializeField] private PlayerEqupment equip;
	[SerializeField] private InventorySlot keySlot;
	//public PlayerEquipmentSlot PlayerEquipmentSlot;
	//public InventorySlot[] slotsForEquipment;
	public Collider colliderForEnemy = null;
	public Collider[] colliders;
	private float timer;
	private float OldDelay;
	private float animationFrame = 0;
	/*
	 *    0
	 *    |
	 * 3--+--1
	 *    |
	 *    2 
	 */
	private int lookDir = 0;
	private int animtaionFrame = 0;
	public Vector3[] walkDir = new Vector3[4]
	{
		new Vector3(0, 0, 0.25f),
		new Vector3(0.25f, 0, 0),
		new Vector3(0, 0, -0.25f),
		new Vector3(-0.25f, 0, 0)
	};
	private Vector3[] walkDirFloat = new Vector3[4]
	{
		new Vector3(0, 0, 0.25f),
		new Vector3(0.25f, 0, 0),
		new Vector3(0, 0, -0.25f),
		new Vector3(-0.25f, 0, 0)
	};

	private Vector3[] hitboxDir = new Vector3[4]
	{
		new Vector3(0, 0, 0.3f),
		new Vector3(0.3f, 0, 0),
		new Vector3(0, 0, -0.3f),
		new Vector3(-0.3f, 0, 0)
	};

	public Vector3[] runDir = new Vector3[4]
	{
		new Vector3(0, 0, 1),
		new Vector3(1, 0, 0),
		new Vector3(0, 0, -1),
		new Vector3(-1, 0, 0)

	};

	private bool isWalking = false;
	private bool isMMapOn = false;
	public bool isStandingOnCutScene = false;
	[SerializeField] private bool isMenuShowed = false;

	[Header("For Debug")]
	[SerializeField] private EnemyHolderScript holderScript;

	[Header("map changer")]
	[SerializeField] private mapChanger mapchanger;

	[Header("Minimap")]
	[SerializeField] private Canvas MMap;

	[Header("In game memnu canvas")]
	[SerializeField] private Canvas inGameMenuCanvas;



	public int a { get; set; }
	

	private void Awake()
	{
		timer = movementDelay;
		
	}

	private void Start()
	{
		OldDelay = animationDelay;
		animationsArrays[0] = playerIdleAnimation;
		animationsArrays[1] = playerForwardMoveAnimation;
		animationsArrays[2] = playerRightMoveAnimation;
		animationsArrays[3] = playerBackMoveAnimation;
		animationsArrays[4] = playerLeftMoveAnimation;
		if(!PlayerPrefs.HasKey("Started"))
			PlayerPrefs.DeleteAll();
    }


    void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			if (!IfEnemyColliding())
			{
				if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
				{
					animationDelay = movementDelay;
					isWalking = true;
					lookDir = 0;
					if (!isColiding() || colliders[0].gameObject.GetComponent<PickUp>() != null || colliders[0].gameObject.GetComponent<catSceneScript>() != null)
					{
						OnArrowsClick(lookDir, 1);
					}
				}

				if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
				{
					animationDelay = movementDelay;
					isWalking = true;
					lookDir = 1;
					if (!isColiding() || colliders[0].gameObject.GetComponent<PickUp>() != null || colliders[0].gameObject.GetComponent<catSceneScript>() != null)
					{
						OnArrowsClick(lookDir, 2);
					}
				}
				else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
				{
					animationDelay = movementDelay;
					isWalking = true;
					lookDir = 3;
					if (!isColiding() || colliders[0].gameObject.GetComponent<PickUp>() != null || colliders[0].gameObject.GetComponent<catSceneScript>() != null)
					{
						OnArrowsClick(lookDir, 4);
					}
				}

				if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
				{
					animationDelay = movementDelay;
					isWalking = true;
					lookDir = 2;
					if (!isColiding() || colliders[0].gameObject.GetComponent<PickUp>() != null || colliders[0].gameObject.GetComponent<catSceneScript>() != null)
					{
						OnArrowsClick(lookDir, 3);
					}
				}

				

				// for animations 
				if (isWalking && /*animtaionFrame >= 0*/ !Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.RightArrow) || !Input.GetKey(KeyCode.DownArrow) || !Input.GetKey(KeyCode.LeftArrow))
				{
					isWalking = false;
					animationDelay = OldDelay;
				}

				if (!isWalking)
				{
					PlayAnimations(animationsArrays[0]);

					//if (lookDir == 0 && !isWalking)
					//	gfx.sprite = forIdle[0];
					//else if (lookDir == 1 && ! isWalking)
					//	gfx.sprite = forIdle[1];
					//else if (lookDir == 2 && !isWalking)
					//	gfx.sprite = forIdle[2];
					//else if (lookDir == 3 && ! isWalking)
					//	gfx.sprite = forIdle[3];
					
				}

				//Debug.Log(isWalking);
			}
		}

		
		
		// cheking if colliding with enemy
		//IfEnemyColliding();
		if(Input.GetKey(KeyCode.Q))
        {
			Debug.Log(IfEnemyColliding());
        }

		// alignment of player position
//		PlayerPostionAligment();

		//for debug
		ForDebug();

		if (Input.GetKey(KeyCode.E))
		{
			// Application.Quit();
			PlayerPrefs.DeleteAll();
		}

		if (Input.GetKey(KeyCode.L))
		{
			Debug.Log(keySlot.item.name);
		}

        if (Input.GetKeyDown(KeyCode.O) && !mapchanger.isMapHell)
        {
            mapchanger.mapChangeToHell();
        }
        else if (Input.GetKeyDown(KeyCode.O) && mapchanger.isMapHell)
        {
            mapchanger.mapChangeToSimple();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && isMMapOn == false)
		{
			MMap.enabled = true;
			isMMapOn = true;
		}
		else if (Input.GetKeyDown(KeyCode.Tab) && isMMapOn == true)
		{
			MMap.enabled = false;
			isMMapOn = false;
		}

		if(Input.GetKeyDown(KeyCode.Escape))
        {
			//show in game menu
			isMenuShowed = !isMenuShowed;
			inGameMenuCanvas.enabled = !inGameMenuCanvas.enabled;
        }


	}

	private void OnArrowsClick(int lookDir, int whichAnimation)
    {
		//if (!colliders[0].gameObject.GetComponent<catSceneScript>().isCatSceneShowing)
		//{
		//	return;
		//}
		//else
		//      {
		//	transform.position += walkDirFloat[lookDir];
		//	timer = 0.01f;
		//	PlayAnimations(animationsArrays[whichAnimation]);
		//}

		if (!isMenuShowed)
		{
			if (!isStandingOnCutScene)
			{

				transform.position += walkDirFloat[lookDir];
				timer = 0.01f;
				PlayAnimations(animationsArrays[whichAnimation]);
			}
		}

	}



    public bool isColiding()
	{
		var coll = Physics.OverlapBox(
				gameObject.transform.position + Vector3.one * 0.5f + hitboxDir[lookDir],
				transform.localScale / 4,
				Quaternion.identity
		);

		if (coll.Length > 0)
			colliders = coll;
		else
			colliders = null;

		if (coll.Length != 0 && coll[0].name == "Enemy")
			colliderForEnemy = coll[0];
		else if (coll.Length != 0 && (coll[0].name == "EnemyGuardian1" || coll[0].name == "EnemyGuardian2" || coll[0].name == "EnemyGuardian3" || coll[0].name == "EnemyGuardian4"))
			colliderForEnemy = coll[0];
		else if (coll.Length != 0 && coll[0].name != "Enemy" && (coll[0].name != "EnemyGuardian1" || coll[0].name != "EnemyGuardian2" || coll[0].name != "EnemyGuardian3" || coll[0].name != "EnemyGuardian4"))
			colliderForEnemy = null;


		return coll.Length > 0;  
	}

	private void PlayAnimations(Sprite[] whichAnimation)
	{
		if (Time.timeSinceLevelLoad - animationFrame > animationDelay)
		{
			animtaionFrame += 1;
			if (animtaionFrame >= whichAnimation.Length)
			{
				animtaionFrame = 0;
			}
			gfx.sprite = whichAnimation[animtaionFrame];
			animationFrame = Time.timeSinceLevelLoad;
		}
	}

/*	private void PlayerPostionAligment()
	{
		transform.position = new Vector3(
				Mathf.Floor(transform.position.x),
				Mathf.Floor(transform.position.y),
				Mathf.Floor(transform.position.z)
			);
	}
*/
	private bool IfEnemyColliding()
	{
		if (isColiding() && colliderForEnemy != null)
		{
			if (colliderForEnemy.name == "Enemy")
			{
				// show battle entering stage
				battleQuestion.enabled = true;
				return true;
			}
			else if (colliderForEnemy.name == "EnemyGuardian1" && !keySlot.isEmpty && keySlot.item.name == "Wolf Trophy")
			{
				battleQuestion.enabled = true;
				return true;
			}
			else if(colliderForEnemy.name == "EnemyGuardian2" && !keySlot.isEmpty && keySlot.item.name == "Bug Head")
            {
				battleQuestion.enabled = true;
				return true;
			}
			else if (colliderForEnemy.name == "EnemyGuardian3" && !keySlot.isEmpty && keySlot.item.name == "WOrm")
			{
				battleQuestion.enabled = true;
				return true;
			}
			else if (colliderForEnemy.name == "EnemyGuardian4" && !keySlot.isEmpty && keySlot.item.name == "Heart of the Desert")
			{
				battleQuestion.enabled = true;
				return true;
			}
			else
            {
				colliderForEnemy = null;
				return false;
            }
		}
		else
		{
			battleQuestion.enabled = false;
			return false;
		}
	}

	public GameObject GetEnemyObject()
    {
		return colliderForEnemy.gameObject;
    }

	public int getLookDir()
	{
		return lookDir;
	}

	public void setLookDir(int LookDir)
	{
		lookDir = LookDir;
	}

    public Collider GetCollider()
    {
		return colliders[0];
	}

	public Vector3 getPlayerPosition()
    {
		return transform.position;
    }

	private void ForDebug()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var coll = Physics.OverlapBox(
				gameObject.transform.position + Vector3.one * 0.5f + hitboxDir[lookDir],
				transform.localScale / 4,
				Quaternion.identity
			);

			if (coll.Length > 0)
			{
				Debug.Log(coll[0].gameObject.name);
			}
		}
	}

		//private void OnDrawGizmos()
		//{
		//	Gizmos.color = isColiding() ? Color.red : Color.green;
		//	Gizmos.DrawCube(
		//		gameObject.transform.position + Vector3.one * 0.5f + hitboxDir[lookDir],
		//		transform.localScale / 4
		//	);
		//
		//	Gizmos.color = Color.red;
		//	Gizmos.DrawRay(
		//		transform.position + Vector3.up + Vector3.one * 0.5f,
		//		walkDirFloat[lookDir]
		//	);
		//}
}
