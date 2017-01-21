using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitOther : MonoBehaviour 
{
	[SerializeField] private Transform orbitTransform = null;
	[SerializeField] private float speed = 1, xAxis = 3, yAxis = 2;
	[SerializeField] private bool clockwise;

	private float currentDegree;

	void Start()
	{
		Orbit (orbitTransform);
	}

	public void Orbit(Transform other) 
	{
		orbitTransform = other;
		Vector3 diff = transform.position - orbitTransform.position;
		currentDegree = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
	}

	public void Deorbit()
	{
		orbitTransform = null;
	}

	float currentX, currentY;
	void FixedUpdate()
	{
		if (orbitTransform != null)
		{
			//Determine direction
			int clockwiseCoefficient = clockwise ? 1 : -1;
			//Calculate current degree based on time.  
			currentDegree += clockwiseCoefficient * speed;
			//Calculate X and Y.  
			currentX = xAxis * Mathf.Cos (currentDegree * Mathf.Deg2Rad); 
			currentY = yAxis * Mathf.Sin (currentDegree * Mathf.Deg2Rad);
			//Update transform.  
			transform.position = new Vector3 (currentX, currentY, 0) + orbitTransform.transform.position;
			//Update heading.  
			transform.localEulerAngles = new Vector3(0, 0, clockwise ? -1 * (180 - currentDegree) : currentDegree);
		}
	}

}
