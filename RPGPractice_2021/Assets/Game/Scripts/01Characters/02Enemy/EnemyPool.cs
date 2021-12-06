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

    // 적 활성화
    void ActiveEnemy()
    {
        // 마우스 좌클릭 할 때마다 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            // 발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
            if (enemies[currentEnemyIndex].gameObject.activeSelf)
            {
                return;
            }

            // 총알 초기 위치는 전투기랑 같게
            // enemies[currentEnemyIndex].transform.position = this.transform.position;

            // 총알 활성화 해주기
            enemies[currentEnemyIndex].gameObject.SetActive(true);

            // 방금 9번째 총알을 발사했다면 다시 0번째 총알을 발사할 준비를 한다.
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
