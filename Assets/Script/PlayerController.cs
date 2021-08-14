using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	PlayerCharacter character;
	PlayerHUD hud;
	GameMode gameMode;
	
	private void Awake()
	{
		character = FindObjectOfType<PlayerCharacter>();
		hud = FindObjectOfType<PlayerHUD>();
		gameMode = FindObjectOfType<GameMode>();
	}

	private void Update()
	{
		if (!gameMode.GetStartState()) return;
		if (!character.isAlive) return;
		character.Move(); // 角色固定向前移动

		var jump = Input.GetKeyDown(KeyCode.Space); // 跳跃
		var changeColor = Input.GetKeyDown(KeyCode.UpArrow);
		if (jump) character.Jump();
		if (changeColor) character.ChangeColorState();
	}

	public void StartGame()
	{
		gameMode.SetStartState(true);
		character.InitCharacter();
	}

	// 打开游戏结束面板
	public void ShowGameOverPanel()
	{
		hud.ShowGameOverPanel();
		gameMode.SetStartState(false);
	}
}
