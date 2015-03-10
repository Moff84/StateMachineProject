using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public bool gameOver = false;
	public Text money, ppl, paused;
	enum GameState{ // a state machine that will switch between the game and the menus
		mainMenu,
		game,
		gameOver,
		paused
	}

	GameState currentState = GameState.mainMenu;

	void Update(){
		Debug.Log (currentState);
		switch (currentState) {
		case GameState.mainMenu:
			if(Input.GetKeyDown(KeyCode.Return)){
				Time.timeScale = 1;
				currentState = GameState.game;
			}
			break;

		case GameState.game:
			paused.text = "";
			Zoom();
			if(Input.GetKeyDown(KeyCode.Escape)){
				Time.timeScale = 0;
				currentState = GameState.paused;
			}
			if(gameOver){
				Time.timeScale = 0;
				currentState = GameState.gameOver;
			}
			break;

		case GameState.paused:
			paused.text = "Paused";
			if(Input.GetKeyDown(KeyCode.Escape)){
				Time.timeScale = 1;
				currentState = GameState.game;
			}
			break;


		case GameState.gameOver:
			Camera.main.fieldOfView = 60;
			if(Input.GetKeyDown(KeyCode.Return)){
				ResetGame();
				currentState = GameState.mainMenu;
			}
			break;

		}
	}

	void Zoom(){
		if (Input.GetMouseButton (1)) {
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 40, Time.deltaTime);
		} else
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 60, Time.deltaTime);
	}

	void ResetGame(){
		gameOver = false;
	}
}
