using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float attack { get; private set; }

    private void Update()
    {
        MovementInput();
        AttackInput();
    }

    void MovementInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    void AttackInput()
    {
        attack = Input.GetAxis("Basic Attack");
    }
}
