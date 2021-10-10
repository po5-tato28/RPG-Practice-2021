using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] CommonStats common;

    int currentHp;
    public int CurrentHp { get { return currentHp; } }

    int maxHp;
    public int MaxHp { get { return maxHp; } }

    [SerializeField] bool isDead = false;

    float speed = 0.1f;

    // test��
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    private void Awake()
    {
        currentHp = common.currentHP;
        maxHp = common.GetMaxHP();
    }


    public bool IsDead()
    {
        return isDead;
    }
    public int GetHealthPoint()
    {
        return currentHp / common.GetMaxHP();
    }

    public void TakeDamage(float damage)
    {
        // �÷��̾��� ������ ������ �������� �� ī�޶� ����
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);

        //healthPoints = Mathf.Max(healthPoints - damage, 0);
        currentHp = (int)Mathf.Max(currentHp - damage, 0);

        //if (healthPoints == 0)
        if (currentHp == 0)
        {
            Die();
        }
        Debug.Log("hp :: " + currentHp);

        MoveBack();
        Attacked();
    }

    public void RecoverHealth(float damage)
    {
        if (currentHp >= 100) return;

        currentHp = (int)Mathf.Max(currentHp + damage, 10);
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

        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }


}
