using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int Balance = 1000;
    public int PlayerNumber;
    public Color PlayerColor;
    public Text moneyUI;
    public StandScript[] stands;

    private float _movementSpeed = 0.1f;
    private Vector3 _lookingDirection = new Vector3();

    private GameObject item = null;
    private Interactable standInRange = null;

    void Update()
    {
        Vector3 deltaMovement = new Vector3(Input.GetAxis("Horizontal " + PlayerNumber),
            Input.GetAxis("Vertical " + PlayerNumber), 0);
        _lookingDirection = deltaMovement.normalized;
        transform.Translate(deltaMovement * _movementSpeed);

        if (standInRange != null && Input.GetButtonUp("Act " + PlayerNumber))
        {
            if (item != null)
            {
                standInRange.Give(this, item);

                HashSet<String> items = new HashSet<string>();
                foreach (var stand in stands)
                {
                    if (stand.yield != null)
                    {
                        items.Add(stand.yield.name);
                    }
                }
                if (QuestScript.Instance.Solve(items))
                {
                    foreach (var stand in stands)
                    {
                        Destroy(stand.yield);
                        stand.yield = null;
                    }
                }
            }
            else
            {
                item = standInRange.Take(this);
                if (item != null)
                {
                    item.transform.parent = gameObject.transform;
                    item.transform.localPosition = new Vector3();
                }
            }
        }

        moneyUI.text = Balance.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable stand = other.gameObject.GetComponent<Interactable>();
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