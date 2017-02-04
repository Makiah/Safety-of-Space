using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour 
{
	public float currentHealth;
	private float maxHealth;

	[SerializeField] private bool useHealthBar;
	[SerializeField] private GameObject healthBarPrefab;

	private GameObject healthBar;

	void Start()
	{
		maxHealth = currentHealth;
		if (useHealthBar)
		{
			healthBar = (GameObject) (Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity));
			healthBar.GetComponent <FollowTransform> ().Follow (transform);
			healthBar.GetComponent <FollowTransform> ().SetOffset(new Vector3(0, 0, 0));
		}
	}

	public void Damaged(float damage)
	{
		currentHealth -= damage;

		Debug.Log ("Deducted damage for total of " + currentHealth);

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
