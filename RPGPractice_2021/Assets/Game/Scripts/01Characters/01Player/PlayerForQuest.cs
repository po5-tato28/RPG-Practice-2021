using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForQuest : MonoBehaviour
{
    public static PlayerForQuest instance;
    public static PlayerForQuest GetInstance()
    {
        return instance;
    }

    Health health;
    PlayerExp exp;

    public Quest quest;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        health = GetComponent<Health>();
        exp = GetComponent<PlayerExp>();
    }

    public void GoBattle()
    {
        if (quest.isActive)
        {
            quest.goal.EnemyKilled();

            if(quest.goal.IsReached())
            {
                // º¸»ó
                // exp.value += quest.experienceReward;
                // item.value += quest.itemReward;
                // quest.Complete();                
            }
        }
    }
}
