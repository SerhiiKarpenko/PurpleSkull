using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnBattleSceneScript : MonoBehaviour
{
    public UnitTemplate[] unitTemplates;
    public Unit unit;
    private int id;
    public bool isSet = false;
    public Animator animator;

   

    private void Start()
    {
        id = PlayerPrefs.GetInt("EnemyThatCollidingID");
        unit.unitTemplate = unitTemplates[id - 1];
        isSet = true;
        animator.SetInteger("whichEnemy", unit.unitTemplate.whichEnemy);
    }
}
