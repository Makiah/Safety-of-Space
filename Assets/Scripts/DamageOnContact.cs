using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour 
{
	float damage = 0;

	public void SetDamage(float value)
	{
		damage = value;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent <Damageable> () != null)
		{
			other.gameObject.GetComponent <Damageable> ().Damaged (damage);
			Destroy (gameObject);
		}
	}
}
