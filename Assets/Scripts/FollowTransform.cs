using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour 
{
	[SerializeField] private Transform otherTransform;
	[SerializeField] private Vector3 offset;

	public void Follow(Transform other)
	{
		otherTransform = other;
	}

	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
	}

	void Update()
	{
		if (otherTransform != null)
		{
			transform.position = otherTransform.transform.position + offset;
		}
	}
}
