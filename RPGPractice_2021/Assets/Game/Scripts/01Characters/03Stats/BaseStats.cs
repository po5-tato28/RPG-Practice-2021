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
        // 임시 레벨을 저장할 변수를 선언 및 초기화
        int tempLevel = CalculateLevel();

        // 임시 레벨이 현재 레벨보다 높으면 
        if (tempLevel > currentLevel)
        {
            // 현재 레벨 = 임시 레벨 (전달)
            currentLevel = tempLevel;
            //LevelUpEffect();

            // onLevelUp 이벤트 실행
            onLevelUp();

            exp.CurrentExp = 0;
        }
    }

    public int CalculateLevel()
    {
        //PlayerExp exp = GetComponent<PlayerExp>();

        // exp가 비어있으면 -> 시작레벨(startingLevel) 반환후 종료
        if (exp == null)
        {
            //Debug.Log(startingLevel);
            return startingLevel;
        }
    
        // 현재 exp를 저장할 임시변수 생성 및 초기화
        float tempExp = exp.CurrentExp;

        // 전체 level을 저장할 임시변수 생성 및 초기화
        // GetLevels()를 통해 현재 레벨을 받아옴
        int tempLevels = characterStats.GetLevels(StatsType.ExpToLevelUp, characterType);
        // Debug.Log(tempLevel);

        // 전체 level 순환
        for (int level = 1; level <= tempLevels; level++)
        {
            // 레벨업에 필요한 exp (ExpToLevelUp)를 저장할 임시변수 생성 및 초기화
            float tempExpToLevelUp = characterStats.GetStats(StatsType.ExpToLevelUp, characterType, level);

            // 만약, 필요한 exp가 현재 exp보다 크면
            if (tempExpToLevelUp > tempExp)
            {
                //Debug.Log("if level " + level);
                // 현재 순환순서의 레벨을 반환하고 종료
                return level;
            }
        }

        //Debug.Log("tempLevels" + (tempLevels + 1));
        // 현재 레벨 +1
        return tempLevels + 1;
    }

}
