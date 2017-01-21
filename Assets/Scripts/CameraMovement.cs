using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public static CameraMovement instance;
	void Awake() {instance = this;}

	Tappable centeredOn;

	private Vector3 resetPosition = new Vector3 (0, 0, -10);

	public void CenterOn(Tappable other)
	{
		transform.position = new Vector3 (0, 0, -4 * other.transform.localScale.x);
			
		centeredOn = other;
	}

	public void Decenter()
	{
		centeredOn = null;
		transform.position = resetPosition;
	}

	void Update()
	{
		if (centeredOn != null)
			transform.position = new Vector3(centeredOn.transform.position.x, centeredOn.transform.position.y, transform.position.z);
	}
}
