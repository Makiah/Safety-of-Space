using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Directable))]
[RequireComponent(typeof(BattleSide))]

public class GunController : MonoBehaviour 
{
	//Public because it can be modified by both upgrades and the UI.  
	public float fireDelay = 1, fireRange = 10, fireDamage = 1, fireForce = 500;
	[SerializeField] private Sprite customBulletImage = null;
	[SerializeField] private GameObject bulletPrefab = null;
	[SerializeField] private bool enableTrailOnBullet = true, enableRocketExhaustOnBullet = true;
	private Directable directableComp;
	private Transform rangeTransform, fireTransform;

	private IEnumerator fireCoroutine;

	void Start()
	{
		directableComp = GetComponent <Directable> ();
		rangeTransform = new GameObject ("Range Transform").transform;
		rangeTransform.SetParent (transform);
		rangeTransform.localPosition = new Vector3 (0, -fireRange, 0);
		fireTransform = new GameObject ("Fire Transform").transform;
		fireTransform.SetParent (transform);
		fireTransform.localPosition = new Vector3 (0, -1, 0);

		EnableFire ();
	}

	public void DisableFire()
	{
		if (fireCoroutine != null)
			StopCoroutine (fireCoroutine);
	}

	public void EnableFire()
	{
		if (fireCoroutine == null)
		{
			fireCoroutine = ContinuouslyFire ();
			StartCoroutine (fireCoroutine);
		}
	}

	//Searches to see if the target destination is within range, and if it is, instantiates bullets heading in that direction.  
	private IEnumerator ContinuouslyFire()
	{
		while (true)
		{
			if (directableComp != null)
			{
				if (directableComp.targetDestination != null)
				{
					RaycastHit2D[] linecastResults = Physics2D.LinecastAll (transform.position, rangeTransform.position, 1 << LayerMask.NameToLayer("Interactable"));
					if (linecastResults != null)
					{
						Damageable damageableComp = null;
						//Search through to find the one being directed to, and determine whether it is on the opposing side.  
						foreach (RaycastHit2D hit in linecastResults)
							if (hit.collider.gameObject.GetComponent <Tappable> () == directableComp.targetDestination)
								if (GetComponent <BattleSide> ().side != directableComp.targetDestination.gameObject.GetComponent <BattleSide> ().side)
									damageableComp = hit.collider.gameObject.GetComponent <Damageable> ();

						if (damageableComp != null)
						{
							GameObject instantiatedPrefab = (GameObject) (Instantiate (bulletPrefab, fireTransform.position, Quaternion.identity));
							instantiatedPrefab.GetComponent <SpriteRenderer> ().sprite = customBulletImage;
							instantiatedPrefab.GetComponent <TrailRenderer> ().enabled = enableTrailOnBullet;
							instantiatedPrefab.transform.GetChild (0).GetComponent <SpriteRenderer> ().enabled = enableRocketExhaustOnBullet;

							if (bulletPrefab.GetComponent <Rigidbody2D> () != null)
							{
								Vector2 diff = damageableComp.gameObject.transform.position - transform.position;
								instantiatedPrefab.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
								instantiatedPrefab.GetComponent <Rigidbody2D> ().AddRelativeForce(new Vector3 (0, fireForce, 0));
								instantiatedPrefab.GetComponent <ExplodeOnContactWithEnemy> ().SetDamage (fireDamage);
							}
						}
					}
				}
			}

			yield return new WaitForSeconds (fireDelay);
		}
	}
}
