﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tappable : MonoBehaviour 
{
	[HideInInspector] public Directable directableComp;
	[HideInInspector] public Upgradeable upgradableComp;

	//Get all component references.  
	void Start()
	{
		if (GetComponent <Directable> () != null)
			directableComp = GetComponent <Directable> ();
		if (GetComponent <Upgradeable> () != null)
			upgradableComp = GetComponent <Upgradeable> ();
	}

	//YAS 2D MODE FOR DA WINNNN
	public void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject ())
		{
			Debug.Log ("Mouse down on " + gameObject.name);
			SelectionUI.instance.GotMouseDownOn (this);
		}
	}
}
