using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueList list;
    [SerializeField] DialogueManager manager;

    [SerializeField] NpcController currentNpc;
    private List<NpcController> tempNpcList;

    public UnityEvent TryDialogue;


    private void Update ()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (currentNpc == null) return;

        if (currentNpc.GetIsPossibleToTalk())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //TriggerDialogue((int)DialogueType.FirstTime);
                TryDialogue.Invoke();
                // npc 카메라 켜기
                currentNpc.EnableNpcCamera();
                // npc 애니메이션
                currentNpc.GetComponent<Animator>().SetBool("Talking", true);
            }
        }
    }

    public NpcController GetCurrentNPC()
    {
        if(currentNpc == null)
        {
            return null;
        }

        return currentNpc;
    }


    // TryDialogue 이벤트
    public void TriggerDialogue()
    {
        var _type = currentNpc.GetNpcDialogueType();
        var _list = list.GetDialogueContainer(_type);

        manager.StartDialogue(_list);
    }

    // NpcController.ReadyToTriggerDialogue 이벤트
    public void SetNpc()
    {
        // 임시 리스트 갱신
        tempNpcList = FindObjectsOfType<NpcController>().ToList();

        var player = FindObjectOfType<PlayerController>();

        // LINQ 메소드를 이용해 가장 가까운 적을 찾는다.
        var neareastNpc = tempNpcList.OrderBy(obj =>
            {
                return Vector3.Distance(player.transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        currentNpc = neareastNpc;
    }

    // NpcController.ExitToTriggerDialogue 이벤트
    public void ResetNpc()
    {
        currentNpc = null;
    }
}
