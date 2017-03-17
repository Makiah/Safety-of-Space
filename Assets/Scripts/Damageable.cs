using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour 
{
	public float currentHealth;
	public float maxHealth;
	public float armorStrength = 1;

	[SerializeField] private bool useHealthBar;
	[SerializeField] private GameObject healthBarPrefab;

	private GameObject healthBar;
	private Rigidbody2D rb2d;

	void Awake()
	{
		maxHealth = currentHealth;
		if (useHealthBar)
		{
			healthBar = (GameObject)(Instantiate (healthBarPrefab, Vector3.zero, Quaternion.identity, transform));
			healthBar.transform.localPosition = Vector3.zero;
		}

		rb2d = GetComponent <Rigidbody2D> ();
	}

	public void Damaged(float damage, Rigidbody2D otherRigidbody)
	{
		currentHealth -= damage / armorStrength;

		Vector3 forceToBeApplied = (rb2d.velocity - otherRigidbody.velocity * otherRigidbody.mass) / rb2d.mass;

		rb2d.AddForce (forceToBeApplied);

		if (currentHealth <= 0)
		{
			Destroy (gameObject);
			Destroy (healthBar.gameObject);
		}

		if (useHealthBar)
		{
			healthBar.transform.GetChild(0).localScale = new Vector3(currentHealth / maxHealth, 1, 1);
		}
	}
}
