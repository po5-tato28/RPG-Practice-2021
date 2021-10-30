using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public CommonStats common;
    [SerializeField] bool isDead = false;

    int cH;
    public int CH { get { return cH; } }


    Animator animator;

    private void OnEnable()
    {
        cH = common.MaxHp;
        isDead = false;
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
        cH = Mathf.Max(cH - damage, 0);

        //if (healthPoints == 0)
        if (cH == 0)
        {
            Die();
        }
        Debug.Log("hp :: " + cH);
        //Debug.Log("maxhp :: " + common.MaxHp);

        GetComponent<Animator>().SetTrigger("Hit");

        //MoveBack();
        //Attacked();
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

        GetComponent<Animator>().SetTrigger("Dead");
        //this.gameObject.SetActive(false);
        //GetComponent<Animator>().SetTrigger("die");
        //GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    public float GetHpValue()
    {
        return ((float)cH / (float)common.MaxHp);
    }


    
}
