using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour 
{
	public void OnMouseDown()
	{
		Debug.Log ("Got drag on " + gameObject.name);

		if (!EventSystem.current.IsPointerOverGameObject ())
		{
			CameraMovement.instance.SetGestureControlsState (false);
			StartCoroutine ("FollowMouse");
		}
	}

	public void OnMouseUp()
	{
		if (!EventSystem.current.IsPointerOverGameObject ())
		{
			CameraMovement.instance.SetGestureControlsState (true);
			StopCoroutine ("FollowMouse");
		}
	}

	private IEnumerator FollowMouse()
	{
		while (true)
		{
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			yield return null;
		}
	}
}
