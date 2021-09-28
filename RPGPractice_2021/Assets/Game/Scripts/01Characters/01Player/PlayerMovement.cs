using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    PlayerInput charInput;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    protected override void Awake()
    {
        base.Awake();
        charInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        speed = 5f;
    }

    private void Update()
    {
        UpdateAnimator();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void Cancel()
    {
        // 아무것도안함
    }

    protected override void UpdateAnimator()
    {
        Vector3 dir = new Vector3(charInput.horizontal, 0f, charInput.vertical);
        animator.SetFloat("forwardSpeed", dir.magnitude);
    }

    protected override void Move()
    {
        Vector3 dir = new Vector3(charInput.horizontal, 0f, charInput.vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            rigidBody.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }
    }
}
