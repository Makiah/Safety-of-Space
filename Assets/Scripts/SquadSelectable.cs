using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectable : MonoBehaviour 
{
	void Awake()
	{
		//Add self to the selectable panel.  
		SquadSelectionManager.instance.Add (this);
	}
}
