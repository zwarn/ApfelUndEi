using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestScript : MonoBehaviour
{

	public GameObject[] allItems;
	public HashSet<GameObject> questItems;
	public Image[] itemImages;

	void Start () {
		GenerateQuest();
	}
	
	void Update () {
		
	}

	public void GenerateQuest()
	{
		var choosenItems = new HashSet<GameObject>();
		while (choosenItems.Count < 3)
		{
			int index = Random.Range (0, allItems.Length);
			choosenItems.Add(allItems[index]);
			itemImages[choosenItems.Count - 1].sprite = allItems[index].GetComponent<SpriteRenderer>().sprite;
		}

		questItems = choosenItems;
	}
}
