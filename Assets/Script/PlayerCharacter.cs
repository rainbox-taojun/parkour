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

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		render = GetComponentInChildren<Renderer>();
		animator = GetComponent<Animator>();
	}

	public void Move()
	{
		var vel = rigid.velocity;
		vel.z = (Vector3.forward * speed).z;
		rigid.velocity = vel;
	}

	public void Jump()
	{
		
	}

	public void Die()
	{

	}

	public void ChangeColorState()
	{

	}
}
