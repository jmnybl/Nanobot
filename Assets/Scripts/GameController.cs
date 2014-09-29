﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public GUITexture texture;
	public float fadeSpeed = 0.5f;

	private Texture2D heavy_icon;
	private Texture2D light_icon;
	private Texture2D magnet_icon;

	private bool isFading = false;
	private bool sceneStarting = false;
	
	private bool showEndGameText = false;
	private bool showLevelCompleteText = false;
	private string endGameString = "Game Over";
	private string levelCompleteString = "Level Complete";
	private GUIStyle style = new GUIStyle();
	private GUIStyle textstyle = new GUIStyle();
	
	private bool isPause = false;
	private Rect windowRect = new Rect(Screen.width/2-200, Screen.height/2-100, 400, 200);

	public string activePlayer = "";
	private string tempactivePlayer = "";
	private float timer = 1.0f;
	
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
		texture.color = Color.black;
		style.fontSize = 20;
		textstyle.fontSize = 30;
		LevelController.gamePaused = false;
		PlayerController.isPaused = false;
		// disable screen dimming (because accelerometer does not do that)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//tempactivePlayer = activePlayer;
		StartScene();
		
		// load player icons
		heavy_icon = (Texture2D)Resources.Load("heavy_icon");
		light_icon = (Texture2D)Resources.Load("light_icon");
		magnet_icon = (Texture2D)Resources.Load("magnet_icon");
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

		if(tempactivePlayer != activePlayer)
		{
			timer = 1f;
			tempactivePlayer = activePlayer;
		}
		if(timer > 0.1f)
		{
			timer = timer - 0.1f;
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
		
		// show active player pictures
		if (activePlayer == "heavycircle")
		{
			GUI.DrawTexture (new Rect (Screen.width/2-50, 30, 100, 100), magnet_icon);
			GUI.DrawTexture (new Rect (Screen.width-150, 30, 100, 100), light_icon);
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, timer);
			GUI.DrawTexture (new Rect (50, 30, 100, 100), heavy_icon);
		}
		else if (activePlayer == "lightcircle")
		{
			GUI.DrawTexture (new Rect (50, 30, 100, 100), heavy_icon);
			GUI.DrawTexture (new Rect (Screen.width/2-50, 30, 100, 100), magnet_icon);
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, timer);
			GUI.DrawTexture (new Rect (Screen.width-150, 30, 100, 100), light_icon);
		}
		else
		{
			GUI.DrawTexture (new Rect (50, 30, 100, 100), heavy_icon); 
			GUI.DrawTexture (new Rect (Screen.width-150, 30, 100, 100), light_icon);
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, timer);
			GUI.DrawTexture (new Rect (Screen.width/2-50, 30, 100, 100), magnet_icon);
		}
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
		if(GUILayout.Button("Main Menu",GUILayout.Height(windowRect.height/5)))
		{
			Application.LoadLevel("MainMenu");
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Level Menu",GUILayout.Height(windowRect.height/5)))
		{
			Application.LoadLevel("LevelMenu");
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Restart level",GUILayout.Height(windowRect.height/5)))
		{
			Application.LoadLevel(Application.loadedLevelName);
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Quit",GUILayout.Height(windowRect.height/5)))
		{
			Application.Quit();
		}
	}
}
