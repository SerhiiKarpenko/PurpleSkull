using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Unit Script")]
public class UnitTemplate : ScriptableObject
{
    public int id;
    public int maxHp;
    public int damage;
    public int lvl;
    public int healPower;
    public int MaxHealCountPerBattle;

    public Sprite sprite;

    public string unitName;

    [Header("Only for enemiesHolder")]
    public int whichEnemy;
}
