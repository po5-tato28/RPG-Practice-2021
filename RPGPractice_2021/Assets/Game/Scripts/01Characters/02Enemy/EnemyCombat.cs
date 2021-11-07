using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    [SerializeField] EnemyStats enemyStats;

    public Health player;

    private void Update()
    {
        if (player.IsDead()) return;

        if (GetIsInRange(player.transform))
        {
            animator.SetTrigger("Attack");
        }
    }


    public override void Hit()
    {
        player.TakeDamage((int)enemyStats.AttackDamage);
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
        throw new System.NotImplementedException();
    }

    public override bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) < enemyStats.AttackRange;
    }
}
