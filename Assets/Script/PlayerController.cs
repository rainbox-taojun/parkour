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
		character.Move();

		var jump = Input.GetKeyDown(KeyCode.Space);
		if (jump)
		{
			character.Jump();
		}
	}
}
