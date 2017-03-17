using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleSide))]
[RequireComponent(typeof(Directable))]
public class TargetingControl : MonoBehaviour 
{
	private Directable directableComp;

	void Awake()
	{
		directableComp = GetComponent <Directable> ();
	}

	private int currentTargetingLevel = 0;

	public void ImproveTargeting()
	{
		currentTargetingLevel++;
		//At the moment I only have one idea on how to improve targeting: auto-targeting, and so when this is called it just enables the same thing.  
		switch (currentTargetingLevel)
		{
			case 1: 
				StartCoroutine (AutoTarget ());
				break;
		}
	}

	private IEnumerator AutoTarget()
	{
		Debug.Log ("Started coroutine");
		while (true)
		{
			if (!directableComp.IsBeingDirected ())
			{
				//Find closest enemy on the OPPOSITE battle side.  
				BattleSide[] hazards = GameObject.FindObjectsOfType <BattleSide> ();

				//Find closest valid target.  .  
				BattleSide closestHazard = null;
				float closestHazardDistance = float.MaxValue;
				foreach (BattleSide hazard in hazards)
				{
					if (hazard.GetComponent <BattleSide> ().side != GetComponent <BattleSide> ().side)
					{
						float dist = Vector2.Distance (hazard.transform.position, transform.position);
						if (dist < closestHazardDistance)
						{
							closestHazard = hazard;
							closestHazardDistance = dist;
						}
					}
				}

				//Now direct to the nearest hazard found.  
				if (closestHazard != null)
				{
					directableComp.DirectTo (closestHazard.GetComponent <Tappable> ());
					Debug.Log ("Directing to " + closestHazard.gameObject.name);
				}
			}

			yield return null;
		}
	}
}
