using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamageOnContact : MonoBehaviour 
{
	float damage = 0;

	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent <Rigidbody2D> ();
	}

	public void SetDamage(float value)
	{
		damage = value;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent <Damageable> () != null)
		{
			other.gameObject.GetComponent <Damageable> ().Damaged (damage, rb2d);
			Destroy (gameObject);
		}
	}
}
