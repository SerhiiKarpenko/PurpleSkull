using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemySprite;
    private Unit enemyUnit;

    private void Start()
    {
        enemyUnit = gameObject.GetComponent<Unit>();
        enemySprite.sprite = enemyUnit.getSprite();
    }

    

}
