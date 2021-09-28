using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
	//protected StateController stateController;
	protected Health health;

	public float speed = 12f;
	public float turnSpeed = 180f;

	protected Rigidbody rigidBody;
	protected Animator animator;
	protected float movementInputValue;
	protected float turnInputValue;

	public Transform[] spawnPoints;
	public List<Transform> wayPoints;

	// ¸Þ¼­µå
	public abstract void Cancel();
	protected abstract void UpdateAnimator();


	protected virtual void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

		//stateController = GetComponent<StateController>();
		health = GetComponent<Health>();
	}

	protected virtual void OnEnable()
	{
		// When the tank is turned on, make sure it's not kinematic.
		rigidBody.isKinematic = false;

		// Also reset the input values.
		movementInputValue = 0f;
		turnInputValue = 0f;
	}

	protected virtual void OnDisable()
	{
		// When the tank is turned off, set it to kinematic so it stops moving.
		rigidBody.isKinematic = true;
	}

	protected virtual void Move()
	{
		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		rigidBody.MovePosition(rigidBody.position + movement);
	}
	protected virtual void Turn()
	{
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = turnInputValue * turnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
	}
}
