using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserConsole : MonoBehaviour 
{
	public static UserConsole instance;
	void Awake() {instance = this;}

	private Text userConsole;

	void Start()
	{
		userConsole = GetComponent <Text> ();
	}

	public void Output(string output)
	{
		userConsole.text = output;
	}
}
