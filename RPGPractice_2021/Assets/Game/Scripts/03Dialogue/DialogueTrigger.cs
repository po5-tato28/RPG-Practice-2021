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
                // npc ī�޶� �ѱ�
                currentNpc.EnableNpcCamera();
                // npc �ִϸ��̼�
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


    // TryDialogue �̺�Ʈ
    public void TriggerDialogue()
    {
        var _type = currentNpc.GetNpcDialogueType();
        var _list = list.GetDialogueContainer(_type);

        manager.StartDialogue(_list);
    }

    // NpcController.ReadyToTriggerDialogue �̺�Ʈ
    public void SetNpc()
    {
        // �ӽ� ����Ʈ ����
        tempNpcList = FindObjectsOfType<NpcController>().ToList();

        var player = FindObjectOfType<PlayerController>();

        // LINQ �޼ҵ带 �̿��� ���� ����� ���� ã�´�.
        var neareastNpc = tempNpcList.OrderBy(obj =>
            {
                return Vector3.Distance(player.transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        currentNpc = neareastNpc;
    }

    // NpcController.ExitToTriggerDialogue �̺�Ʈ
    public void ResetNpc()
    {
        currentNpc = null;
    }
}
