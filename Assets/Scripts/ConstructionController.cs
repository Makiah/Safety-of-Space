using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionController : MonoBehaviour 
{
	public static ConstructionController instance;
	void Awake() { instance = this; }

	[SerializeField] private Sprite[] parts = null;
	[SerializeField] private GameObject scrollRectOptionPrefab = null;
	[SerializeField] private GameObject constructionComponent = null;

	private Transform beingConstructed;

	public void Start()
	{
		//Create the parent of the sprites which will be instantiated into it.  
		beingConstructed = new GameObject ("Prefab Being Constructed").transform;

		//Create option panels for every part listed above.  
		foreach (Sprite part in parts)
		{
			GameObject createdScrollRectPrefab = (GameObject)(Instantiate (scrollRectOptionPrefab, transform.GetChild(0).GetChild(0).GetChild(0), false));
			createdScrollRectPrefab.GetComponent <Image> ().sprite = part;
			createdScrollRectPrefab.GetComponent <Button> ().onClick.AddListener(
				delegate 
				{ 
					AddToConstruction(createdScrollRectPrefab.GetComponent <Image> ().sprite);
				}
			);
		}

		SetState (false);
	}

	public void SetState(bool active)
	{
		gameObject.SetActive (active);
	}

	public void AddToConstruction(Sprite part)
	{
		GameObject createdConstructionPrefab = (GameObject)(Instantiate (constructionComponent, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Quaternion.identity));
		createdConstructionPrefab.transform.SetParent (beingConstructed);
		createdConstructionPrefab.GetComponent <SpriteRenderer> ().sprite = part;
	}
}
