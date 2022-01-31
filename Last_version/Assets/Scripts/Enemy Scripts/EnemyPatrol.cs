using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float WalkTime;
    [SerializeField] private float IdleTime;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Canvas buttleQuestionCanvas;
    private int movement;
    private float gotime;
    private float resttime;
   
    private Vector3[] walkDir = new Vector3[2]
    {
        new Vector3(0.25f,0,0),
        new Vector3(-0.25f,0,0)
    };
//    private Quaternion TurnAround = new Quaternion(0, 180, 0, 0);
	
    void Start()
    {
        gotime = 0;
        resttime = IdleTime;
        movement = 0;
    }

    void Update()
    {
        if (player.colliderForEnemy == null) 
        {
            resttime -= Time.deltaTime;
            gotime -= Time.deltaTime;


            if (resttime <= 0 && movement == 0)
            {
                movement = 1;
                gotime = WalkTime;
            }
            if (resttime <= 0 && movement == 2)
            {
                movement = 3;
                gotime = WalkTime;
                //           transform.rotation = TurnAround;
                enemySprite.flipX = true;

            }


            if (gotime > 0 && movement == 1)
            {
                transform.position += walkDir[0];
            }
            if (gotime <= 0 && movement == 1)
            {
                movement = 2;
                resttime = IdleTime;
            }

            if (gotime > 0 && movement == 3)
            {
                transform.position += walkDir[1];
            }
            if (gotime <= 0 && movement == 3)
            {
                movement = 0;
                resttime = IdleTime;
                enemySprite.flipX = false;
                //            transform.rotation = TurnAround;
            }
        }
        else if(player.colliderForEnemy != null && !buttleQuestionCanvas.enabled)
        {
            resttime -= Time.deltaTime;
            gotime -= Time.deltaTime;


            if (resttime <= 0 && movement == 0)
            {
                movement = 1;
                gotime = WalkTime;
            }
            if (resttime <= 0 && movement == 2)
            {
                movement = 3;
                gotime = WalkTime;
                //           transform.rotation = TurnAround;
                enemySprite.flipX = true;

            }


            if (gotime > 0 && movement == 1)
            {
                transform.position += walkDir[0];
            }
            if (gotime <= 0 && movement == 1)
            {
                movement = 2;
                resttime = IdleTime;
            }

            if (gotime > 0 && movement == 3)
            {
                transform.position += walkDir[1];
            }
            if (gotime <= 0 && movement == 3)
            {
                movement = 0;
                resttime = IdleTime;
                enemySprite.flipX = false;
                //            transform.rotation = TurnAround;
            }
        }
        else
        {
            if (player.colliderForEnemy.gameObject.GetComponent<Unit>().idOnScene == this.gameObject.GetComponent<Unit>().idOnScene) // here can also be == or !=
            {
                return;
            }
        }
    }
}
