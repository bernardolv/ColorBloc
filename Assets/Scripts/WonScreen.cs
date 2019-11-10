using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonScreen : MonoBehaviour
{
	public static WonScreen Instance;
	public Text text;

	void Awake(){
		Instance = this;
	}

	public void TurnOn(){
		text.enabled = true;
		PlayerMovement.Instance.paused = true;
	}

	public void TurnOff(){
		text.enabled = false;
		PlayerMovement.Instance.paused = false;
	}
}

