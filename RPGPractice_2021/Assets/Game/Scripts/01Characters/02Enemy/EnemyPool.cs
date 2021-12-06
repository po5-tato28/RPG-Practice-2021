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

    // �� Ȱ��ȭ
    void ActiveEnemy()
    {
        // ���콺 ��Ŭ�� �� ������ �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
        {
            // �߻�Ǿ���� ������ �Ѿ��� ������ �߻��� �ķ� ���� ���ư��� �ִ� ���̶��, �߻縦 ���ϰ� �Ѵ�.
            if (enemies[currentEnemyIndex].gameObject.activeSelf)
            {
                return;
            }

            // �Ѿ� �ʱ� ��ġ�� ������� ����
            // enemies[currentEnemyIndex].transform.position = this.transform.position;

            // �Ѿ� Ȱ��ȭ ���ֱ�
            enemies[currentEnemyIndex].gameObject.SetActive(true);

            // ��� 9��° �Ѿ��� �߻��ߴٸ� �ٽ� 0��° �Ѿ��� �߻��� �غ� �Ѵ�.
            if (currentEnemyIndex >= enemyMaxCount - 1)
            {
                currentEnemyIndex = 0;
            }
            else
            {
                currentEnemyIndex++;
            }
        }
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
