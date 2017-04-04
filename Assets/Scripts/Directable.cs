using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleSide))]
[RequireComponent(typeof(Rigidbody2D))]
public class Directable : OrbitSpaceStation 
{
	//The states that the Directable can be doing.  
	public enum DirectableState { TrackingEnemy, MovingToSpaceStation, OrbitingSpaceStation }
	public DirectableState state;
	[SerializeField] public float thrustPower = 2, maxSpeed = 15, stopDistance = 2;

	//Get components required.  
	private Rigidbody2D rb2d;
	protected override void Awake()
	{
		rb2d = GetComponent <Rigidbody2D> ();
		FindAndMoveTowardNearestSpaceStation ();
	}

	//The grunt work of directing the object to some transform.  
	private IEnumerator directToCoroutine = null;
	public void DirectTo (Transform target)
	{
		//Clear the existing coroutines.  
		if (orbitCoroutine != null)
			StopCoroutine (orbitCoroutine);

		if (directToCoroutine != null)
			StopCoroutine (directToCoroutine);

		//Set new location.  
		targetDestination = target;

		//Determine how the ship will react upon arrival.  
		if (targetDestination.gameObject.tag.Equals ("Space Station"))
			state = DirectableState.MovingToSpaceStation;
		else
			state = DirectableState.TrackingEnemy;

		//Start the coroutine.  
		directToCoroutine = DirectToCoroutine ();
		StartCoroutine (directToCoroutine);
	}
	private IEnumerator DirectToCoroutine()
	{
		//While the other object has not been destroyed.  
		while (targetDestination != null)
		{
			//Move based on calculated values.  
			Vector2 targetPosition = new Vector2(targetDestination.transform.position.x, targetDestination.transform.position.y);
			Vector2 diff = targetPosition - new Vector2(transform.position.x, transform.position.y);

			if (Vector2.Distance (targetPosition, transform.position) > stopDistance)
			{
				//Update velocity (make sure not to go over the max velocity)
				Vector2 thrustVector = diff.normalized * thrustPower;

				if ((rb2d.velocity + thrustVector).magnitude <= maxSpeed)
					rb2d.AddForce (thrustVector);
				else
					rb2d.velocity = (rb2d.velocity + thrustVector).normalized * maxSpeed;
			}
			else
			{
				if (state == DirectableState.TrackingEnemy)
					rb2d.velocity *= 0.95f; //Diminish the velocity by some factor.  
				else
					BeginToOrbit (targetDestination);
			}

			//Update heading regardless of distance from ideal position.  
			transform.localEulerAngles = new Vector3 (0, 0, Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg + 90);

			yield return null;
		}

		//Upon reaching the destination successfully, return home by finding the nearest space station.  
		FindAndMoveTowardNearestSpaceStation();
	}

	private void FindAndMoveTowardNearestSpaceStation()
	{
		//Find the nearest space station.  
		GameObject[] spaceStations = GameObject.FindGameObjectsWithTag("Space Station");

		GameObject nearestSpaceStation = null;
		float leastDistance = float.MaxValue;
		foreach (GameObject spaceStation in spaceStations)
		{
			float distance = Vector2.Distance (spaceStation.transform.position, transform.position);
			if (distance < leastDistance)
			{
				leastDistance = distance;
				nearestSpaceStation = spaceStation;
			}
		}

		//Start moving toward this object.  
		DirectTo(nearestSpaceStation.transform);

		//Set the appropriate state.  
		state = DirectableState.MovingToSpaceStation;
  	}

	protected override void BeginToOrbit(Transform someSpaceStation)
	{
		//End the other coroutine.  
		StopCoroutine (directToCoroutine);

		//Actually DO the thing.  
		base.BeginToOrbit (someSpaceStation);

		//Set the state appropriately.  
		state = DirectableState.OrbitingSpaceStation;
	}
}
