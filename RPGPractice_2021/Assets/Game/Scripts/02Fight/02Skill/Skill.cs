using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fight/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] AnimatorOverrideController skillOverride = null;
    [SerializeField] float coolTime = 1f;
    public float CoolTime { get { return coolTime; } }


    [SerializeField] int needMp;
    public int NeedMp { get { return needMp; } }


    [SerializeField] float attackDamage = 1f;
    public float AttackDamage { get { return attackDamage; } }

    [SerializeField] float attackRadius = 1f;
    public float AttackRadius { get { return attackRadius; } }

    [SerializeField] Vector3 attackPivot;
    public Vector3 AttackPivot { get { return attackPivot; } }


    Vector3 currentPivot;

    public SkillType skillType; //단일, 다수... 등
    public GameObject effectPrefab = null;
    public Vector3 effectPosition;
    public Vector3 effectRotation;


    public void ChangeAnimatorFoSkill(Animator animator)
    {
        if (skillOverride != null)
        {
            animator.runtimeAnimatorController = skillOverride;
        }
    }

    public Vector3 CalculatePivot(Vector3 distance)
    {
        currentPivot = PlayerCombat.Instance().center.position + distance;
        //Debug.Log(currentPivot);

        return currentPivot;
    }
}
