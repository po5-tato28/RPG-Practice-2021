using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCombat combat;
    private Movement movement;

    private Health health;
    private PlayerMp mp;
    private PlayerExp exp;
    

    List<Health> enemys;

    int layerMarsks = 0;
    int buffLayerMarsks = 0;

    // test용

    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();

        enemys = new List<Health>();
    }

    private void Start()
    {
        layerMarsks = 1 << (LayerMask.NameToLayer("Enemy"));
        buffLayerMarsks = 1 << (LayerMask.NameToLayer("Player"));
        //layerMarsks |= 1 << LayerMask.NameToLayer("Player");
        //layerMarsks |= 1 << LayerMask.NameToLayer("Environment");
    }



    //private void OnDrawGizmos()
    //{
    //    if (combat.currentWeapon == null) return;
    //
    //    Gizmos.DrawWireSphere(combat.center.position, combat.currentWeapon.attackRange);
    //    //Debug.DrawRay(center.position, center.forward.normalized * weapon.attackRange, Color.cyan);
    //}


    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(combat.center.position, combat.currentWeapon.AttackRange, layerMarsks);


        //if (hitColliders.Length != 0)
        //{
        //    Debug.Log("hitColliders 뭔가 찾았습니다!");
        //    Debug.Log(hitColliders[0].gameObject);
        //}


        switch (combat.currentWeapon.WeaponType)
        {
            case WeaponType.Unarmed:
            case WeaponType.Bow:
                {
                    foreach (var hitHealth in hitColliders)
                    {
                        if (hitHealth.gameObject.GetComponent<Health>() != null
                            && hitHealth.gameObject.CompareTag("Enemy"))
                        {
                            if (combat.targets.Count <= 0
                                && hitHealth.gameObject.GetComponent<Health>().IsDead() == false
                                && hitHealth.gameObject.activeSelf)
                            {
                                combat.targets.Add(hitHealth.gameObject.GetComponent<Health>());
                            }

                            if (hitHealth.gameObject.GetComponent<Health>().IsDead() == true
                                || combat.GetIsInRange(hitHealth.gameObject.transform))
                            {
                                combat.targets.Remove(hitHealth.gameObject.GetComponent<Health>());
                            }
                        }
                    }
                }
                break;
            //case WeaponType.Unarmed:
            case WeaponType.TwoHandSword:
                {
                    foreach (var hitHealth in hitColliders)
                    {
                        if (hitHealth.gameObject.GetComponent<Health>() != null
                            && hitHealth.gameObject.CompareTag("Enemy"))
                        {
                            if (!combat.targets.Contains(hitHealth.gameObject.GetComponent<Health>())
                                && hitHealth.gameObject.GetComponent<Health>().IsDead() == false
                                && hitHealth.gameObject.activeSelf)
                            {
                                combat.targets.Add(hitHealth.gameObject.GetComponent<Health>());
                            }

                            if (hitHealth.gameObject.GetComponent<Health>().IsDead() == true
                                || combat.GetIsInRange(hitHealth.gameObject.transform))
                            {
                                combat.targets.Remove(hitHealth.gameObject.GetComponent<Health>());
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }

    }
    public void FindTargetWhenUseSkill(Vector3 distance)
    {
        combat.targets.Clear();

        Vector3 pivot = combat.currentSkill.CalculatePivot(distance);// + combat.centerHeight;        

        //if (skillColliders.Length != 0)
        //{
        //    Debug.Log("skillColliders 뭔가 찾았습니다!");
        //    Debug.Log(skillColliders[0].gameObject);
        //}

        switch (combat.currentSkill.skillType)
        {
            case SkillType.ATTACK_ONE:
                {
                    Collider[] skillColliders = Physics.OverlapSphere(pivot, combat.currentSkill.AttackRadius, layerMarsks);

                    foreach (var skillHealth in skillColliders)
                    {
                        if (skillHealth.gameObject.GetComponent<Health>() != null
                            && skillHealth.gameObject.CompareTag("Enemy"))
                        {
                            if (combat.targets.Count <= 0
                                && skillHealth.gameObject.GetComponent<Health>().IsDead() == false
                                && skillHealth.gameObject.activeSelf)
                            {
                                combat.targets.Add(skillHealth.gameObject.GetComponent<Health>());
                            }
                        }
                    }
                }
                break;
            case SkillType.ATTACK_MULTI:
                {
                    Collider[] skillColliders = Physics.OverlapSphere(pivot, combat.currentSkill.AttackRadius, layerMarsks);

                    foreach (var skillHealth in skillColliders)
                    {
                        if (skillHealth.gameObject.GetComponent<Health>() != null
                            && skillHealth.gameObject.CompareTag("Enemy"))
                        {
                            if (!combat.targets.Contains(skillHealth.gameObject.GetComponent<Health>())
                                && skillHealth.gameObject.GetComponent<Health>().IsDead() == false
                                && skillHealth.gameObject.activeSelf)
                            {
                                combat.targets.Add(skillHealth.gameObject.GetComponent<Health>());
                            }
                        }
                    }
                }
                break;
            case SkillType.ATTACK_ALL:
                {

                }
                break;
            case SkillType.BUFF_HEAL:
                {
                    combat.targets.Add(this.gameObject.GetComponent<Health>());
                }
                break;
        }
    }
}
