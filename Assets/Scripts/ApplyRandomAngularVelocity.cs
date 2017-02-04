using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ApplyRandomAngularVelocity : MonoBehaviour 
{
	[SerializeField] private float maxSpeed = 10, minSpeed = 3;

	void Start()
	{
		GetComponent <Rigidbody2D> ().angularVelocity = (Random.value * minSpeed - minSpeed / 2f) * maxSpeed;
	}
}
