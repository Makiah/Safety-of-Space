using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public static CameraMovement instance;
	void Awake() {instance = this;}

	Tappable centeredOn;

	private Vector3 resetPosition = new Vector3 (0, 0, -10);

	private IEnumerator lerpToZCoroutine = null;

	public void CenterOn(Tappable other)
	{
		if (lerpToZCoroutine != null)
			StopCoroutine (lerpToZCoroutine);
		
		lerpToZCoroutine = LerpTransformToZ (-6 * other.transform.localScale.x);
		StartCoroutine (lerpToZCoroutine);

		centeredOn = other;
	}

	//Used when the camera zooms in on an item or zooms out on an item.  
	private IEnumerator LerpTransformToZ (float desiredZ)
	{
		float startTime = Time.time, permittedTime = 1;
		float lerpState = 0;
		float initialZ = transform.position.z;

		while (lerpState < 1)
		{
			lerpState = (Time.time - startTime) / permittedTime;
			transform.position = Vector3.Lerp (
				new Vector3 (transform.position.x, transform.position.y, initialZ), 
				new Vector3 (transform.position.x, transform.position.y, desiredZ), 
				lerpState); 

			yield return null;
		}

		transform.position = new Vector3(transform.position.x, transform.position.y, desiredZ);
	}

	public void Decenter()
	{
		if (lerpToZCoroutine != null)
			StopCoroutine (lerpToZCoroutine);

		lerpToZCoroutine = LerpTransformToZ (resetPosition.z);
		StartCoroutine (lerpToZCoroutine);

		centeredOn = null;
	}

	void Update()
	{
		if (centeredOn != null)
		{
			transform.position = new Vector3 (centeredOn.transform.position.x, centeredOn.transform.position.y, transform.position.z);
		}
	}
}
