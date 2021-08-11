using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	Rigidbody rigid;
	Renderer render;
	Animator animator;
	Collision collisionRet;

	public float speed;
	public float jumpForce;
	public float doubleJumpForce;

	int jumpCount = 0;
	bool onGround;
	

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		render = GetComponentInChildren<Renderer>();
		animator = GetComponentInChildren<Animator>();
	}

	public bool GroundCheck()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 0.35f);

		for(int i=0;i< colliders.Length;i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				return true;
			}
		}

		return false;
	}

	private void Update()
	{
		onGround = GroundCheck();

		animator.SetBool("onGround", onGround);
	}

	public void Move()
	{
		var vel = rigid.velocity;
		vel.z = (Vector3.forward * speed).z;
		rigid.velocity = vel;
	}

	public void Jump()
	{
		if (jumpCount < 2)
		{
			if (jumpCount == 0)
			{
				rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
				rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
			else if(jumpCount ==1)
			{
				rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
				rigid.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
			}
			jumpCount++;
		}
	}

	public void Die()
	{

	}

	public void ChangeColorState()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		jumpCount = 0;
	}
}
