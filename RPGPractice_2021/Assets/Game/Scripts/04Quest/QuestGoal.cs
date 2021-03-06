using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GoalType
{
    Kill,
    Gathering,
}

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount = 10;
    public int currentAmount = 0;


    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if(goalType == GoalType.Kill)
        {
            currentAmount++;
        }
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }
}
