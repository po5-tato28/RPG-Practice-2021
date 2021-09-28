using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fight/Skill")]
public class Skill : ScriptableObject
{
    public AnimatorOverrideController skillOverride = null;
    public float coolTime = 1f;
    public float attackDamage = 1f;
    public float attackRadius = 1f;
    public Vector3 attackPivot;

    Vector3 currentPivot;

    public SkillType skillType; //단일, 다수... 등
    public GameObject effectPrefab = null;
    public Vector3 effectPosition;
    public Vector3 effectRotation;

    public void UseSkill(Animator animator)
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
