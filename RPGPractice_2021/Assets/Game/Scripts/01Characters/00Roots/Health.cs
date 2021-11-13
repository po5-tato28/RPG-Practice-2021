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
        // maxHp = GetInitialHealth();
        currentHp = GetInitialHealth();

        isDead = false;

        GetComponent<BaseStats>().onLevelUp += RecoverHealth;
    }

    private void OnDisable()
    {
        GetComponent<BaseStats>().onLevelUp -= RecoverHealth;
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
        //Debug.Log("hp :: " + currentHp);

        GetComponent<Animator>().SetTrigger("Hit");

        //MoveBack();
        //Attacked();
    }

    public void RecoverHealth()
    {
        if (currentHp >= GetInitialHealth()) return;

        int recoverHp = GetComponent<BaseStats>().GetStat(StatsType.Hp);

        currentHp = Mathf.Max(currentHp, recoverHp);
    }

    public void RecoverHealth(int point = 0)
    {
        if (currentHp >= GetInitialHealth()) return;

        int recoverHp = currentHp + point;

        currentHp = Mathf.Max(currentHp, recoverHp);
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
