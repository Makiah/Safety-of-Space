using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public static CameraMovement instance;
	void Awake() {instance = this;}

	private Tappable centeredOn;

	private Vector3 resetPosition = new Vector3 (0, 0, -10);

	private IEnumerator lerpToZCoroutine = null;

	public bool IsCenteredOn()
	{
		return centeredOn != null;
	}

	private bool gestureControlsEnabled = true;
	public void SetGestureControlsState(bool state)
	{
		gestureControlsEnabled = state;
	}

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

	/********* GESTURE CONTROLS ********/
	[SerializeField] private float scrollSpeed = 1;
	[SerializeField] private bool reverseScroll = false;

	private Vector3 dragOrigin;
	[SerializeField] private float dragSpeed = 1;
	void LateUpdate()
	{
		if (!IsCenteredOn () && gestureControlsEnabled)
		{
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor ||
			    Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				//Scrolling zoom.  
				float delta = Input.GetAxis ("Mouse ScrollWheel");
				if (delta != 0)
					transform.position = transform.position + new Vector3 (0, 0, (reverseScroll ? -1 : 1) * scrollSpeed * delta);

				if (transform.position.z > 0)
					transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

				//Dragging capabilities.  
				if (Input.GetMouseButtonDown (0))
				{
					dragOrigin = Input.mousePosition;
					return;
				}

				if (!Input.GetMouseButton (0))
					return;

				Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);
				Vector3 move = new Vector3 (-1 * pos.x * dragSpeed * -transform.position.z, -1 * pos.y * dragSpeed * -transform.position.z, 0);
				transform.Translate (move, Space.World);  

				dragOrigin = Input.mousePosition; //Prevent constant movement. 
			}
		}

		if (IsCenteredOn())
		{
			transform.position = new Vector3 (centeredOn.transform.position.x, centeredOn.transform.position.y, transform.position.z);
		}
	}
}
