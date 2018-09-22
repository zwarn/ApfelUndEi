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
		if (FloatingTextPrefab){
			CreateFloatingText();
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePrice();
		UpdateFloatingText();
		if(!PrivIsSelling && (owner == null)){
			if (Random.Range(0f, 10.0f) < Time.deltaTime){
				SpawnItem();
			}
		}
	}

	void SpawnItem(){
		float randnr = Random.Range(0f, 6.0f);
		if (randnr < 1){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 2){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);	
		} else if (randnr < 3){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 4){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 5){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} else if (randnr < 6){
			SellingItem = Instantiate(ChickenPrefab, transform.position, Quaternion.identity, transform);
		} 
		Give(null, SellingItem);
	}

	public bool IsSelling(){
		return PrivIsSelling;
	}
	
	public override void Give(PlayerController SellingPlayer, GameObject SellingItem) {
		if (!PrivIsSelling && (owner == SellingPlayer)){
			this.SellingItem = SellingItem;
			this.SellingPlayer = SellingPlayer;
			PrivIsSelling = true;
			PriceFloat = SellingItem.GetComponent<AnimalStats>().Price;
			placeItem(this.SellingItem);
		}
	}

	public override GameObject Take(PlayerController BuyingPlayer) {
		if (PrivIsSelling){
			if (owner == BuyingPlayer){
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
		FloatingTextInstance = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
	}

	private void OnDestroy(){
		Destroy(FloatingTextInstance);
	}

	void UpdateFloatingText(){
		if (PrivIsSelling){
			FloatingTextInstance.GetComponent<TextMesh>().text = PriceInt.ToString();
		} else {
			FloatingTextInstance.GetComponent<TextMesh>().text = "";
		}	
	}

	void UpdatePrice(){
		PriceFloat *= Mathf.Exp(-0.05f * Time.deltaTime);
		PriceInt = (int)PriceFloat;
	}
}
