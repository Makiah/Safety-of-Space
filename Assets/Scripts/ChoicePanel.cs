using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour 
{
	public static ChoicePanel instance;
	void Awake() {instance = this;}

	class Choice
	{
		public GameObject objectComp;
		public Button buttonComp;
		public Text textComp;
		public Slider sliderComp;

		public Choice(Transform other)
		{
			objectComp = other.gameObject;
			if (other.FindChild("Upgrade") != null)
				buttonComp = other.FindChild("Upgrade").GetComponent <Button> ();
			if (other.FindChild("Text") != null)
				textComp = other.FindChild("Text").GetComponent <Text> ();
			if (other.FindChild("Slider") != null)
				sliderComp = other.FindChild("Slider").GetComponent <Slider> ();
		}
	}

	//The many required components for future reference.  
	private Choice targeting, shielding, moveSpeed, fireSpeed, fireDamage, directTo;
	private Choice[] choiceArray;

	void Start()
	{
		//Get ALL references.  
		targeting = new Choice(transform.FindChild("Targeting"));
		moveSpeed = new Choice (transform.FindChild ("Move Speed"));
		shielding = new Choice (transform.FindChild ("Shielding"));
		fireSpeed = new Choice (transform.FindChild ("Fire Speed"));
		fireDamage = new Choice (transform.FindChild ("Fire Damage"));
		directTo = new Choice (transform.FindChild ("Direct To"));

		//Create the array for future ease of coding.  
		choiceArray = new Choice[] {targeting, shielding, moveSpeed, fireSpeed, fireDamage, directTo};

		DisableAllActions ();

		NewPurchaseController.instance.SetState (false);
	}

	private void DisableAllActions()
	{
		foreach (Choice choice in choiceArray)
			choice.objectComp.SetActive (false);
		
		NewPurchaseController.instance.SetState (false);
  	}

	private void UpdateUpgradeCosts()
	{
		for (int i = 0; i < choiceArray.Length; i++)
		{
			if (choiceArray [i].textComp != null)
			{
				choiceArray [i].textComp.text = "" + (Mathf.Pow (5, currentlySelected.GetComponent <Upgradeable> ().upgradeSelections [i]));
			}
		}
	}

	private void UpdateUpgradeSliders()
	{
		for (int i = 0; i < choiceArray.Length; i++)
		{
			if (choiceArray [i].sliderComp != null)
			{
				choiceArray [i].sliderComp.value = (currentlySelected.GetComponent <Upgradeable> ().upgradeSelections [i] * 1.0f) / currentlySelected.GetComponent <Upgradeable> ().maxUpgradesForItem [i];
			}
		}
	}

	float lastTapOnNothingness = 0;
	private Tappable currentlySelected = null;

	public void InitializeWith (Tappable other)
	{
		//Make sure that the user didn't just click on the background.  
		if (other.gameObject.name.Equals ("Nothingness"))
		{
			//If we just double-tapped on nothingness, return the camera to its usual position.  
			if (Time.time - lastTapOnNothingness < .5f)
			{
				CameraMovement.instance.Decenter ();
				DisableAllActions ();
				UserConsole.instance.Clear();
				currentlyDirecting = false;
				currentlySelected = null;
			}

			lastTapOnNothingness = Time.time;
			return;
		}

		if (currentlySelected != null)
		{
			if (currentlyDirecting)
			{
				currentlySelected.GetComponent <Directable> ().DirectTo (other);
				currentlyDirecting = false;
				UserConsole.instance.Clear ();
				currentlySelected = null;
				return;
			}
			else
			{
				DisableAllActions ();
			}
		}

		//Zoom in on the item.  
		CameraMovement.instance.CenterOn(other);

		//Play the opening animation.  
		GetComponent <Animator> ().SetTrigger("PlayOpening");

		//Set each component active depending on whether or not it has the required components.  
		//Make sure that the upgradeable component exists on this item first.  .  
		currentlySelected = other;

		if (other.gameObject.GetComponent <Upgradeable> () != null)
		{
			UpdateUpgradeCosts ();
			UpdateUpgradeSliders ();

			if (other.gameObject.GetComponent <Damageable> () != null)
			{
				shielding.objectComp.SetActive (true);
			}
		
			if (other.gameObject.GetComponent <Directable> () != null)
			{
				moveSpeed.objectComp.SetActive (true);
			}

			if (other.gameObject.GetComponent <GunController> () != null)
			{
				fireSpeed.objectComp.SetActive (true);
				fireDamage.objectComp.SetActive (true);
			}

			if (other.gameObject.GetComponent <TargetingControl> () != null)
			{
				targeting.objectComp.SetActive (true);
			}
		}
	
		if (other.gameObject.GetComponent <Directable> () != null)
		{
			directTo.objectComp.SetActive (true);
		}

		if (other.gameObject.CompareTag ("Space Station"))
		{
			NewPurchaseController.instance.SetState (true);
		}
	}

	/************** Upgrade choice options *****************/

	private bool CompletePrelimUpgradeStuff(int choice)
	{
		//Make sure that this component can be upgraded (hasn't reached max upgrades.  
		if (currentlySelected.GetComponent <Upgradeable> ().upgradeSelections [choice] >= currentlySelected.GetComponent <Upgradeable> ().maxUpgradesForItem [choice])
		{
			UserConsole.instance.Output ("Already fully upgraded!", Color.red, 1);
			return false;
		}

		//Determine the cost and make sure that the purchase can be made before making it.  Then update the cost of the new upgrade.  
		int cost = int.Parse(choiceArray[choice].textComp.text);
		if (!ResourceController.instance.ChangeBy (-cost))
		{
			UserConsole.instance.Output ("Insufficient funds!", Color.red, 1);
			return false;
		}
		currentlySelected.GetComponent <Upgradeable> ().upgradeSelections [choice]++;
		UpdateUpgradeCosts ();
		UpdateUpgradeSliders ();

		return true; //Only return true if everything else went without a hitch.  
	}

	public void OnTargetingChosen()
	{
		if (!CompletePrelimUpgradeStuff (0))
			return;

		//Now do the actual upgrade thingamajig.  
		currentlySelected.GetComponent <TargetingControl> ().ImproveTargeting();
	}

	public void OnShieldingChosen()
	{
		if (!CompletePrelimUpgradeStuff (1))
			return;

		//Now do the actual upgrade thingamajig.  
		currentlySelected.GetComponent <Damageable> ().armorStrength++;
	}

	public void OnMoveSpeedChosen()
	{
		if (!CompletePrelimUpgradeStuff (2))
			return;

		//Now do the actual upgrade thingamajig.  
		currentlySelected.GetComponent <Directable> ().thrustPower *= 2;
	}

	public void OnFireSpeedChosen()
	{
		if (!CompletePrelimUpgradeStuff (3))
			return;

		//Now do the actual upgrade thingamajig.  
		currentlySelected.GetComponent <GunController> ().fireDelay /= 2.0f;
	}

	public void OnFireDamageChosen()
	{
		if (!CompletePrelimUpgradeStuff (4))
			return;

		//Now do the actual upgrade thingamajig.  
		currentlySelected.GetComponent <GunController> ().fireDamage++;
	}

	private bool currentlyDirecting = false;
	public void OnDirectToChosen()
	{
		DisableAllActions ();
		UserConsole.instance.Output ("Choose object to direct to", Color.green, 3);
		CameraMovement.instance.Decenter ();
		currentlyDirecting = true;
	}
}
