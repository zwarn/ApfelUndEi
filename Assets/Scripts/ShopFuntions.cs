using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFuntions : Interactable {
	public PlayerController owner;
	public GameObject FloatingTextPrefab;
	public GameObject ChickenPrefab;
	public GameObject RabbitPrefab;
	public GameObject PigPrefab;
	public GameObject GoatPrefab;
	public GameObject CowPrefab;
	public GameObject HorsePrefab;
	private GameObject FloatingTextInstance;
	private float PriceFloat;
	private int PriceInt;
	private bool PrivIsSelling = false;
	private GameObject SellingItem;
	private PlayerController SellingPlayer;

	// Use this for initialization
	void Start () {
		CreateFloatingText();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePrice();
		UpdateFloatingText();
		if(!PrivIsSelling && (owner == null)){
			if (Random.Range(0f, 2f) < Time.deltaTime){
				SpawnItem();
			}
		}
	}

	void SpawnItem(){
		float randnr = Random.Range(0f, 6.0f);
		if (randnr < 1){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 2){
			SellingItem = Instantiate(RabbitPrefab, transform.position, Quaternion.identity, transform);	
		} else if (randnr < 3){
			SellingItem = Instantiate(PigPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 4){
			SellingItem = Instantiate(GoatPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 5){
			SellingItem = Instantiate(CowPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 6){
			SellingItem = Instantiate(HorsePrefab, transform.position, Quaternion.identity, transform);
		} 
		Give(null, SellingItem);
	}

	public bool IsSelling(){
		return PrivIsSelling;
	}
	
	public override bool Give(PlayerController SellingPlayer, GameObject SellingItem) {
		if (!PrivIsSelling && (owner == SellingPlayer)){
			this.SellingItem = SellingItem;
			this.SellingPlayer = SellingPlayer;
			PrivIsSelling = true;
			PriceFloat = SellingItem.GetComponent<AnimalStats>().Price;
			placeItem(this.SellingItem);
			return true;
		}

		return false;
	}

	public override GameObject Take(PlayerController BuyingPlayer) {
		if (PrivIsSelling){
			if (owner != BuyingPlayer){
				if (BuyingPlayer.Balance < PriceInt){
					return null;	
				}
				BuyingPlayer.Balance -= PriceInt;
				if (owner != null){
					SellingPlayer.Balance += PriceInt;
				}
			}
			PrivIsSelling = false;
			GameObject soldItem = SellingItem;
			SellingItem = null;
			return soldItem;
		}

		return null;
	}
	void CreateFloatingText(){
		FloatingTextInstance = Instantiate(FloatingTextPrefab, transform.position + new Vector3(0,0.5f,0), Quaternion.identity, transform);
	}

	private void OnDestroy(){
		Destroy(FloatingTextInstance);
	}

	void UpdateFloatingText(){
		if (PrivIsSelling){
			FloatingTextInstance.GetComponent<TextMesh>().text = PriceInt.ToString();
		} else {
			FloatingTextInstance.GetComponent<TextMesh>().text = "Sold";
		}	
	}

	void UpdatePrice(){
		PriceFloat *= Mathf.Exp(-0.05f * Time.deltaTime);
		PriceInt = (int)PriceFloat;
		if (PriceInt < 20)
		{
			Destroy(SellingItem);
			SellingItem = null;
			PrivIsSelling = false;
		}
	}
}
