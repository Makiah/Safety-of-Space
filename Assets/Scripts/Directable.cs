using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleSide))]
[RequireComponent(typeof(Rigidbody2D))]
public class Directable : MonoBehaviour 
{
	[SerializeField] public float thrustPower = 2, maxSpeed = 15, stopDistance = 2;

	private IEnumerator moveToCoroutine = null;

	[HideInInspector] public Tappable targetDestination; //As unnecessary as this may seem, keep it in since it is used by GunController.  

	private Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = GetComponent <Rigidbody2D> ();
	}

	public void DirectTo (Tappable target)
	{
		if (GetComponent <OrbitOther> () != null)
			GetComponent <OrbitOther> ().Deorbit ();

		if (moveToCoroutine != null)
		{
			targetDestination = null;
			StopCoroutine (moveToCoroutine);
		}

		targetDestination = target;

		moveToCoroutine = DirectToCoroutine ();
		StartCoroutine (moveToCoroutine);
	}

	private IEnumerator DirectToCoroutine()
	{
		while (targetDestination != null)
		{
			//Move based on calculated values.  
			Vector2 targetPosition = new Vector2(targetDestination.transform.position.x, targetDestination.transform.position.y);
			Vector2 diff = targetPosition - new Vector2(transform.position.x, transform.position.y);

			if (Vector2.Distance (targetPosition, transform.position) > stopDistance)
			{
				//Update velocity (make sure not to go over the max velocity)
				Vector2 thrustVector = diff.normalized * thrustPower;

				if ((rb2d.velocity + thrustVector).magnitude <= maxSpeed)
				{
					rb2d.AddForce (thrustVector);
				}
				else
				{
					rb2d.velocity = (rb2d.velocity + thrustVector).normalized * maxSpeed;
				}
			}
			else
			{
				
				rb2d.velocity *= 0.95f;
			}

			//Update heading regardless of distance from ideal position.  
			transform.localEulerAngles = new Vector3 (0, 0, Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg + 90);

			yield return null;
		}
	}

	public bool IsBeingDirected()
	{
		return moveToCoroutine == null;
	}
}
