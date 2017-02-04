using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSide : MonoBehaviour 
{
	[System.Serializable]
	public enum Side 
	{
		PLAYER_FRIENDLY, 
		PLAYER_HOSTILE
	}

	public Side side;
}
