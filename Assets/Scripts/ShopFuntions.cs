using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFuntions : MonoBehaviour {

	public GameObject FloatingTextPrefab;
	private GameObject FloatingTextInstance;
	private float PriceFloat;
	private int PriceInt;
	private bool PrivIsSelling = false;
	private GameObject SellingItem;
	private GameObject SellingPlayer;

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

	public void Sell(GameObject SellingItem, GameObject SellingPlayer) {
		if (!PrivIsSelling){
			this.SellingItem = SellingItem;
			this.SellingPlayer = SellingPlayer;
			PrivIsSelling = true;
			PriceFloat = SellingItem.GetComponent<AnimalStats>().Price;
		}
	}

	public void Buy(GameObject BuyingPlayer) {
		if (PrivIsSelling){
			if (BuyingPlayer.GetComponent<PlayerController>().Balance < PriceInt){
				return;	
			}
			BuyingPlayer.GetComponent<PlayerController>().Balance -= PriceInt;
			SellingPlayer.GetComponent<PlayerController>().Balance += PriceInt;
			PrivIsSelling = false;
		}
	}
	void CreateFloatingText(){
		FloatingTextInstance = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
	}

	void DestroyFloatingText(){
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
