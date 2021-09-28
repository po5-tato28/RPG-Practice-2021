using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkill : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerCombat playerCombat;
    [SerializeField] List<Skill> skill;
    [SerializeField] List<Button> button;

    private void Awake()
    {
        playerCombat = player.GetComponent<PlayerCombat>();
    }


    public void OnClickSkill(int index)
    {
        StartCoroutine(CoolTime(skill[index].coolTime, index));
        playerCombat.TriggerSkill(skill[index]);
    }

    IEnumerator CoolTime(float cool, int index)
    {
        print("��Ÿ�� �ڷ�ƾ ����");
        button[index].interactable = false;

        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        print("��Ÿ�� �ڷ�ƾ �Ϸ�");
        button[index].interactable = true;
    }
}
