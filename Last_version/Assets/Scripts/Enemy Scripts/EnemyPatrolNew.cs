using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolNew : MonoBehaviour
{

    [SerializeField] Vector3 lastPoint;
    [SerializeField] Vector3 firstPoint;
    private Vector3[] walkDir = new Vector3[2]
    {
        new Vector3(0.25f,0,0),
        new Vector3(-0.25f,0,0)
    };
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Canvas buttleQuestionCanvas;
    bool isInPoint = true;

    [SerializeField] private float waitTimeBeforStart;
    [SerializeField] private float waitTime;

    private void Start()
    {
        waitTimeBeforStart = waitTime;
    }


    void Update()
    {
        if (player.colliderForEnemy == null)
        {
            if (isInPoint)
                StartCoroutine(PatrolToLastPoint());
            else
                StartCoroutine(PatrolToFirstPoint());
        }
        else if (player.colliderForEnemy != null && !buttleQuestionCanvas.enabled)
        {
            if (isInPoint)
                StartCoroutine(PatrolToLastPoint());
            else
                StartCoroutine(PatrolToFirstPoint());
        }
        else
        {
            if (player.colliderForEnemy.gameObject.GetComponent<Unit>().idOnScene == this.gameObject.GetComponent<Unit>().idOnScene) // here can also be == or !=
            {
                return;
            }
        }


    }



    IEnumerator PatrolToLastPoint()
    {
        if (PlayerPrefs.HasKey("ReadyToGo"))
        {
            if (gameObject.transform.position.x >= lastPoint.x)
            {
                yield return new WaitForSeconds(waitTime);
                isInPoint = false;
                enemySprite.flipX = true;
                //yield return new WaitForSeconds(waitTime);
                StopCoroutine(PatrolToFirstPoint());
            }
            else
            {
                transform.position += walkDir[0];
            }
        }
        else
        {
            yield return new WaitForSeconds(waitTimeBeforStart);
            PlayerPrefs.SetInt("ReadyToGo", 1);
        }

    }

    IEnumerator PatrolToFirstPoint()
    {

        if (gameObject.transform.position.x <= firstPoint.x)
        {
            yield return new WaitForSeconds(waitTime);
            isInPoint = true;
            enemySprite.flipX = false;
            //yield return new WaitForSeconds(waitTime);
            StopCoroutine(PatrolToLastPoint());
        }
        else
        {
            transform.position += walkDir[1];
        }

    }
}
