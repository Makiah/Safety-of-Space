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

	public bool ChangeBy(int change)
	{
		if (GetAvailable() + change >= 0)
		{
			resourceText.text = "" + (int.Parse (resourceText.text) + change);
			return true;
		}
		else
		{
			return false;
		}
	}
}
