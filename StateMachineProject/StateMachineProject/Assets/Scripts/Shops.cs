using UnityEngine;
using System.Collections;

public class Shops : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "AI") {
			Player aiScript = col.gameObject.GetComponent<Player>();
			aiScript.hunger= aiScript.maxHunger;
		}
	}
}
