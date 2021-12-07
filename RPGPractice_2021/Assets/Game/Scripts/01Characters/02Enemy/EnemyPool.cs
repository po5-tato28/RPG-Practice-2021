using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool instance;
    public static EnemyPool GetInstance()
    {
        return instance;
    }

    // �� ������
    public GameObject enemy;

    // �� ���� ����Ʈ
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] Transform createPosition;
    [SerializeField] Transform[] patrolPositions;

    private Coroutine activeCoroutine;


    // ���� ������ ���� ��
    private readonly int enemyMaxCount = 10;

    // ���� ��� ���� �� �ε���
    private int currentEnemyIndex = 0;

    private void Awake()
    {
        if(!instance)
        {
            instance = this; 
        }
    }

    void Start()
    {
        //�Ѿ� 10�� �̸� ����
        for (int i = 0; i < enemyMaxCount; ++i)
        {
            GameObject e = Instantiate<GameObject>(enemy);

            // ���� �߻��ϱ� �������� ��Ȱ��ȭ ���ش�.
            e.SetActive(false);

            enemies.Add(e);
        }
    }

    void Update()
    {
        ActiveEnemy();
    }
    
    private void ActiveEnemy()
    {
        if(activeCoroutine == null)
        {
            activeCoroutine = StartCoroutine(ActiveEnemyCoroutine());
        }        
    }

    // �� Ȱ��ȭ
    private IEnumerator ActiveEnemyCoroutine()
    {
        while(true)
        {
            // �����Ǿ���� ������ ���� �����Ǿ� ������ ����
            if (enemies[currentEnemyIndex].gameObject.activeSelf)
            {
                // ������ �ε����� ���� ��ȯ�ߴٸ� ������ ��ȣ -> 0���� ��ȭ
                if (currentEnemyIndex >= enemyMaxCount - 1)
                {
                    currentEnemyIndex = 0;
                }
                else
                {
                    // �ƴϸ� �׳� �ε��� ����
                    currentEnemyIndex++;
                }

                break;
            }

            // �� Ȱ��ȭ
            enemies[currentEnemyIndex].gameObject.SetActive(true);

            // ������ �ε����� ���� ��ȯ�ߴٸ� ������ ��ȣ -> 0���� ��ȭ
            if (currentEnemyIndex >= enemyMaxCount - 1)
            {
                currentEnemyIndex = 0;
            }
            else
            {
                // �ƴϸ� �׳� �ε��� ����
                currentEnemyIndex++;
            }

            yield return new WaitForSecondsRealtime(3f);
        }

        activeCoroutine = null;
    }

    public Transform GetCreatePosition()
    {
        return createPosition;
    }

    public Transform GetPatrolPosition()
    {
        int rand = Random.Range(0, patrolPositions.Length);
        Transform randPosition = patrolPositions[rand];

        return randPosition;
    }
}
