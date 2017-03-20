using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgradeable : MonoBehaviour 
{
	//Option selection array.  
	[HideInInspector] public int[] upgradeSelections;
	[HideInInspector] public int[] maxUpgradesForItem; //Have to include the direct to component (the 0 value).  

	void Awake() 
	{
		upgradeSelections = new int[6];
		maxUpgradesForItem = new int[6] { 6, 6, 6, 6, 6, 0 };
	}
}
