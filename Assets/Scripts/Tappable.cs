using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tappable : MonoBehaviour 
{
	//YAS 2D MODE FOR DA WINNNN
	public void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject ())
		{
			Debug.Log ("Got tap on " + gameObject.name);
			ChoicePanel.instance.InitializeWith (this);
		}
	}
}
