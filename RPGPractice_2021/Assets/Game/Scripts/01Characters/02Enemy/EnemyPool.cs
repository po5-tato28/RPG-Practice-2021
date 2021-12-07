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

    // 적 프리팹
    public GameObject enemy;

    // 적 관리 리스트
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] Transform createPosition;
    [SerializeField] Transform[] patrolPositions;

    private Coroutine activeCoroutine;


    // 내가 생성할 적의 수
    private readonly int enemyMaxCount = 10;

    // 현재 대기 중인 적 인덱스
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
        //총알 10개 미리 생성
        for (int i = 0; i < enemyMaxCount; ++i)
        {
            GameObject e = Instantiate<GameObject>(enemy);

            // 적을 발사하기 전까지는 비활성화 해준다.
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

    // 적 활성화
    private IEnumerator ActiveEnemyCoroutine()
    {
        while(true)
        {
            // 생성되어야할 순번의 적이 생성되어 있으면 리턴
            if (enemies[currentEnemyIndex].gameObject.activeSelf)
            {
                // 마지막 인덱스의 적을 소환했다면 마지막 번호 -> 0으로 변화
                if (currentEnemyIndex >= enemyMaxCount - 1)
                {
                    currentEnemyIndex = 0;
                }
                else
                {
                    // 아니면 그냥 인덱스 증가
                    currentEnemyIndex++;
                }

                break;
            }

            // 적 활성화
            enemies[currentEnemyIndex].gameObject.SetActive(true);

            // 마지막 인덱스의 적을 소환했다면 마지막 번호 -> 0으로 변화
            if (currentEnemyIndex >= enemyMaxCount - 1)
            {
                currentEnemyIndex = 0;
            }
            else
            {
                // 아니면 그냥 인덱스 증가
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
