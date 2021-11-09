using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Not use anymore.", true)]
//[CreateAssetMenu(menuName = "Stats/Enemy")]
public class EnemyStats : ScriptableObject
{
    public CommonStats commonStats = null;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float lookRange = 40f;
    [SerializeField] float lookSphereCastRadius = 1f;

    [SerializeField] float attackRange = 1f;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] float attackRate = 1f;
    [SerializeField] float attackForce = 1f;
    [SerializeField] int attackDamage = 50;
    public float AttackDamage { get { return attackDamage; } }

    [SerializeField] float searchDuration = 4f;
    [SerializeField] float searchingTurnSpeed = 120f;

    [SerializeField] int dropExp = 10;
    public int DropExp { get { return dropExp; } }

    [SerializeField] GameObject[] dropItem = null;
}
