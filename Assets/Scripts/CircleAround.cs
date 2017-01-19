using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAround : MonoBehaviour 
{
	[SerializeField] private Transform targetTransform = null;
	[SerializeField] private float speed = 1, xAxis = 3, yAxis = 2;
	[SerializeField] private bool clockwise;

	private float currentDegree;
	void OnEnable() 
	{
		currentDegree = Mathf.Atan2 (transform.position.y, transform.position.x) * Mathf.Rad2Deg;
	}

	float currentX, currentY;
	void FixedUpdate()
	{
		if (targetTransform != null)
		{
			//Determine direction
			int clockwiseCoefficient = clockwise ? 1 : -1;
			//Calculate current degree based on time.  
			currentDegree += clockwiseCoefficient * speed;
			//Calculate X and Y.  
			currentX = xAxis * Mathf.Cos (currentDegree * Mathf.Deg2Rad); 
			currentY = yAxis * Mathf.Sin (currentDegree * Mathf.Deg2Rad);
			//Update transform.  
			transform.position = new Vector3 (currentX, currentY, 0) + targetTransform.transform.position;
			//Update heading.  
			transform.localEulerAngles = new Vector3(0, 0, clockwise ? -1 * (180 - currentDegree) : currentDegree);
		}
	}

}
