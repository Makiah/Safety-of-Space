using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCreator : MonoBehaviour 
{
	public static MasterCreator instance;
	void Awake() {instance = this;}

	[SerializeField] private GameObject fleetShip = null, spaceStation = null, asteroidHazard = null, fleetShipHazard = null, fleetShipBullet = null;

	public GameObject CreateNewFleetShip(Vector3 location, Tappable orbitAround)
	{
		GameObject instantiatedShip = (GameObject)(Instantiate (fleetShip, Vector3.zero, Quaternion.identity));
		instantiatedShip.GetComponent <OrbitOther> ().Orbit (orbitAround.transform);
		return instantiatedShip;
	}

	public GameObject CreateNewSpaceStation(Vector3 location)
	{
		GameObject instantiatedStation = (GameObject)(Instantiate (spaceStation, location, Quaternion.identity));
		return instantiatedStation;
	}

	public GameObject CreateNewAsteroidHazard(Vector3 location, Tappable target)
	{
		GameObject instantiatedAsteroidHazard = (GameObject)(Instantiate (asteroidHazard, location, Quaternion.identity));
		instantiatedAsteroidHazard.GetComponent <Directable> ().DirectTo (target);
		return null;
	}

	public GameObject CreateNewFleetShipHazard(Vector3 location, Tappable target)
	{
		GameObject instantiatedFleetShipHazard = (GameObject)(Instantiate (fleetShipHazard, Vector3.zero, Quaternion.identity));
		instantiatedFleetShipHazard.GetComponent <Directable> ().DirectTo (target);
		return null;
	}

	public GameObject CreateNewFleetShipBullet() //Don't specify parameters since this will vary to a large degree based on the thing doing the shooting.  
	{
		GameObject instantiatedBullet = (GameObject)(Instantiate (fleetShipBullet, Vector3.zero, Quaternion.identity));
		return instantiatedBullet;
	}
}
