using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    //[SerializeField] EnemyStats enemyStats;
    //BaseStats enemyStats;

    public Health target;
    Vector3 targetPosition;

    float attackRange = 2.5f;


    private void OnEnable()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead()) return;

        if (GetIsInRange(target.transform))
        {
            UpdateAnimator();
        }
    }


    public override void Hit()
    {
        if (target == null) return;

        target.TakeDamage((int)baseStats.GetStat(StatsType.Damage));
    }

    public override void WeaponEffect()
    {
        throw new System.NotImplementedException();
    }
    public override void DestroyWeaponEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateAnimator()
    {
        //targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        //transform.LookAt(targetPosition);

        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            TriggerAttack();

            timeSinceLastAttack = 0;
        }
    }

    public override bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) < attackRange;
    }
}
