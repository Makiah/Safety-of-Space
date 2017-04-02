using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour 
{
	public void InSeconds(float secondsUntilDestruction)
	{
		//Although a coroutine would also work this is just easier.  
		Invoke ("DestroySelf", secondsUntilDestruction);
	}

	private void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
