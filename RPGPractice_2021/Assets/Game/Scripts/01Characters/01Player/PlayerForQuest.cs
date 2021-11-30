using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForQuest : MonoBehaviour
{
    Health health;
    PlayerExp exp;

    public Quest quest;

    private void Awake()
    {
        health = GetComponent<Health>();
        exp = GetComponent<PlayerExp>();
    }

}
