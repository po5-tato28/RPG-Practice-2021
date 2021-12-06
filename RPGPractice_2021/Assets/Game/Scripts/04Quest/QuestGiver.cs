using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerForQuest player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text itemText;


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);

        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        itemText.text = quest.itemReward.ToString();
    }

    public void AcceptQuest()
    {
        //questWindow.SetActive(false);
        quest.isActive = true;
        // give to player;
        player.quest = this.quest;
    }
}
