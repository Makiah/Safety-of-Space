using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureControls : MonoBehaviour 
{
	[SerializeField] private Canvas selectionUI = null;
	[SerializeField] private Button attack = null, upgrade = null;

	void Update()
	{
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || 
			Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			if (Input.GetMouseButtonDown (0))
			{
			}
		}
	}
}
