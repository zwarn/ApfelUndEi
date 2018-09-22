using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestScript : MonoBehaviour
{

	public GameObject[] allItems;
	public HashSet<String> questItems = null;

	void Start () {
//		GenerateQuest();
	}
	
	void Update () {
		
	}

	public void GenerateQuest()
	{
		var choosenItems = new HashSet<String>();
		while (choosenItems.Count < 3)
		{
			int index = Random.Range (0, allItems.Length);
			choosenItems.Add(allItems[index].name);
		}

		questItems = choosenItems;
	}
}
