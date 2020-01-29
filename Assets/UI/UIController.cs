using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	// General Effects Variables
	public float TextBlinkSpeed = 0.5f;

	// Stores the Tap to Start Text
	public Text StartText;

	// Stores the Score and Highscore Wrapper
	public GameObject ScoreWrapper;
	public GameObject HighscoreWrapper;


	// Awake is used to initialise all GameObjects
	void Awake()
	{
		// Check if Highscore should be displayed or hidden
		HighscoreController();
	}


	// Update is called once per frame
	void Update () 
	{
		// Method to Blink Start Text
		BlinkStartText();
	}


	// Method to Blink Tap to Start Text
	void BlinkStartText()
	{
        #region Use Ping Pong to Switch the Alpha Value
        StartText.GetComponent<Text>().color = new Color
		(
			255, //R
			255, //G
			255, //B
			Mathf.Round(Mathf.PingPong(Time.time * TextBlinkSpeed, 1.0f)) //A
		);
        #endregion
    }


    // Method that Hides or Show the Highscore
    void HighscoreController()
	{		
		#region Check for Previous Highscore 

		if(PlayerPrefs.GetInt("Highscore") == 0)
		{
			// Hide Highscore
			HighscoreWrapper.SetActive(false);
		}
		else
		{
			// Show Highscore
			HighscoreWrapper.SetActive(true);	
		}

		#endregion
	}
}
