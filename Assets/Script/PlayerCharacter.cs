using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	public enum TransColor
	{
		Red,
		Green,
		Undefine
	}

	TransColor colorCurrent;

	Rigidbody rigid; // 刚体
	Renderer render;
	Animator animator; // 动画
	Collision collisionRet; // 碰撞

	public float speed; // 速度
	public float jumpForce; // 跳跃力度
	public float doubleJumpForce; // 二段跳力度
	public bool isAlive; // 角色存活
	public AudioClip jumpSound; // 跳跃音效
	public AudioClip landSound; // 落地音效
	public AudioClip changeColorSound; // 变色音效
	public ParticleSystem diePartic;
	public ParticleSystem water;
	public ParticleSystem steam;

	int jumpCount = 0; // 跳跃的次数
	bool onGround; // 是否在地面上
	

	private void Awake()
	{
		// 初始化
		rigid = GetComponent<Rigidbody>();
		render = GetComponentInChildren<Renderer>();
		animator = GetComponentInChildren<Animator>();

		diePartic.Stop();
		water.Stop();
		steam.Play();

		isAlive = true;
		render.material.color = Color.red;
		colorCurrent = TransColor.Red;
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

	// update是根据帧数 而fixedUpdate是根据真实时间 物理判断要放在FixedUpdate
	private void FixedUpdate()
	{
		if (!isAlive) return;

		if (collisionRet != null)
		{
			if (collisionRet.gameObject.CompareTag("Red"))
			{
				if (colorCurrent != TransColor.Red)
				{
					Die();
				}
			}
			else if (collisionRet.gameObject.CompareTag("Green"))
			{
				if (colorCurrent != TransColor.Green)
				{
					Die();
				}
			}
			else
			{
				Die();
			}
		}
		onGround = GroundCheck();//时刻执行是否在地面判断
		animator.SetBool("onGround", onGround);//将判断结果写入动画的属性,让他去切换动画
	}


	public void Move()
	{
		if (!isAlive) return;
		// 角色自动向前移动(z轴)
		var vel = rigid.velocity;
		vel.z = (Vector3.forward * speed).z;
		rigid.velocity = vel;
	}

  // 跳跃方法
  // y方向力归零是为了防止下落的时候抵消了跳跃向上的力。
	public void Jump()
	{
		if (!isAlive) return;

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
			AudioSource.PlayClipAtPoint(jumpSound, transform.position);
			jumpCount++;
		}
	}

  // 狗带方法
	public void Die()
	{
		isAlive = false;
		render.enabled = false;
		rigid.velocity = Vector3.zero;
		diePartic.Play();
		water.Stop();
		steam.Stop();
		Invoke("Restart", 1);
	}

	public void Restart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}

  // 设置颜色
	public void ChangeColorState()
	{
		if (!isAlive) return;

		

		if (colorCurrent == TransColor.Red)
		{
			colorCurrent = TransColor.Green;
			render.material.color = Color.green;
			water.Play();
			steam.Stop();
		}
		else if (colorCurrent == TransColor.Green)
		{
			colorCurrent = TransColor.Red;
			render.material.color = Color.red;
			steam.Play();
			water.Stop();
		}

		animator.SetTrigger("onChangeColor");//将判断结果写入动画的属性,让他去切换动画
		AudioSource.PlayClipAtPoint(changeColorSound, transform.position);

	}

	// 开始碰撞事件
	private void OnCollisionEnter(Collision collision)
	{
		jumpCount = 0; // 跳跃数归零
		AudioSource.PlayClipAtPoint(landSound, transform.position);
		collisionRet = collision;
	}

	// 
	private void OnCollisionStay(Collision collision)
	{
		collisionRet = collision;
	}

	// 退出碰撞
	private void OnCollisionExit(Collision collision)
	{
		collisionRet = null;
	}
}
