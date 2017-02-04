using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureControls : MonoBehaviour 
{
	[SerializeField] private float scrollSpeed = 1;
	[SerializeField] private bool reverseScroll = false;

	private Vector3 dragOrigin;
	[SerializeField] private float dragSpeed = 4;
	void LateUpdate()
	{
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || 
			Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			//Scrolling zoom.  
			float delta = Input.GetAxis ("Mouse ScrollWheel");
			if (delta != 0)
				Camera.main.transform.position = Camera.main.transform.position + new Vector3 (0, 0, (reverseScroll ? -1 : 1) * scrollSpeed * delta);

			//Dragging capabilities.  
			if (Input.GetMouseButtonDown(0))
			{
				dragOrigin = Input.mousePosition;
				return;
			}

			if (!Input.GetMouseButton(0)) return;

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(-1 * pos.x * dragSpeed, -1 * pos.y * dragSpeed, 0);
			Camera.main.transform.Translate(move, Space.World);  

			dragOrigin = Input.mousePosition; //Prevent constant movement. 
			
		}
	}
}
