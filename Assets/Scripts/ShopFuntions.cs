using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFuntions : MonoBehaviour {

	public GameObject FloatingTextPrefab;
	private GameObject FloatingTextInstance;
	private float PriceFloat;
	private float PriceInt;
	private bool IsSelling = false;
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

	void Sell(GameObject SellingItem, GameObject SellingPlayer) {
		if (!IsSelling){
			this.SellingItem = SellingItem;
			this.SellingPlayer = SellingPlayer;
			IsSelling = true;
			PriceFloat = SellingItem.Price();
		}
	}

	void Buy(GameObject BuyingPlayer) {
		if (IsSelling){
			if (BuyingPlayer.Balance < PriceInt){
				return;	
			}
			BuyingPlayer.Balance -= PriceInt;
			SellingPlayer.Balance += PriceInt;
			DestroyFloatingText();
		}
	}
	void CreateFloatingText(){
		FloatingTextInstance = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
	}

	void DestroyFloatingText(){
		Destroy(FloatingTextInstance);
	}

	void UpdateFloatingText(){
		if (IsSelling){
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
