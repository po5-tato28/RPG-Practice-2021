using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //public CommonStats common;
    [SerializeField] bool isDead = false;


    int maxHp;
    int currentHp;
    public int CurrentHp { get { return currentHp; } }

    Animator animator;


    private void OnEnable()
    {
        maxHp = GetInitialHealth();
        currentHp = maxHp;

        isDead = false;
    }

    public int GetInitialHealth()
    {
        return GetComponent<BaseStats>().GetStat(StatsType.Hp);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        // 플레이어의 공격이 적에게 적중했을 때 카메라 흔들기
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);

        //healthPoints = Mathf.Max(healthPoints - damage, 0);
        // common.CurrentHp = (int)Mathf.Max(common.CurrentHp - damage, 0);
        currentHp = Mathf.Max(currentHp - damage, 0);

        //if (healthPoints == 0)
        if (currentHp <= 0)
        {
            Die();
        }
        Debug.Log("hp :: " + currentHp);
        //Debug.Log("maxhp :: " + common.MaxHp);

        GetComponent<Animator>().SetTrigger("Hit");

        //MoveBack();
        //Attacked();
    }

    public void RecoverHealth(int damage)
    {
        if (currentHp >= 100) return;

        currentHp = Mathf.Max(currentHp + damage, 10);
    }


    private void Die()
    {
        if (isDead) return;

        Debug.Log("Die");
        isDead = true;

        GetComponent<Animator>().SetTrigger("Dead");
        //this.gameObject.SetActive(false);
        //GetComponent<Animator>().SetTrigger("die");
        //GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    
    public float GetHpValue()
    {
        return ((float)currentHp / (float)GetInitialHealth());
    }
    
}
