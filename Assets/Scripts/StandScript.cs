using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StandScript : Interactable
{

	public GameObject yield;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Give(PlayerController player, GameObject obj)
	{
		if (yield == null)
		{
			yield = obj;
			placeItem(obj);
		}
	}

	public override GameObject Take(PlayerController player)
	{
		GameObject result = yield;
		yield = null;
		return result;
	}
}
