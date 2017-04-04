using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSpaceStation : MonoBehaviour 
{
	[SerializeField] private float orbitSpeed = 1, orbitHorizontalAxis = 3, orbitVerticalAxis = 2;
	[SerializeField] public Transform targetDestination = null;

	protected virtual void Awake()
	{
		BeginToOrbit (targetDestination);
	}

	//Used to orbit different space stations.  
	protected IEnumerator orbitCoroutine = null;
	protected virtual void BeginToOrbit(Transform someSpaceStation)
	{
		//Start the orbiting coroutine.  
		orbitCoroutine = OrbitCoroutine ();
		StartCoroutine (orbitCoroutine);

		if (GetComponent <Rigidbody2D> () != null)
			GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
	}
	private IEnumerator OrbitCoroutine()
	{
		//Calculate values required to set initial position.  
		Vector3 diff = transform.position - targetDestination.position;
		float currentDegree = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;

		while (true)
		{
			//Calculate current degree based on time.  
			currentDegree += orbitSpeed;
			//Calculate X and Y.  
			float currentX = orbitHorizontalAxis * Mathf.Cos (currentDegree * Mathf.Deg2Rad); 
			float currentY = orbitVerticalAxis * Mathf.Sin (currentDegree * Mathf.Deg2Rad);
			//Update transform.  
			transform.position = new Vector3 (currentX, currentY, 0) + targetDestination.transform.position;
			//Update heading.  
			transform.localEulerAngles = new Vector3 (0, 0, -1 * (180 - currentDegree));

			yield return null;
		}
	}
}
