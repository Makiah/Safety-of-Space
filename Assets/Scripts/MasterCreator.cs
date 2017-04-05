using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCreator : MonoBehaviour 
{
	public static MasterCreator instance;
	void Awake() {instance = this;}

	[SerializeField] public GameObject fleetShip = null, spaceStation = null, asteroidHazard = null, fleetShipHazard = null, fleetShipBullet = null, explosion = null;

	public GameObject CreateNewFleetShip(Vector3 location, Transform orbitAround)
	{
		GameObject instantiatedShip = (GameObject)(Instantiate (fleetShip, Vector2.zero + Random.insideUnitCircle, Quaternion.identity));
		instantiatedShip.GetComponent <Directable> ().DirectTo (orbitAround);
		StartCoroutine(EnableTrailRendererOnFleetShipAfterDelay(instantiatedShip.GetComponent <TrailRenderer> ())); //Enable it AFTERWARD because then it is in the correct position.  
		return instantiatedShip;
	}
	private IEnumerator EnableTrailRendererOnFleetShipAfterDelay(TrailRenderer trailRenderer)
	{
		yield return new WaitForSeconds (1);
		trailRenderer.enabled = true;
	}

	public GameObject CreateNewSpaceStation(Vector2 location)
	{
		GameObject instantiatedStation = (GameObject)(Instantiate (spaceStation, location, Quaternion.identity));
		return instantiatedStation;
	}

	public GameObject CreateNewAsteroidHazard(Vector2 location, Tappable target, float speedOfMovement)
	{
		GameObject instantiatedAsteroidHazard = (GameObject)(Instantiate (asteroidHazard, location, Quaternion.identity));
		Vector2 diff = target.transform.position - instantiatedAsteroidHazard.transform.position;
		instantiatedAsteroidHazard.GetComponent <Rigidbody2D> ().velocity = diff.normalized * speedOfMovement;
		return instantiatedAsteroidHazard;
	}

	public GameObject CreateNewFleetShipHazard(Vector2 location, Transform target)
	{
		GameObject instantiatedFleetShipHazard = (GameObject)(Instantiate (fleetShipHazard, location, Quaternion.identity));
		instantiatedFleetShipHazard.GetComponent <Directable> ().DirectTo (target);
		return instantiatedFleetShipHazard;
	}

	public GameObject CreateNewFleetShipBullet() //Don't specify parameters since this will vary to a large degree based on the thing doing the shooting.  
	{
		GameObject instantiatedBullet = (GameObject)(Instantiate (fleetShipBullet, Vector2.zero, Quaternion.identity));
		return instantiatedBullet;
	}

	public GameObject CreateExplosion(Vector2 location)
	{
		GameObject instantiatedExplosion = (GameObject)(Instantiate (explosion, location, Quaternion.identity));
		return instantiatedExplosion;
	}
}
