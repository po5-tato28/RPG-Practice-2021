using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    // public UnityEvent onEnemyDead;

    //GameObject target;
    Health health;
    NavMeshAgent agent;
    EnemyCombat combat;

    Vector3 targetPosition;

    //[SerializeField] EnemyStats enemyStats;
    BaseStats enemyStats;
    Animator animator;

    // test용
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    float speed = 0.1f;

    ItemDrop itemDrop;

    public Action OnEnemyDead;


    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<EnemyCombat>();

        enemyStats = GetComponent<BaseStats>();

        itemDrop = GetComponent<ItemDrop>();
    }

    private void OnEnable()
    {
        OnEnemyDead += PlayerForQuest.GetInstance().GoBattle;
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnDisable()
    {
        OnEnemyDead -= PlayerForQuest.GetInstance().GoBattle;
    }

    public void Attacked()
    {
        //Debug.Log("Attacked");

        // MoveBack()
        Vector3 move = new Vector3(0, 0, -10);
        this.gameObject.transform.Translate(move * speed);

        // attacked effect
        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0.5f, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }

    public void OnDead()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;

        // exp 받고
        combat.target.GetComponent<PlayerExp>().GainExp(enemyStats.GetStat(StatsType.ExpReward));
        // 타겟을 비운다
        combat.target = null;
        

        // dead effect
        GameObject dead = Instantiate(deadEffect, new Vector3(0, 0.7f, 0), Quaternion.identity);
        dead.transform.SetParent(gameObject.transform, false);

        itemDrop.Drop();

        Destroy(dead, 3f);

        // 적 사망 이벤트 실행
        OnEnemyDead.Invoke();

        // 비활성화
        Invoke("DisableEnemy", 2f);
    }

    void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}