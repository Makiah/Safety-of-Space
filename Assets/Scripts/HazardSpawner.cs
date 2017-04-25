using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour 
{
	public static HazardSpawner instance;
	void Awake() {instance = this;}

	public void InitiateEnemySwarms()
	{
		StartCoroutine (CreateEnemies ());
	}

	private IEnumerator CreateEnemies()
	{
		while (true)
		{
			float xPositionOfInstantiation = (((int)(Random.Range (0, 2))) == 0 ? -1 : 1) * (70 + Random.Range (0, 30));
			float yPositionOfInstantiation = (((int)(Random.Range (0, 2))) == 0 ? -1 : 1) * (70 + Random.Range (0, 30));
			Vector2 positionOfInstantiation = new Vector2 (xPositionOfInstantiation, yPositionOfInstantiation);

			Tappable target = GameObject.FindGameObjectWithTag ("Space Station").GetComponent <Tappable> ();

			MasterCreator.instance.CreateNewAsteroidHazard (positionOfInstantiation, target, 3, Random.Range(8, 13));

			yield return new WaitForSeconds (15);
		}
	}
}
