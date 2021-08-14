using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    bool isStart = false;
	public void SetStartState(bool state)
	{
		isStart = state;
	}

	public bool GetStartState()
	{
		return isStart;
	}
}
