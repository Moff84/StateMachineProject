using UnityEngine;
using System.Collections;

public class Player : GameManager {
	Renderer thisRenderer;
	NavMeshAgent myNavAgent;
	public float hunger;
	Vector3 newDestination;
	enum AI{
		idle,
		hungry,
		happy,
		bored
	}
	AI ai = AI.idle;
	void Start(){
		myNavAgent = GetComponent<NavMeshAgent> ();
		hunger = 5;
		thisRenderer = GetComponent<Renderer> ();
	}
	void OnEnable(){
		SetupAI ();
		hunger = Random.Range (minHunger, maxHunger);
	}

	void Update(){
		Debug.Log ("Current Position:"+ transform.position+"   Destination: "+newDestination);
		switch (ai) {
		case AI.idle:
			thisRenderer.material.color = Color.blue;
			hunger-=Time.deltaTime;
			if(hunger <0){
				ai = AI.hungry;
			}
			break;
		case AI.bored:
			thisRenderer.material.color = Color.red;
			break;
		case AI.happy:
			thisRenderer.material.color = Color.yellow;
			myNavAgent.destination = newDestination;
			if(myNavAgent.remainingDistance < myNavAgent.stoppingDistance){
				ai = AI.idle;
			}
			break;
		case AI.hungry:
			thisRenderer.material.color = Color.red;
			myNavAgent.destination = new Vector3(0,0,0);
			if(hunger >=maxHunger){
				newDestination = new Vector3(Random.Range(-10,10),1.0833f,Random.Range(-10,10));
				ai = AI.happy;
			}
			break;
		}
	}
	public void SetupAI(){
		minHunger = 2.5f;
		maxHunger = 5;
	}

}
