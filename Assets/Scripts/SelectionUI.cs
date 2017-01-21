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
				Debug.Log ("Resetting camera view after nothingness clicked.");
				CameraMovement.instance.Decenter ();
				Deinit ();
				return;
			}

			if (tappableBeingDirected)
			{
				this.tappable.directableComp.DirectTo (tappable);
				Deinit ();
			}
			else if (tappableBeingUpgraded)
			{
				Debug.Log ("Not yet implemented");
				Deinit ();
			}
			
		}
	}

	//Used to remove any content from the initial selection UI.  
	public void Deinit()
	{
		userOutput.text = "";
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
		userOutput.text = "Tap on object that this object will be directed to.";
		tappableBeingDirected = true;
		CameraMovement.instance.Decenter ();
	}

	private bool tappableBeingUpgraded = false;
	public void OnUpgradeClicked()
	{
		directable.gameObject.SetActive (false);
		userOutput.text = "Choose the appropriate upgrade";
		tappableBeingUpgraded = true;
		//Place upgrade code stuff here
	}
}
