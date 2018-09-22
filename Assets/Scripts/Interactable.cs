using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

	public abstract void Give(PlayerController player, GameObject obj);

	public abstract GameObject Take(PlayerController player);
	
	protected void placeItem(GameObject item)
	{
		item.transform.parent = transform;
		item.transform.localPosition = new Vector3();
	}
}
