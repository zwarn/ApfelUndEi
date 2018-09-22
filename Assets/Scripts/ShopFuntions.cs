using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFuntions : Interactable {

	public GameObject FloatingTextPrefab;
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
	}

	public bool IsSelling(){
		return PrivIsSelling;
	}
	
	public override void Give(PlayerController SellingPlayer, GameObject SellingItem) {
		Debug.Log("sell");
		if (!PrivIsSelling){
			this.SellingItem = SellingItem;
			this.SellingPlayer = SellingPlayer;
			PrivIsSelling = true;
			PriceFloat = SellingItem.GetComponent<AnimalStats>().Price;
			placeItem(this.SellingItem);
		}
	}

	public override GameObject Take(PlayerController BuyingPlayer) {
		if (PrivIsSelling){
			if (BuyingPlayer.Balance < PriceInt){
				return null;	
			}
			BuyingPlayer.Balance -= PriceInt;
			SellingPlayer.Balance += PriceInt;
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
