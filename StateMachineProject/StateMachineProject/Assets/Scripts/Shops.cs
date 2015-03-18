using UnityEngine;
using System.Collections;

public class Shops : MonoBehaviour {

	void Start(){
		gameObject.name = "FoodStand";
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "AI") {
			Player aiScript = col.gameObject.GetComponent<Player>();
			aiScript.hunger= aiScript.maxHunger;
		}
	}
}
