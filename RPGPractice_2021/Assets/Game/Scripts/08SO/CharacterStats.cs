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
        public StatsListClass[] stats = null;
    }

    [System.Serializable]
    class StatsListClass
    {
        public StatsList statsList;
        public int[] levels = null;
    }


    // =========================================

    Dictionary<CharacterType, Dictionary<StatsList, int[]>> lookupTable = null;

    public int GetStats(StatsList stat, CharacterType type, int lv)
    {
        foreach (CharacterTypeClass character in characterClasses)
        {
            if(character.characterType == type)
            {
                foreach(StatsListClass statsValue in character.stats)
                {
                    if (statsValue.statsList != stat) continue;

                    if (statsValue.levels.Length < lv) continue;

                    return statsValue.levels[lv - 1];
                }
            }
        }

        return 0;
    }
  
}
