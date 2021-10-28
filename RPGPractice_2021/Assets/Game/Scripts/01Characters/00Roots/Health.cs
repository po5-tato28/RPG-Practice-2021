using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public CommonStats common;

    [SerializeField] bool isDead = false;

    int cH;
    public int CH { get { return cH; } }

    float speed = 0.1f;

    // test��
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    private void OnEnable()
    {
        cH = common.MaxHp;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        // �÷��̾��� ������ ������ �������� �� ī�޶� ����
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);

        //healthPoints = Mathf.Max(healthPoints - damage, 0);
        // common.CurrentHp = (int)Mathf.Max(common.CurrentHp - damage, 0);
        cH = Mathf.Max(cH - damage, 0);

        //if (healthPoints == 0)
        if (cH == 0)
        {
            Die();
        }
        Debug.Log("hp :: " + cH);
        //Debug.Log("maxhp :: " + common.MaxHp);

        MoveBack();
        Attacked();
    }

    public void RecoverHealth(int damage)
    {
        if (cH >= 100) return;

        cH = Mathf.Max(cH + damage, 10);
    }


    private void Die()
    {
        if (isDead) return;

        Debug.Log("Die");
        isDead = true;
        //this.gameObject.SetActive(false);
        //GetComponent<Animator>().SetTrigger("die");
        //GetComponent<ActionScheduler>().CancelCurrentAction();

        GameObject dead = Instantiate(deadEffect, new Vector3(0, 0.7f, 0), Quaternion.identity);
        dead.transform.SetParent(gameObject.transform, false);

        Destroy(dead, 3f);
    }

    public float GetHpValue()
    {
        return ((float)cH / (float)common.MaxHp);
    }


    /// test method��
    /// ���Ŀ� �� �Ű��� ��!!
    void MoveBack()
    {
        Vector3 move = new Vector3(0, 0, -10);
        this.gameObject.transform.Translate(move * speed);
    }

    void Attacked()
    {
        Debug.Log("Attacked");

        //hpbar.SetSliderValue(cH, common.MaxHp);

        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }
}
