using UnityEngine;
using System.Collections;

public class Player : GameManager {
	Renderer thisRenderer;
	NavMeshAgent myNavAgent;
	public float hunger;
	public int timesEaten =-1;
	Vector3 newDestination;
	Animator myAnimiator;
	enum AI{
		idle,
		hungry,
		walkingWithFood,
		bored
	}
	AI ai = AI.idle;
	void Start(){
		myAnimiator = GetComponent<Animator> ();
		myNavAgent = GetComponent<NavMeshAgent> ();
		hunger = 5;
		thisRenderer = GetComponent<Renderer> ();
	}
	void OnEnable(){
		SetupAI ();
		hunger = Random.Range (minHunger, maxHunger);
	}

	void Update(){
		switch (ai) {
		case AI.idle:
			myAnimiator.SetBool("walking",false);
			myAnimiator.SetBool("walkingWithFood",false);
			thisRenderer.material.color = Color.blue;
			hunger-=Time.deltaTime;
			if(hunger <0&&timesEaten<4){
				timesEaten++;
				ai = AI.hungry;
			}else if(timesEaten>=4){
				timesEaten++;
				ai = AI.bored;
			}
			break;
		case AI.bored:
			myNavAgent.destination = GameObject.Find("Exit").transform.position;
			thisRenderer.material.color = Color.white;
			if(myNavAgent.remainingDistance<myNavAgent.stoppingDistance){
				gameObject.SetActive(false);
			}
			break;
		case AI.walkingWithFood:
			myAnimiator.SetBool("walkingWithFood",true);
			myAnimiator.SetBool("walking",false);
			thisRenderer.material.color = Color.yellow;
			myNavAgent.destination = newDestination;
			if(myNavAgent.remainingDistance < myNavAgent.stoppingDistance){
				ai = AI.idle;
			}
			break;
		case AI.hungry:
			thisRenderer.material.color = Color.red;
			myAnimiator.SetBool("walking",true);
			GameObject foodShop = GameObject.Find("FoodStand");
			if(foodShop)
			myNavAgent.destination = foodShop.transform.position;
			else
				Debug.Log("No Food Shop Found");
			if(hunger >=maxHunger){
				newDestination = new Vector3(Random.Range(-100,100),1.0833f,Random.Range(-100,100));
				ai = AI.walkingWithFood;
			}
			break;
		}
	}
	public void SetupAI(){
		minHunger = 2.5f;
		maxHunger = 5;
	}

}
