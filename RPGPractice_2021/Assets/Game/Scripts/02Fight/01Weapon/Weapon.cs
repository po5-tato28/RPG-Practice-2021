using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fight/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponPrefab = null;
    public AnimatorOverrideController weaponOverride = null;
    public float timeBetweenAttacks = 1f;
    public float attackRange = 1f;
    public float attackDamage = 1f;

    public WeaponType weaponType;
    public GameObject effectPrefab = null;
    public Vector3 effectPosition;
    public Vector3 effectRotation;

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
