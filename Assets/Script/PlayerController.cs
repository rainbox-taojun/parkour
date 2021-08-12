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
		if (!character.isAlive) return;
		character.Move(); // 角色固定向前移动

		var jump = Input.GetKeyDown(KeyCode.Space); // 跳跃
		var changeColor = Input.GetKeyDown(KeyCode.UpArrow);
		if (jump) character.Jump();
		if (changeColor) character.ChangeColorState();
	}
}
