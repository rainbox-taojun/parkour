using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	Rigidbody rigid; // 刚体
	Renderer render;
	Animator animator; // 动画
	Collision collisionRet; // 碰撞

	public float speed; // 速度
	public float jumpForce; // 跳跃力度
	public float doubleJumpForce; // 二段跳力度

	int jumpCount = 0; // 跳跃的次数
	bool onGround; // 是否在地面上
	

	private void Awake()
	{
    // 初始化
		rigid = GetComponent<Rigidbody>();
		render = GetComponentInChildren<Renderer>();
		animator = GetComponentInChildren<Animator>();
	}

  // 判断是否在地面上
	public bool GroundCheck()
	{
    // 碰撞
		Collider[] colliders = Physics.OverlapSphere(transform.position, 0.35f);

		for(int i=0;i< colliders.Length;i++)
		{
			if (colliders[i].gameObject != gameObject) // 判断碰撞是不是自身
			{
				return true;
			}
		}

		return false;
	}

	private void Update()
	{
		onGround = GroundCheck();//时刻执行是否在地面判断

		animator.SetBool("onGround", onGround);//将判断结果写入动画的属性,让他去切换动画
	}

	public void Move()
	{
    // 角色自动向前移动(z轴)
		var vel = rigid.velocity;
		vel.z = (Vector3.forward * speed).z;
		rigid.velocity = vel;
	}

  // 跳跃方法
  // y方向力归零是为了防止下落的时候抵消了跳跃向上的力。
	public void Jump()
	{
		if (jumpCount < 2)// 判断是否已经跳了两次
		{
			if (jumpCount == 0) // 第一次跳
			{
				rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z); // 将y轴的力归零
				rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
			else if(jumpCount ==1) // 第二次跳
			{
				rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);// 将y轴的力归零
				rigid.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
			}
			jumpCount++;
		}
	}

  // 狗带方法
	public void Die()
	{

	}

  // 设置颜色
	public void ChangeColorState()
	{

	}

  // 碰撞事件
	private void OnCollisionEnter(Collision collision)
	{
		jumpCount = 0; // 跳跃数归零
	}
}
