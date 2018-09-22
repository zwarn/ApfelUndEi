using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public int PlayerNumber;
	public Color PlayerColor;

	private float _movementSpeed = 0.1f;
	private Vector3 _lookingDirection = new Vector3();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Movement
		Vector3 deltaMovement = new Vector3(Input.GetAxis("Horizontal " + PlayerNumber), Input.GetAxis("Vertical " + PlayerNumber),0);
		_lookingDirection = deltaMovement.normalized;
		transform.Translate(deltaMovement * _movementSpeed);
	}
}
