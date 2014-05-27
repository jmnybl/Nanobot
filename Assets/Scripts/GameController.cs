﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public GUITexture texture;
	public float fadeSpeed = 0.5f;

	private bool isFading = false;
	private bool sceneStarting = false;
	
	private bool showEndGameText = false;
	private bool showLevelCompleteText = false;
	private string endGameString = "Game Over";
	private string levelCompleteString = "Level Complete";
	private GUIStyle style = new GUIStyle();
	private GUIStyle textstyle = new GUIStyle();
	
	private bool isPause = false;
	private Rect windowRect = new Rect(Screen.width/2-100, Screen.height/2, 200, 100);

	public string activePlayer;
	
	void Start()
	{
		instance = this;
		texture.color = Color.black;
		style.fontSize = 20;
		textstyle.fontSize = 30;
		LevelController.gamePaused = false;
		PlayerController.isPaused = false;
		// disable screen dimming (because accelerometer does not do that)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		StartScene();
	}
	
	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;
			if(isPause)
			{
				Screen.sleepTimeout = 5; // let the screen dim to save power
				LevelController.gamePaused = true;
				PlayerController.isPaused = true;
				Time.timeScale = 0;
			}
			else
			{
				Screen.sleepTimeout = SleepTimeout.NeverSleep; // disable screen dimming again
				LevelController.gamePaused = false;
				PlayerController.isPaused = false;
				Time.timeScale = 1;
			}
		}
		
		if(isFading)
		{
			if(sceneStarting)
			{
				FadeToClear();
			}
			else
			{
				FadeToBlack();
			}
		}
	}
	
	void OnGUI()
	{
		if(showEndGameText)
		{
			style.normal.textColor = Color.red;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), endGameString, style);
		}
		if(showLevelCompleteText)
		{
			style.normal.textColor = Color.green;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), levelCompleteString, style);
		}
		if(isPause)
		{
			GUI.color = Color.blue;
			GUI.Window(0, windowRect, TheMainMenu, "");
		}


		// show active player picture, version A
		/**
		if (activePlayer == "heavycircle")
		{
			GUI.Label(new Rect((Screen.width/4)-30,30,50,50),"magnet",textstyle);
			//GUI.Label(new Rect(Screen.width/2,30,50,50),"heavy"); // active player
			GUI.Label(new Rect(((Screen.width/4)*3)-30,30,50,50),"light",textstyle);
		}
		else if (activePlayer == "lightcircle")
		{
			GUI.Label(new Rect((Screen.width/4)-30,30,250,250),"heavy",textstyle);
			//GUI.Label(new Rect(Screen.width/2,30,50,50),"light"); // active player
			GUI.Label(new Rect(((Screen.width/4)*3)-30,30,250,250),"magnet",textstyle);
		}
		else
		{
			GUI.Label(new Rect((Screen.width/4)-30,30,250,250),"light",textstyle);
			//GUI.Label(new Rect(Screen.width/2,30,50,50),"magnet"); // active player
			GUI.Label(new Rect(((Screen.width/4)*3)-30,30,250,250),"heavy",textstyle);
		}
		**/


		// show active player picture, version B
		GUI.Label(new Rect((Screen.width/4)-50,30,50,50),"heavy",textstyle);
		GUI.Label(new Rect((Screen.width/2)-50,30,50,50),"magnet", textstyle);
		GUI.Label(new Rect(((Screen.width/4)*3)-50,30,50,50),"light",textstyle);

	}
	
	private void FadeToBlack()
	{
		if(texture.color.a > 0.95)
		{
			texture.color = Color.black;
			isFading = false;
			Application.LoadLevel(0);
		}
		texture.color = Color.Lerp(texture.color, Color.black, Time.deltaTime * fadeSpeed);
	}

	private void FadeToClear()
	{
		if (texture.color.a < 0.05)
		{
			texture.color = Color.clear;
			isFading = false;
		}
		texture.color = Color.Lerp(texture.color, Color.clear, Time.deltaTime * fadeSpeed);
	}

	public void StartScene()
	{
		isFading = true;
		sceneStarting = true;
	}

	public void EndScene()
	{
		showEndGameText = false;
		showLevelCompleteText = false;
		isFading = true;
		sceneStarting = false;
	}
	
	public void EndLevel()
	{
		showLevelCompleteText = true;
	}
	
	public void EndGame()
	{
		showEndGameText = true;
	}
	
	void TheMainMenu(int windowID)
	{
		if(GUILayout.Button("Main Menu"))
		{
			Application.LoadLevel("MainMenu");
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Restart"))
		{
			Application.LoadLevel("Level1");
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Quit"))
		{
			Application.Quit();
		}
	}
}