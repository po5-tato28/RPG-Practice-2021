using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fight/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] AnimatorOverrideController weaponOverride = null;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float attackRange = 1f;

    public float AttackRange { get { return attackRange; } }

    [SerializeField] int attackDamage = 1;
    public int AttackDamage { get { return attackDamage; } }


    [SerializeField] WeaponType weaponType;
    public WeaponType WeaponType { get { return weaponType; } }

    [SerializeField] GameObject effectPrefab = null;
    public GameObject EffectPrefab { get { return effectPrefab; } }

    [SerializeField] Vector3 effectPosition;
    public Vector3 EffectPosition { get { return effectPosition; } }
    [SerializeField] Vector3 effectRotation;
    public Vector3 EffectRotation { get { return effectRotation; } }

    public void Spawn(Transform handTransform, Animator animator)
    {
        if (weaponPrefab != null)
        {
            Instantiate(weaponPrefab, handTransform);
        }

        if (weaponOverride != null)
        {
            animator.runtimeAnimatorController = weaponOverride;
        }
    }
}
