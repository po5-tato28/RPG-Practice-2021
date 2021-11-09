using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Stats/Characters", order = 0)]
public class CharacterStats : ScriptableObject
{
    [SerializeField] CharacterTypeClass[] characterClasses = null;

    [System.Serializable]
    class CharacterTypeClass
    {
        public CharacterType characterType;
        // stat 클래스
        public StatsListClass[] stats = null;
    }

    [System.Serializable]
    class StatsListClass
    {
        public StatsType statsType;
        public int[] levels = null;
    }


    // =========================================

    // dictionary로 저장 < 캐릭터 타입, < 스탯, 레벨 > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<CharacterType, Dictionary<StatsType, int[]>> lookupTable = null;

    // 외부에서 스탯 접근
    public int GetStats(StatsType stat, CharacterType type, int lv)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupTable();

        // 전달받은 매개변수의 값을 임시변수 levels 배열에 저장
        int[] levels = lookupTable[type][stat];

        // 만약 levels의 길이가 입력받은 lv보다 짧으면
        if (levels.Length < lv)
        {
            // 0을 반환하고 종료 -> 잘못된 것이므로 예외처리
            return 0;
        }

        // levels에서 [lv-1] 인덱스의 값을 반환
        return levels[lv - 1] ;
    }



    public int GetLevels(StatsType stat, CharacterType type)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupTable();

        // 전달받은 매개변수의 값을 임시변수 levels 배열에 저장
        int[] levels = lookupTable[type][stat];

        // levels 배열의 길이를 반환
        // 현재 레벨 = 배열의 길이... (stat이 key이고 level이 value임.)
        return levels.Length;
    }


    void SetupTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (lookupTable != null) return;

        lookupTable = new Dictionary<CharacterType, Dictionary<StatsType, int[]>>();
        
        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (CharacterTypeClass character in characterClasses)
        {
            // stat을 임시로 저장할 컨테이너를 만든다
            var statLookupTable = new Dictionary<StatsType, int[]>();

            // type에 맞는 stat 클래스를 순회한다 (type 클래스가 stat 클래스를 포함한다)
            foreach (StatsListClass statsValue in character.stats)
            {
                // stat 컨테이너에 stat에 따른 레벨별 값(=levels)을 저장한다.
                statLookupTable[statsValue.statsType] = statsValue.levels;
            }

            // stat 클래스 순회가 끝나면 전체 컨테이너에 type에 따른 stat 컨테이너를 저장한다.
            lookupTable[character.characterType] = statLookupTable;
        }
    }
}
