using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	[SerializeField] private GameObject sosLogo = null;

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

		//Create the fleet with the MASTERCREATOR after parsing the file which stores fleet data.  
		GameObject createdSS = MasterCreator.instance.CreateNewSpaceStation(Vector3.zero);
		MasterCreator.instance.CreateNewFleetShip (Vector3.zero, createdSS.GetComponent <Tappable> ());

		//Now enable the hazard spawner and let's get some action going.  
		HazardSpawner.instance.InitiateEnemySwarms();
	}
}
