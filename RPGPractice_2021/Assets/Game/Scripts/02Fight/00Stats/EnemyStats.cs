using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Enemy")]
public class EnemyStats : ScriptableObject
{
    public CommonStats commonStats = null;

    public float moveSpeed = 1f;
    public float lookRange = 40f;
    public float lookSphereCastRadius = 1f;

    public float attackRange = 1f;
    public float attackRate = 1f;
    public float attackForce = 1f;
    public int attackDamage = 50;

    public float searchDuration = 4f;
    public float searchingTurnSpeed = 120f;

    public float dropExp = 10.0f;
    public GameObject[] dropItem = null;
}
