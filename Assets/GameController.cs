using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// GameController Instance
	public static GameController instance;

    #region Box Variables

	// Stores the Current and Previous Box Spawned
	public GameObject CurrentBox;

	// Stores the Current Box AI Script
	public BoxAI CurrentBoxScript;

	// Stores the Box Spawner Script
	public BoxSpawn CurrentBoxSpawnScript;

	// Stores the Spawn Speed of the Box
	public float BoxSpawnSpeed = 1.5f;

    #endregion

	#region Score and Count Variables

	// Stores the Number of Box Spawned
	public int BoxCount;

	// Stores the Player Score
	public int ScoreCount = 0;
	public int HighscoreCount;

	// Score to Award Player
	public int BoxStackScore = 10;

	#endregion

	#region Camera Variables

	// Stores the Camera Follow Script
	public CameraFollow CurrentCameraFollowScript;

	// Stores the Camera Move Count
	public int CameraMoveCount;

	#endregion

	#region UI Elements

	// Scoring UI Elements
	public Text ScoreUI;
	public Text HighscoreUI;

	#endregion

    #region Cloud Variables

    // Cloud Controller
    public static float MinCloudSpeed = 0.5f;
    public static float MaxCloudSpeed = 0.8f;

	#endregion

	#region Game State

	// Bool to Set Game Status
	public bool GameStarted = false;
	public bool GamePaused = false;
	public bool GameOver = false;

	// GameObject holds the Screen Status Panel
	public GameObject GameStartScreen, GamePlayScreen, GamePausedScreen, GameOverScreen;


	#endregion

	// Awake is used to initialise all GameObjects
	void Awake()
	{
		// Set Current Instance
		if(instance == null)
		{ instance = this; }
	}

    // Use this for initialization
    void Start ()
    {
		// Set UI Panel Status
		GameStartScreen.SetActive(true);
		GamePlayScreen.SetActive(false); 
		GamePausedScreen.SetActive(false); 
		GameOverScreen.SetActive(false);

		// Get Highscore from Player Prefs
		HighscoreCount = PlayerPrefs.GetInt("Highscore", HighscoreCount);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Methotd to Control UI Switching
		PanelSwitching();

		// Method to Detect User Input
		DetectInput();	

		// Display Scoring in UI Element
		ScoreUI.text = ScoreCount.ToString();
		HighscoreUI.text = HighscoreCount.ToString();
	}

	// ************************************************************************ UI ELEMENTS

	// Method to Control UI Panels Switching
	void PanelSwitching()
	{
        #region

        // Check if Game has Started
        if (GameStarted == true)
		{
			// Play Game
			Time.timeScale = 1.0f;

			// Hide Start Screen and Show Game Screen
			GameStartScreen.SetActive(false);
			GamePlayScreen.SetActive(true);
		}

		// Check if Game Started and Games has not been Paused
		if (GameStarted == true && GamePaused == false)
		{
			// Play Game
			Time.timeScale = 1.0f;

			// Show and Hide Screen Panels
			GamePlayScreen.SetActive(true);
			GamePausedScreen.SetActive(false);
		}

		// Check if Game Started and Game has been Paused
		if (GameStarted == true && GamePaused == true)
		{
			// Pause Game
			Time.timeScale = 0.0f;

			// Show and Hide Screen Panels
			GamePlayScreen.SetActive(false);
			GamePausedScreen.SetActive(true);
		}

		// Check if Game Over
		if (GameOver == true)
		{
			// Pause Game
			Time.timeScale = 0.0f;

			// Hide All Screen Except GameOver Screen
			GameStartScreen.SetActive(false);
			GamePlayScreen.SetActive(false);
			GamePausedScreen.SetActive(false);
			GameOverScreen.SetActive(true);
		}

        #endregion
    }


    // Method to Award Player Score
    public void AwardScore()
	{
		// Increase the Current Score by the BoxStackScore
		ScoreCount+= BoxStackScore;

		// Check if Score if higher than previous HighScore
		if(ScoreCount > HighscoreCount)
		{
			// Set Current Score as High Score
			HighscoreCount = ScoreCount;

			// Update HighScore in Player Prefs
			PlayerPrefs.SetInt("Highscore", HighscoreCount);
		}

	}

	// ************************************************************************ INPUT DETECTION
	// Method to Detect Touch Input
	void DetectInput()
	{
		// Get Mouse Button Down
		if(Input.GetMouseButtonDown(0) && GameStarted == true && GamePaused == false)   
		{
			// Execute Falling Box Script from BoxAI Instance
			CurrentBoxScript.FallingBox();
		}
	}





	// Method to execute new box spawn at given time
	public void SpawnNewBox()
	{
		Invoke("NewBoxSpawn", BoxSpawnSpeed);
	}

	void NewBoxSpawn()
	{
		// Spawn a New Box from Box Spawn
		CurrentBoxSpawnScript.SpawnBox();
	}
		
	// Method that is called to increase the Box Spawn Count
	public void BoxCounter()
	{
		// Increase Box Count
		BoxCount++;
	}
		
	// Method to Move the Camera
	public void MoveCamera()
	{
		// Increase Camera Move Count
		CameraMoveCount++;

		// Check Move Counter
		if(CameraMoveCount == 3)
		{
			// Reset Move Count
			CameraMoveCount = 0;

			// Move Camera Upwards
			CurrentCameraFollowScript.TargetPosition.y += 2f;
		}
	}










	// Method to Start Game
	public void StartGame()
	{
		// Set Game Started Status to True
		GameStarted = true;
		Debug.Log("Game Started Status set to True");

		// Spawn New Box
		SpawnNewBox();
	}

	// Method to Pause Game
	public void PauseGame()
	{
		// Check if Game has been Pause or Not
		if (GamePaused == true)
		{
			GamePaused = false;
		}
		else
		{
			GamePaused = true;
		}
	}

	// Method Set Status to Game Over
	public void SetGameOver()
	{
		// Set Game Status to GameOver
		GameOver = true;
	}


	// Method Set Status to Game Over
	public void QuitGame()
	{
		// Quit Game
		Application.Quit();
	}

	// Method to Restart Game
	public void RestartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene
		(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
		);
	}

	// Method to Purge Player Prefs
	public void PurgePrefs()
	{
		// Execute Function to Purge Player Preference Settings
		PlayerPrefs.DeleteAll();
		Debug.Log("Player Preferences Cleared!");
	}



}
