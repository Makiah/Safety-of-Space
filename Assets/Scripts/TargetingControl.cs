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

	//Automatically checks the state of the directable component and gives it new instructions depending on the state of the directable.  
	private IEnumerator AutoTarget()
	{
		while (true)
		{
			//If the directable is attempting to return to the space station, give it new orders.  
			if (directableComp.state == Directable.DirectableState.MovingToSpaceStation || directableComp.state == Directable.DirectableState.OrbitingSpaceStation)
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
					directableComp.DirectTo (closestHazard.GetComponent <Tappable> ().transform);
				}
			}

			yield return null;
		}
	}
}
