using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolderScript : MonoBehaviour
{
	public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < enemies.Count; ++i)
        {
            PlayerPrefs.SetInt("OdOnSceneOfEnemies" + enemies[i].GetComponent<Unit>().idOnScene.ToString(), 
                enemies[i].GetComponent<Unit>().idOnScene 
                );


        }

        
    }
}
