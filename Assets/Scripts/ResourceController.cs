using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour 
{
	public static ResourceController instance;
	void Awake() {instance = this;}

	private Text resourceText;

	void Start()
	{
		resourceText = GetComponent <Text> ();
	}

	public int GetAvailable() 
	{
		return int.Parse (resourceText.text);
	}

	public void ChangeBy(int change)
	{
		resourceText.text = "" + (int.Parse (resourceText.text) + change);
	}
}
