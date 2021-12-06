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

    // ��Ʈ�� ���� �� ��� �ð�
    [SerializeField] float timeForWaitingPatrol = 5f;
    // ���������� ��Ʈ���� ���� �ð�
    [SerializeField] float timeSinceLastPatrol = 0f;

    [SerializeField] float patrolSpeed = 5f;
    [SerializeField] float trackingSpeed = 10f;

    private Coroutine waitNextPatrolCoroutine;
    private Coroutine timeLastPatrolCoroutine;


    #region OnDrawGizmos
    private void OnDrawGizmos()
    {
        // �ν� �þ� ����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        // ���� �Ÿ�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        // ���� �Ÿ�
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

        // ���� ������ ������
        if (IsInAttackRange())
        {
            state = EnemyState.Attack;
        }
        // Ʈ��ŷ ������ ������
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
    /// ��Ʈ�� �ൿ
    /// </summary>
    private void PatrolBehaviour()
    {
        if (agent.isStopped) agent.isStopped = false;

        // ������ ��ġ�� �����ߴ���
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
                // ���ο� ��ǥ�� �����ϰ�
                UpdatePath();
                // ����������
                break;
            }
        }
        // �ð��� 5��
        timeForWaitingPatrol = 5f;
        // �ڷ�ƾ ����
        waitNextPatrolCoroutine = null;
    }

    private IEnumerator TimeLastPatrol()
    {
        while (true)
        {
            // ���������� ��Ʈ������
            timeSinceLastPatrol += Time.deltaTime;

            yield return new WaitForFixedUpdate();

            // 3�ʸ� �ʰ��ϸ�
            if (timeSinceLastPatrol > 3f)
            {
                // ���ο� ��ǥ�� �����ϰ�
                UpdatePath();
                // ����������
                break;
            }
        }

        // �ٽ� �ð��� 0����
        timeSinceLastPatrol = 0f;
        // �ڷ�ƾ ����
        timeLastPatrolCoroutine = null;
    }


    /// <summary>
    /// ��ǥ�� �����ϴ� �޼���
    /// </summary>
    private void UpdatePath()
    {
        // ���� ��ǥ�� ��ǥ���� ��ǥ�� ����
        currentPosition = nextPosition;

        // ������ ������ ����
        Vector3 movePosition = EnemyPool.GetInstance().GetPatrolPosition().position;

        // ���� ��ǥ ��ǥ�� movePosition���� �Ҵ�
        nextPosition = movePosition;
        // �ڵ� ���� �Ÿ��� 0���� �����Ǿ����� ���� ���
        if (agent.stoppingDistance != 0f)
        {
            // 0���� �ʱ�ȭ ���ش�.
            agent.stoppingDistance = 0f;
        }        
        
        // ������ ��ǥ�� nextPosition���� �Ҵ�
        agent.destination = nextPosition;
        transform.LookAt(agent.destination);
        agent.speed = patrolSpeed;        

        // �ִϸ��̼� ����
        animator.SetFloat("Move", agent.speed);
    }

    private void TrackingBehaviour()
    {
        if (agent.isStopped) agent.isStopped = false;

        // ���� ��ǥ ��ǥ�� �÷��̾�� ����
        nextPosition = combat.target.transform.position;

        // �÷��̾ �ٶ󺸰�
        var targetPosition = new Vector3(combat.target.transform.position.x, transform.position.y, combat.target.transform.position.z);
        transform.LookAt(targetPosition);

        // ���� ��ǥ�� �̵�
        agent.destination = nextPosition;
        // Ʈ��ŷ �ӵ��� ��ȯ
        agent.speed = trackingSpeed;
        // �ִϸ��̼� ����
        animator.SetFloat("Move", 0f);
        animator.SetFloat("Run", agent.speed);
    }

    /// <summary>
    /// ���� �ൿ
    /// </summary>
    private void AttackBehaviour()
    {
        // �ִϸ��̼� ����
        animator.SetFloat("Move", 0f);
        animator.SetFloat("Run", 0f);

        // �÷��̾ �ٶ󺸰�
        var targetPosition = new Vector3(combat.target.transform.position.x, transform.position.y, combat.target.transform.position.z);
        transform.LookAt(targetPosition);

        // �������
        //agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // �����Ѵ�
        Debug.Log("AttackBehaviour()");
        //combat.Hit();
    }

    /// <summary>
    /// �÷��̾�� �Ÿ� ��� (���� ����)
    /// </summary>
    private bool IsInAttackRange()
    {
        // �÷��̾�� ũ��ó�� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(combat.target.transform.position, transform.position);
        //Debug.Log(distanceToPlayer);

        // ���� ���� attack �������� ������ true
        return distanceToPlayer < attackRange;
    }

    /// <summary>
    /// �÷��̾���� �Ÿ� ��� (Ʈ��ŷ ����)
    /// </summary>
    private bool IsInTrackingRange()
    {
        // �÷��̾�� ũ��ó�� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(combat.target.transform.position, transform.position);
        // Debug.Log(distanceToPlayer);

        // ���� ���� tracking �������� ������ true
        return distanceToPlayer < trackingRange;
    }

    /// <summary>
    /// �����ߴ��� �Ǻ��ϴ� �޼���
    /// </summary>
    private bool IsArrive()
    {
        // ���� �����ǰ� ũ������ �Ÿ� ���
        float distanceToWaypoint = Vector3.Distance(transform.position, nextPosition);
        // Debug.Log(distanceToWaypoint);

        return distanceToWaypoint < 1f;
    }
}