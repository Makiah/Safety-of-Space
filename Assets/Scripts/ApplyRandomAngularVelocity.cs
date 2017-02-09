using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ApplyRandomAngularVelocity : MonoBehaviour 
{
	[SerializeField] private float maxSpeed = 10, minSpeed = 3;

	void Start()
	{
		//1 in 2 chance of being negative, same with positive.  
		GetComponent <Rigidbody2D> ().angularVelocity = Random.Range (minSpeed, maxSpeed) * (Random.Range (0, 2) == 0 ? 1 : -1);
	}
}
