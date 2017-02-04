using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleSide))]
[RequireComponent(typeof(OrbitOther))]
public class Directable : MonoBehaviour 
{
	[SerializeField] private float moveSpeed = 3, stopDistance = 3;

	private IEnumerator moveToCoroutine = null;

	public Tappable targetDestination;

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

		moveToCoroutine = MoveTo (targetDestination);
		StartCoroutine (moveToCoroutine);
	}

	private IEnumerator MoveTo(Tappable target)
	{
		while (target != null)
		{
			//Move based on calculated values.  
			Vector3 targetPosition = target.transform.position;
			Vector3 diff = targetPosition - transform.position;

			if (Vector3.Distance (targetPosition, transform.position) > stopDistance)
			{
				//Update position
				Vector3 movement = diff.normalized;
				movement *= moveSpeed;
				transform.position += movement;
			}

			//Update heading regardless of distance from ideal position.  
			transform.localEulerAngles = new Vector3 (0, 0, Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg + 90);

			yield return null;
		}
	}
}
