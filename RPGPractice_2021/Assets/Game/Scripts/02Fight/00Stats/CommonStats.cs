using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Common")]
public class CommonStats : ScriptableObject
{
    [SerializeField] private int maxHp = 100;
    public int MaxHp { get { return maxHp; } }

    [SerializeField] private int currentHp = 100;
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
}
