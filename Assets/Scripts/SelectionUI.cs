using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour 
{
	public static SelectionUI instance;
	void Awake() {instance = this;}

	[SerializeField] private Button directable = null, upgradable = null;
	[SerializeField] private Text userOutput;

	private Tappable tappable;

	private float lastPressOnNothingness;

	public void GotMouseDownOn(Tappable tappable)
	{
		if (this.tappable == null)
		{
			if (tappable.gameObject.name.Equals ("Nothingness"))
				return;
			
			this.tappable = tappable;

			if (tappable.directableComp != null)
				directable.gameObject.SetActive (true);
			if (tappable.upgradableComp != null)
				upgradable.gameObject.SetActive (true);

			CameraMovement.instance.CenterOn (tappable);
		}
		else
		{
			if (tappable.gameObject.name.Equals ("Nothingness"))
			{
				if (Time.time - lastPressOnNothingness < .300) //Only zoom out after a double tap.  
				{
					Debug.Log ("Resetting camera view after nothingness double clicked.");
					CameraMovement.instance.Decenter ();
					Deinit ();
				}
				lastPressOnNothingness = Time.time;

				return;
			}

			if (tappableBeingDirected)
			{
				if (this.tappable != tappable)
					this.tappable.directableComp.DirectTo (tappable);
			}
			else if (tappableBeingUpgraded)
			{
				Debug.Log ("Not yet implemented");
			}

			Deinit (); //Resets all booleans and other such stuff.  
		}
	}

	//Used to remove any content from the initial selection UI.  
	public void Deinit()
	{
		userOutput.text = "";
		userOutput.gameObject.SetActive (false);
		directable.gameObject.SetActive (false);
		upgradable.gameObject.SetActive (false);

		tappable = null;
		tappableBeingDirected = false;
		tappableBeingUpgraded = false;
	}

	private bool tappableBeingDirected = false;
	public void OnDirectClicked()
	{
		upgradable.gameObject.SetActive (false);
		userOutput.gameObject.SetActive (true);
		userOutput.text = "Tap on object that this object will be directed to.";
		tappableBeingDirected = true;
		CameraMovement.instance.Decenter ();
	}

	private bool tappableBeingUpgraded = false;
	public void OnUpgradeClicked()
	{
		directable.gameObject.SetActive (false);
		userOutput.gameObject.SetActive (true);
		userOutput.text = "Choose the appropriate upgrade";
		tappableBeingUpgraded = true;
		//Place upgrade code stuff here
	}
}
