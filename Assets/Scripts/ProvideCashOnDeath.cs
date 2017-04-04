using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvideCashOnDeath : MonoBehaviour
{
	[SerializeField] private int worth = 10;

	public void OnDeath()
	{
		ResourceController.instance.ChangeBy (worth);
	}
}
