using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	[SerializeField] private GameObject sosLogo = null, spaceStation = null, fleetShip = null, hazard = null;

	void Start()
	{
		StartCoroutine (Game ());
	}
		
	private IEnumerator Game()
	{
		//Instantiate the logo and then get rid of it afterward.  (Wait until it's animation has completed.  
		GameObject instantiatedSOSLogo = (GameObject)(Instantiate (sosLogo, Vector3.zero, Quaternion.identity));
		yield return new WaitForSeconds(3);
		Destroy (instantiatedSOSLogo);
		Debug.Log ("Destroyed logo after waiting for animation to complete");

		//Here is where we would parse the attached data file to determine what the space station and the fleet would look like, but in this case, we'll just instantiate the number we need to debug.  
		GameObject instantiatedSpaceStation = (GameObject) (Instantiate(spaceStation, Vector3.zero, Quaternion.identity));

		//Here is where we would parse the attached data file to determine what sort of fleet the player has.  Instead, we'll just create two for the player for testing purposes.  
		GameObject instantiatedFleetShip = (GameObject) (Instantiate(fleetShip, Vector3.zero, Quaternion.identity));
		instantiatedFleetShip.GetComponent <OrbitOther> ().Orbit (instantiatedSpaceStation.transform);

		//Instantiate an example hazard a little ways away.  
		GameObject instantiatedHazard = (GameObject) (Instantiate(hazard, new Vector3(-100, 100, 0), Quaternion.identity));
	}
}
