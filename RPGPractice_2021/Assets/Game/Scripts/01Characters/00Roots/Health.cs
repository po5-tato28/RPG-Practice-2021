using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float currentHealthPoints;
    [SerializeField] CommonStats common;

    [SerializeField] bool isDead = false;

    float speed = 0.1f;

    // test용
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    private void Start()
    {
        currentHealthPoints = common.currentHP;
    }

    public bool IsDead()
    {
        return isDead;
    }
    public float GetHealthPoint()
    {
        return currentHealthPoints / common.GetMaxHP();
    }

    public void TakeDamage(float damage)
    {
        // 플레이어의 공격이 적에게 적중했을 때 카메라 흔들기
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);

        //healthPoints = Mathf.Max(healthPoints - damage, 0);
        currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0);

        //if (healthPoints == 0)
        if (currentHealthPoints == 0)
        {
            Die();
        }
        Debug.Log("hp :: " + currentHealthPoints);

        MoveBack();
        Attacked();
    }

    public void RecoverHealth(float damage)
    {
        if (currentHealthPoints >= 100) return;

        currentHealthPoints = Mathf.Max(currentHealthPoints + damage, 10);
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


    /// test method들
    /// 이후에 꼭 옮겨줄 것!!
    void MoveBack()
    {
        Vector3 move = new Vector3(0, 0, -10);
        this.gameObject.transform.Translate(move * speed);
    }

    void Attacked()
    {
        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }


}
