using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPurchaseController : MonoBehaviour 
{
	//Singleton.  
	public static NewPurchaseController instance;
	void Awake() {instance = this;}

	[SerializeField] private GameObject purchaseSelectionPrefab = null;

	void Start()
	{
		AddAvailablePurchase (1);
	}

	public void SetState(bool state)
	{
		gameObject.SetActive (state);
	}

	//Adds new buttons to the menu which enable the purchase of new and unique items as a result.  
	public void AddAvailablePurchase(int item)
	{
		GameObject instantiatedPanel = (GameObject)(Instantiate (purchaseSelectionPrefab, transform.GetChild (0).GetChild (0), false));

		Sprite chosenSprite = null;
		int costOfItem = 0;

		switch (item)
		{
			case 1: 
				chosenSprite = MasterCreator.instance.fleetShip.GetComponent <SpriteRenderer> ().sprite;
				costOfItem = 10;
				instantiatedPanel.GetComponent <Button> ().onClick.AddListener (
					delegate { 
						if (ResourceController.instance.ChangeBy(-costOfItem))
							MasterCreator.instance.CreateNewFleetShip (Vector3.zero, GameObject.FindGameObjectWithTag ("Space Station").GetComponent <Tappable> ().transform); 
					}
				);
				break;
		}

		instantiatedPanel.transform.FindChild("Item Icon").GetComponent <Image> ().sprite = chosenSprite;
		instantiatedPanel.transform.FindChild ("Resource Cost").FindChild ("Text").GetComponent <Text> ().text = "" + costOfItem;
	}
}
