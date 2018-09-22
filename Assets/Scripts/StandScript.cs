using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StandScript : MonoBehaviour
{

	public GameObject yield;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Give(GameObject obj)
	{
		if (yield == null)
		{
			yield = obj;
			obj.transform.parent = transform;
			obj.transform.localPosition = new Vector3();
		}
	}

	public GameObject Take()
	{
		GameObject result = yield;
		yield = null;
		return result;
	}
}
