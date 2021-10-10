using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Health health;

    Vector3 targetPosition;

    private void Awake()
    {
        health = GetComponent<Health>();
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
}
