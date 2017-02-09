using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour 
{
	public static ChoicePanel instance;
	void Awake() {instance = this;}

	//The many required components for future reference.  
	private Button targetingB, moveSpeedB, shieldingB, fireSpeedB, fireDamageB, directToB;
	private Slider targetingS, moveSpeedS, shieldingS, fireSpeedS, fireDamageS, directToS;

	void Start()
	{
		//Get ALL references.  
		targetingB = transform.FindChild("Targeting").FindChild("Upgrade").GetComponent <Button> ();
		targetingS = transform.FindChild("Targeting").FindChild("Slider").GetComponent <Slider> ();

		moveSpeedB = transform.FindChild("Move Speed").FindChild("Upgrade").GetComponent <Button> ();
		moveSpeedS = transform.FindChild("Move Speed").FindChild("Slider").GetComponent <Slider> ();

		shieldingB = transform.FindChild("Shielding").FindChild("Upgrade").GetComponent <Button> ();
		shieldingS = transform.FindChild("Shielding").FindChild("Slider").GetComponent <Slider> ();

		fireSpeedB = transform.FindChild("Fire Speed").FindChild("Upgrade").GetComponent <Button> ();
		fireSpeedS = transform.FindChild("Fire Speed").FindChild("Slider").GetComponent <Slider> ();

		fireDamageB = transform.FindChild("Fire Damage").FindChild("Upgrade").GetComponent <Button> ();
		fireDamageS = transform.FindChild("Fire Damage").FindChild("Slider").GetComponent <Slider> ();

		directToB = transform.FindChild("Direct To").FindChild("Upgrade").GetComponent <Button> ();
		directToS = transform.FindChild("Direct To").FindChild("Slider").GetComponent <Slider> ();

		DisableAllActions ();
	}

	void DisableAllActions()
	{
		targetingB.gameObject.SetActive (false);
		targetingS.gameObject.SetActive (false);

		moveSpeedB.gameObject.SetActive (false);
		moveSpeedS.gameObject.SetActive (false);

		shieldingB.gameObject.SetActive (false);
		shieldingS.gameObject.SetActive (false);

		fireSpeedB.gameObject.SetActive (false);
		fireSpeedS.gameObject.SetActive (false);

		fireDamageB.gameObject.SetActive (false);
		fireDamageS.gameObject.SetActive (false);

		directToB.gameObject.SetActive (false);
		directToS.gameObject.SetActive (false);
  	}

	float lastTapOnNothingness = 0;
	private Tappable currentlySelected = null;

	public void InitializeWith (Tappable other)
	{
		//Make sure that the user didn't just click on the background.  
		if (other.gameObject.name.Equals ("Nothingness"))
		{
			//If we just double-tapped on nothingness.  
			if (Time.time - lastTapOnNothingness < .5f)
			{
				CameraMovement.instance.Decenter ();
				DisableAllActions ();
				UserConsole.instance.Output ("");
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
				UserConsole.instance.Output ("");
				currentlySelected = null;
			}
			return;
		}

		//Zoom in on the item.  
		CameraMovement.instance.CenterOn(other);

		//Play the opening animation.  
		GetComponent <Animator> ().SetTrigger("PlayOpening");

		//Set each component active depending on whether or not it has the required components.  
		//Make sure that the upgradeable component exists on this item first.  .  
		if (other.gameObject.GetComponent <Upgradeable> () != null)
		{
			shieldingB.gameObject.SetActive (true);
			shieldingS.gameObject.SetActive (true);

			if (other.gameObject.GetComponent <Directable> () != null)
			{
				directToB.gameObject.SetActive (true);
				directToS.gameObject.SetActive (true);
			}

			if (other.gameObject.GetComponent <GunController> () != null)
			{
				targetingB.gameObject.SetActive (true);
				targetingS.gameObject.SetActive (true);

				fireSpeedB.gameObject.SetActive (true);
				fireSpeedS.gameObject.SetActive (true);

				fireDamageB.gameObject.SetActive (true);
				fireDamageS.gameObject.SetActive (true);
			}
		}
		else if (other.gameObject.GetComponent <Directable> () != null)
		{
			directToB.gameObject.SetActive (true);
			directToS.gameObject.SetActive (true);
		}

		currentlySelected = other;
	}

	/************** Upgrade choice options *****************/

	public void OnTargetingChosen()
	{
		
	}

	public void OnShieldingChosen()
	{

	}

	public void OnMoveSpeedChosen()
	{

	}

	public void OnFireSpeedChosen()
	{

	}

	public void OnFireDamageChosen()
	{

	}

	private bool currentlyDirecting = false;
	public void OnDirectToChosen()
	{
		DisableAllActions ();
		UserConsole.instance.Output ("Choose object to direct to");
		CameraMovement.instance.Decenter ();
		currentlyDirecting = true;
	}
}
