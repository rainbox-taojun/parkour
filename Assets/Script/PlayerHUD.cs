using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
	PlayerController controller;
	public GameObject StartPanel;
	public GameObject RestartPanel;

	private void Awake()
	{
		controller = FindObjectOfType<PlayerController>();
	}

	public void OnClick_Start()
	{
		StartPanel.SetActive(false);
		controller.StartGame();
	}

	public void ShowGameOverPanel()
	{
		RestartPanel.SetActive(true);
	}

	public void OnClick_Restart()
	{
		RestartPanel.SetActive(false);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}
}
