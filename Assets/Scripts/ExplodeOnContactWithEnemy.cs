using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BattleSide))]
[RequireComponent (typeof(Collider2D))]
public class ExplodeOnContactWithEnemy : MonoBehaviour 
{
	//TODO: Make more graphic.  Looks SUPER lame right now.  

	private float damage = 0;

	public void SetDamage(float damage)
	{
		this.damage = damage;
	}

	[SerializeField] private Sprite explosionSprite = null;

	//Damage and such only applied once b/c this is destroyed right afterward.  
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent <Damageable> () != null)
		{
			other.gameObject.GetComponent <Damageable> ().Damaged (damage, GetComponent <Rigidbody2D> ());

			if (explosionSprite != null)
			{
				GameObject createdExplosion = new GameObject ("" + gameObject.name + " explosion");
				createdExplosion.transform.position = transform.position;
				createdExplosion.AddComponent <SpriteRenderer> ();
				createdExplosion.GetComponent <SpriteRenderer> ().sprite = explosionSprite;
				createdExplosion.AddComponent <SelfDestruct> ();
				createdExplosion.GetComponent <SelfDestruct> ().InSeconds (1);

				if (GetComponent <ProvideCashOnDeath> () != null)
					GetComponent <ProvideCashOnDeath> ().OnDeath ();
			}
			else
			{
				Debug.Log ("Couldn't instantiate explosion for " + gameObject.name + " because no explosion sprite was specified.");
			}

			Destroy (gameObject);
		}
	}
}
