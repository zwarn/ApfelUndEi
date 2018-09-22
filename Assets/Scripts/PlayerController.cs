using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int Balance;
	public int PlayerNumber;
	public Color PlayerColor;

	private float _movementSpeed = 0.1f;
	private Vector3 _lookingDirection = new Vector3();

	private GameObject item = null;
	private StandScript standInRange = null;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Movement
		Vector3 deltaMovement = new Vector3(Input.GetAxis("Horizontal " + PlayerNumber), Input.GetAxis("Vertical " + PlayerNumber),0);
		_lookingDirection = deltaMovement.normalized;
		transform.Translate(deltaMovement * _movementSpeed);

		if (standInRange != null && Input.GetButtonUp("Act " + PlayerNumber))
		{
			if (item != null)
			{
				standInRange.Give(item);	
			} else
			{
				item = standInRange.Take();
				item.transform.parent = gameObject.transform;
				item.transform.localPosition = new Vector3();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		StandScript stand = other.gameObject.GetComponent<StandScript>();
		if (stand != null)
		{
			standInRange = stand;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (standInRange != null && other.gameObject == standInRange.gameObject)
		{
			standInRange = null;
		}
	}
}
