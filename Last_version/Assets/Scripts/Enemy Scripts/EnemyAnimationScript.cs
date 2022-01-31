using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    [SerializeField] private Sprite[] enemyAnimations;
    [SerializeField] private SpriteRenderer enemyGfx;
    [SerializeField] private float animationDelay;
    private float timer;
    int frame = 0;

    private void Start()
    {
        timer = animationDelay;
    }

    private void Update()
    {
        PlayAnimations(enemyAnimations);
    }

    private void PlayAnimations(Sprite[] whichAnimation)
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            frame += 1;
            if (frame >= enemyAnimations.Length)
            {
                frame = 0;
            }

            enemyGfx.sprite = whichAnimation[frame];
            timer = animationDelay;
        }
    }

}
