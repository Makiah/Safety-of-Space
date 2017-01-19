using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ApplyRandomOrbit : MonoBehaviour 
{
	[SerializeField] private float maxSpeed = 1;

	void Start()
	{
		GetComponent <Rigidbody2D> ().angularVelocity = (Random.value * 2 - 1) * maxSpeed;
	}
}
