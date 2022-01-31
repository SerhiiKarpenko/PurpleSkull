using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	public Item item;
	public int id;
	public int idInSlot;
	public int number;
	public bool isDeleted = false;
	public bool isInInventory;
	public PlayerInventory playerInventory;
	[SerializeField] private MainSceneLoader mainSceneLoader;


    private void Start()
    {
		idInSlot = id;
		isInInventory = false;
		isDeleted = false;
    }

    //public SpriteRenderer sprite;

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerInventory>() != null)
		{
			bool isAded = other.GetComponent<PlayerInventory>().Add(id);
			//other.GetComponent<PlayerEQ>().item = item;
			if (isAded)
			{
				for(int i = 0; i < mainSceneLoader.itemsOnScenePositions.Count; i++)
                {
					if(mainSceneLoader.itemsOnScenePositions[i] == this.gameObject.transform.position)
                    {
						// delete spawn position
						mainSceneLoader.itemsOnScenePositions.Remove(mainSceneLoader.itemsOnScenePositions[i]);
						PlayerPrefs.DeleteKey("PlayerPositionForItemX" + i);
						PlayerPrefs.DeleteKey("PlayerPositionForItemY" + i);
						PlayerPrefs.DeleteKey("PlayerPositionForItemZ" + i);
						PlayerPrefs.DeleteKey("PlayerLookingDirectionForItem" + i);
						gameObject.SetActive(false);
                    }
                }
				gameObject.SetActive(false);
				isInInventory = true;
			}
		} 		
	}
}
