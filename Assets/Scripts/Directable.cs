using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directable : MonoBehaviour 
{
	[SerializeField] private float moveSpeed = 3, stopDistance = 3;

	private IEnumerator moveToCoroutine = null;

	public void DirectTo (Tappable target)
	{
		if (GetComponent <OrbitOther> () != null)
			GetComponent <OrbitOther> ().Deorbit ();

		if (moveToCoroutine != null)
			StopCoroutine (moveToCoroutine);
		
		moveToCoroutine = MoveTo (target);
		StartCoroutine (moveToCoroutine);
	}

	private IEnumerator MoveTo(Tappable target)
	{
		while (Vector3.Distance(target.transform.position, transform.position) > stopDistance)
		{
			//Update position
			Vector3 diff = target.transform.position - transform.position;
			Vector3 movement = diff.normalized;
			movement *= moveSpeed;
			transform.position += movement;

			//Update heading.  
			transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90);

			yield return null;
		}

		if (GetComponent <OrbitOther> () != null)
			GetComponent <OrbitOther> ().Orbit (target.transform);
	}
}
