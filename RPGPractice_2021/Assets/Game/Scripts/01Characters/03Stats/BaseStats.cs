using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int startingLevel = 1;
    [SerializeField] CharacterType characterType;
    [SerializeField] CharacterStats characterStats;
    // [SerializeField] GameObject levelUpParticleEffect = null;

    public event Action onLevelUp;

    int currentLevel = 1;

    PlayerExp exp;

    private void Awake()
    {
        exp = GetComponent<PlayerExp>();        
    }
    private void OnEnable()
    {
        currentLevel = CalculateLevel();

        if (exp == null) return;
        exp.onTakeExp += UpdateLevel;
    }

    private void OnDisable()
    {
        if (exp == null) return;
        exp.onTakeExp -= UpdateLevel;
    }

    public int GetStat(StatsType stat)
    {
        //Debug.Log(characterStats.GetStats(stat, characterType, GetCurrentLevel()));
        //return (GetBaseStat(list) + GetAdditiveModifier(list)) * (1 + GetPercentageModifier(list) / 100);
        return characterStats.GetStats(stat, characterType, GetCurrentLevel());
    }

    private int GetBaseStat(StatsType stat)
    {
       
        return characterStats.GetStats(stat, characterType, GetCurrentLevel());
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }


    private void UpdateLevel()
    {
        // �ӽ� ������ ������ ������ ���� �� �ʱ�ȭ
        int tempLevel = CalculateLevel();

        // �ӽ� ������ ���� �������� ������ 
        if (tempLevel > currentLevel)
        {
            // ���� ���� = �ӽ� ���� (����)
            currentLevel = tempLevel;
            //LevelUpEffect();

            // onLevelUp �̺�Ʈ ����
            onLevelUp();

            exp.CurrentExp = 0;
        }
    }

    public int CalculateLevel()
    {
        //PlayerExp exp = GetComponent<PlayerExp>();

        // exp�� ��������� -> ���۷���(startingLevel) ��ȯ�� ����
        if (exp == null)
        {
            //Debug.Log(startingLevel);
            return startingLevel;
        }
    
        // ���� exp�� ������ �ӽú��� ���� �� �ʱ�ȭ
        float tempExp = exp.CurrentExp;

        // ��ü level�� ������ �ӽú��� ���� �� �ʱ�ȭ
        // GetLevels()�� ���� ���� ������ �޾ƿ�
        int tempLevels = characterStats.GetLevels(StatsType.ExpToLevelUp, characterType);
        // Debug.Log(tempLevel);

        // ��ü level ��ȯ
        for (int level = 1; level <= tempLevels; level++)
        {
            // �������� �ʿ��� exp (ExpToLevelUp)�� ������ �ӽú��� ���� �� �ʱ�ȭ
            float tempExpToLevelUp = characterStats.GetStats(StatsType.ExpToLevelUp, characterType, level);

            // ����, �ʿ��� exp�� ���� exp���� ũ��
            if (tempExpToLevelUp > tempExp)
            {
                //Debug.Log("if level " + level);
                // ���� ��ȯ������ ������ ��ȯ�ϰ� ����
                return level;
            }
        }

        //Debug.Log("tempLevels" + (tempLevels + 1));
        // ���� ���� +1
        return tempLevels + 1;
    }

}
