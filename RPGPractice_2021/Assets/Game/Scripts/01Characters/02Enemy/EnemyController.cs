using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    // public UnityEvent onEnemyDead;

    GameObject player;
    Health health;

    Vector3 targetPosition;

    //[SerializeField] EnemyStats enemyStats;
    BaseStats enemyStats;
    Animator animator;

    // test용
    public GameObject attackedEffect = null;
    public GameObject deadEffect = null;

    float speed = 0.1f;


    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        enemyStats = GetComponent<BaseStats>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        if (health.IsDead()) return;        

        targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(targetPosition);
    }

    /// test method들
    /// 이후에 꼭 옮겨줄 것!!
    public void MoveBack()
    {
        //
        //
    }

    public void Attacked()
    {
        Debug.Log("Attacked");

        // MoveBack()
        Vector3 move = new Vector3(0, 0, -10);
        this.gameObject.transform.Translate(move * speed);

        // attacked effect
        GameObject attacked = Instantiate(attackedEffect, new Vector3(0, 0.5f, 0.7f), Quaternion.identity);
        attacked.transform.SetParent(gameObject.transform, false);

        Destroy(attacked, 2f);
    }

    public void OnEnemyDead()
    {
        // exp
        player.GetComponent<PlayerExp>().TakeExp(enemyStats.GetStat(StatsType.ExpReward));

        // dead effect
        GameObject dead = Instantiate(deadEffect, new Vector3(0, 0.7f, 0), Quaternion.identity);
        dead.transform.SetParent(gameObject.transform, false);

        Destroy(dead, 3f);

        // 비활성화
        Invoke("DisableEnemy", 3f);
    }

    void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
