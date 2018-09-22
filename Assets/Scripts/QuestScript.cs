using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestScript : MonoBehaviour
{

	public static QuestScript Instance = null;
	
	public GameObject[] allItems;
	public HashSet<String> questItems;
	public Image[] itemImages;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	void Start () {
		GenerateQuest();
	}

	public bool Solve(HashSet<String> items)
	{
		return questItems.SetEquals(items);
	}

	public void GenerateQuest()
	{
		var choosenItems = new HashSet<String>();
		while (choosenItems.Count < 3)
		{
			int index = Random.Range (0, allItems.Length);
			choosenItems.Add(allItems[index].name);
			itemImages[choosenItems.Count - 1].sprite = allItems[index].GetComponentInChildren<SpriteRenderer>().sprite;
		}

		questItems = choosenItems;
	}
}
