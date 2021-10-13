using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] CommonStats common;
    [SerializeField] HpBar hpbar = null;

    [SerializeField] bool isDead = false;

    float speed = 0.1f;

    // test용
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    private void OnEnable()
    {
        common.CurrentHp = common.MaxHp;
    }

    private void Start()
    {
    }

    public bool IsDead()
    {
        return isDead;
    }

    public int GetHpValue()
    {
        return (common.CurrentHp / common.MaxHp);
    }

    public void TakeDamage(float damage)
    {
        // 플레이어의 공격이 적에게 적중했을 때 카메라 흔들기
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);

        //healthPoints = Mathf.Max(healthPoints - damage, 0);
        common.CurrentHp = (int)Mathf.Max(common.CurrentHp - damage, 0);

        //if (healthPoints == 0)
        if (common.CurrentHp == 0)
        {
            Die();
        }
        Debug.Log("hp :: " + common.CurrentHp);

        MoveBack();
        Attacked();
    }

    public void RecoverHealth(float damage)
    {
        if (common.CurrentHp >= 100) return;

        common.CurrentHp = (int)Mathf.Max(common.CurrentHp + damage, 10);
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
        Debug.Log("Attacked");

        hpbar.SetSliderValue(common.CurrentHp, common.MaxHp);

        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }
}
