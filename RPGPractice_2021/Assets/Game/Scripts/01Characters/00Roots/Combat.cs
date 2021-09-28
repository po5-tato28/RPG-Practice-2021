using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{

	[SerializeField] protected float timeBetweenAttacks = 1f;

	protected Rigidbody rigidBody;
	protected Animator animator;
	protected float timeSinceLastAttack = Mathf.Infinity;

	protected abstract void UpdateAnimator();
	public abstract void Hit();
	public abstract void WeaponEffect();
	public abstract void DestroyWeaponEffect();

	protected virtual void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}


	protected void TriggerAttack()
	{
		animator.ResetTrigger("stopAttack");
		animator.SetTrigger("attack");
	}

	protected void StopAttack()
	{
		animator.ResetTrigger("attack");
		animator.SetTrigger("stopAttack");
	}

	//public void Cancel()
	//{
	//	StopAttack();
	//	target = null;
	//
	//	GetComponent<EnemyMovement>().Cancel();
	//}
}
