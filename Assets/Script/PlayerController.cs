using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	PlayerCharacter character;

	private void Awake()
	{
		character = FindObjectOfType<PlayerCharacter>();
	}

	private void Update()
	{
		character.Move(); // 角色固定向前移动

		var jump = Input.GetKeyDown(KeyCode.Space); // 跳跃
		if (jump)
		{
			character.Jump();
		}
	}
}
