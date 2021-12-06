using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Patrol,
    Tracking,
    Attack,
}

public class EnemyMover : MonoBehaviour
{
    [SerializeField] EnemyState state;

    private NavMeshAgent agent;
    private EnemyController controller;
    private EnemyCombat combat;
    private Animator animator;

    [SerializeField] float trackingRange = 3f;
    [SerializeField] float attackRange = 1f;
    
    private Vector3 currentPosition;
    private Vector3 nextPosition;

    // 패트롤 끝난 후 대기 시간
    [SerializeField] float timeForWaitingPatrol = 5f;
    // 마지막으로 패트롤을 멈춘 시간
    [SerializeField] float timeSinceLastPatrol = 0f;

    [SerializeField] float patrolSpeed = 5f;
    [SerializeField] float trackingSpeed = 10f;

    private Coroutine waitNextPatrolCoroutine;
    private Coroutine timeLastPatrolCoroutine;


    #region OnDrawGizmos
    private void OnDrawGizmos()
    {
        // 인식 시야 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        // 공격 거리
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        // 공격 거리
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(nextPosition, 0.2f);
    }
    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<EnemyController>();
        combat = GetComponent<EnemyCombat>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        agent.enabled = true;

        state = EnemyState.Patrol;
        timeForWaitingPatrol = 5f;

        currentPosition = EnemyPool.GetInstance().GetCreatePosition().position;
        nextPosition = EnemyPool.GetInstance().GetPatrolPosition().position;

        { 
            agent.destination = nextPosition;
            agent.speed = patrolSpeed;
            animator.SetFloat("Move", patrolSpeed);
        }       
    }

    private void Update()
    {
        if (combat.target == null) return;

        // 공격 범위에 들어오면
        if (IsInAttackRange())
        {
            state = EnemyState.Attack;
        }
        // 트래킹 범위에 들어오면
        else if (IsInTrackingRange())
        {
            state = EnemyState.Tracking;
        }

        DecisionBehaviour(state);
    }

    private void DecisionBehaviour(EnemyState _state)
    {
        switch(_state)
        {
            case EnemyState.Patrol:
                PatrolBehaviour();
                break;
            case EnemyState.Tracking:
                TrackingBehaviour();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                break;
            default:
                PatrolBehaviour();
                break;
        }
    }

    /// <summary>
    /// 패트롤 행동
    /// </summary>
    private void PatrolBehaviour()
    {
        if (agent.isStopped) agent.isStopped = false;

        // 지정한 위치에 도착했는지
        if (IsArrive())
        {
            animator.SetFloat("Move", 0f);
            if (waitNextPatrolCoroutine == null)
            {
                waitNextPatrolCoroutine = StartCoroutine(WaitNextPatrol());
            }
        }
    }
    private IEnumerator WaitNextPatrol()
    {
        while (true)
        {
            timeForWaitingPatrol -= Time.deltaTime;

            yield return new WaitForFixedUpdate();

            if (timeForWaitingPatrol < 0f)
            {
                // 새로운 좌표를 지정하고
                UpdatePath();
                // 빠져나간다
                break;
            }
        }
        // 시간을 5로
        timeForWaitingPatrol = 5f;
        // 코루틴 비우기
        waitNextPatrolCoroutine = null;
    }

    private IEnumerator TimeLastPatrol()
    {
        while (true)
        {
            // 마지막으로 패트롤한지
            timeSinceLastPatrol += Time.deltaTime;

            yield return new WaitForFixedUpdate();

            // 3초를 초과하면
            if (timeSinceLastPatrol > 3f)
            {
                // 새로운 좌표를 지정하고
                UpdatePath();
                // 빠져나간다
                break;
            }
        }

        // 다시 시간을 0으로
        timeSinceLastPatrol = 0f;
        // 코루틴 비우기
        timeLastPatrolCoroutine = null;
    }


    /// <summary>
    /// 좌표를 갱신하는 메서드
    /// </summary>
    private void UpdatePath()
    {
        // 현재 좌표를 목표였던 좌표로 갱신
        currentPosition = nextPosition;

        // 움직일 포지션 지정
        Vector3 movePosition = EnemyPool.GetInstance().GetPatrolPosition().position;

        // 다음 목표 좌표를 movePosition으로 할당
        nextPosition = movePosition;
        // 자동 멈춤 거리가 0으로 지정되어있지 않은 경우
        if (agent.stoppingDistance != 0f)
        {
            // 0으로 초기화 해준다.
            agent.stoppingDistance = 0f;
        }        
        
        // 움직일 좌표를 nextPosition으로 할당
        agent.destination = nextPosition;
        transform.LookAt(agent.destination);
        agent.speed = patrolSpeed;        

        // 애니메이션 갱신
        animator.SetFloat("Move", agent.speed);
    }

    private void TrackingBehaviour()
    {
        if (agent.isStopped) agent.isStopped = false;

        // 다음 목표 좌표를 플레이어로 설정
        nextPosition = combat.target.transform.position;

        // 플레이어를 바라보고
        var targetPosition = new Vector3(combat.target.transform.position.x, transform.position.y, combat.target.transform.position.z);
        transform.LookAt(targetPosition);

        // 다음 목표로 이동
        agent.destination = nextPosition;
        // 트래킹 속도로 전환
        agent.speed = trackingSpeed;
        // 애니메이션 갱신
        animator.SetFloat("Move", 0f);
        animator.SetFloat("Run", agent.speed);
    }

    /// <summary>
    /// 공격 행동
    /// </summary>
    private void AttackBehaviour()
    {
        // 애니메이션 갱신
        animator.SetFloat("Move", 0f);
        animator.SetFloat("Run", 0f);

        // 플레이어를 바라보고
        var targetPosition = new Vector3(combat.target.transform.position.x, transform.position.y, combat.target.transform.position.z);
        transform.LookAt(targetPosition);

        // 멈춘다음
        //agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // 공격한다
        Debug.Log("AttackBehaviour()");
        //combat.Hit();
    }

    /// <summary>
    /// 플레이어와 거리 계산 (공격 범위)
    /// </summary>
    private bool IsInAttackRange()
    {
        // 플레이어와 크리처의 거리 계산
        float distanceToPlayer = Vector3.Distance(combat.target.transform.position, transform.position);
        //Debug.Log(distanceToPlayer);

        // 비교한 값이 attack 범위보다 적으면 true
        return distanceToPlayer < attackRange;
    }

    /// <summary>
    /// 플레이어와의 거리 계산 (트래킹 범위)
    /// </summary>
    private bool IsInTrackingRange()
    {
        // 플레이어와 크리처의 거리 계산
        float distanceToPlayer = Vector3.Distance(combat.target.transform.position, transform.position);
        // Debug.Log(distanceToPlayer);

        // 비교한 값이 tracking 범위보다 적으면 true
        return distanceToPlayer < trackingRange;
    }

    /// <summary>
    /// 도착했는지 판별하는 메서드
    /// </summary>
    private bool IsArrive()
    {
        // 다음 포지션과 크리쳐의 거리 계산
        float distanceToWaypoint = Vector3.Distance(transform.position, nextPosition);
        // Debug.Log(distanceToWaypoint);

        return distanceToWaypoint < 1f;
    }
}