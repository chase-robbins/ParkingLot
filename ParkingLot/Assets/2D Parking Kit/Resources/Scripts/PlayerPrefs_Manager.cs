using UnityEngine;
using System.Collections;

public class PlayerPrefs_Manager : MonoBehaviour {
	public int Number_of_Levels = 3;

	// Use this for initialization
	void Start () {

		Debug.Log(PlayerPrefs.GetInt ("LevelsUnlocked"));

		//Adding the sound settings key
		if (!PlayerPrefs.HasKey ("SoundSettings")) 
		{
			PlayerPrefs.SetInt ("SoundSettings", 1);
		}

		//Adding the key that holds the amount of levels the user has unlocked.
		if(!PlayerPrefs.HasKey ("LevelsUnlocked"))
		{
			PlayerPrefs.SetInt ("LevelsUnlocked", 1);
		}


		//Adding the key that holds what game mode the user 
		//has selected. 

		if(!PlayerPrefs.HasKey ("GameMode"))
		{
			PlayerPrefs.SetString("GameMode","Timed");	
		}

		CreateStars ();

	}


	void CreateStars()
	{

		for(int i = 0; i < Number_of_Levels; i++)
		{
			if(!PlayerPrefs.HasKey("Level" + i.ToString() + "Stars"))
			{
				PlayerPrefs.SetInt("Level" + i.ToString() + "Stars",0);
			}

		}



	}
}
