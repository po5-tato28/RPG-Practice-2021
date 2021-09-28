using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] PlayerCombat combat;

    public void ChangeButton()
    {
        combat.EquipWeapon(weapon);
    }
}
