using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadSelectionManager : MonoBehaviour 
{
	public static SquadSelectionManager instance;
	void Awake() {instance = this;}

	[SerializeField] private GameObject squadSelectablePrefab = null;

	public void Add(SquadSelectable squadSelectable)
	{
		GameObject createdPanel = (GameObject)(Instantiate (squadSelectablePrefab, transform.GetChild(0).GetChild(0), false));

		//Change the image so that it shows the correct item.  
		createdPanel.GetComponent <Image> ().sprite = squadSelectable.GetComponent <SpriteRenderer> ().sprite;

		//Change the button's onClick function so that it selects this object.  Done with Unity Forums(!)
		createdPanel.GetComponent <Button> ().onClick.AddListener(
			delegate 
			{ 
				ChoicePanel.instance.InitializeWith (squadSelectable.GetComponent <Tappable> ()); 
			}
		);
	}
}
