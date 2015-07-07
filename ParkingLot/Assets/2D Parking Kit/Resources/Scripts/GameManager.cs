using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public int LevelNumber;
	public GameObject imgStar1;
	public GameObject imgStar2;
	public GameObject imgStar3;
	public AudioClip audPickup;
	public AudioClip audWin;
	public AudioClip audCrash;
	//Panels
	public GameObject PausePanel;
	public GameObject PlayingPanel;
	public GameObject WinPanel;
	public GameObject LosePanel;
	public GameObject SettingsPanel;
	public Text txtGraphics;
	public Text MuteText;

	GameObject player;

	int StarsCollected;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		Time.timeScale = 1;
		StarsCollected = 0;


		//Gets whether the sound is set to muted, and changes audio settings accordingly.
		if (PlayerPrefs.GetInt("SoundSettings") == 1)
		{
			AudioListener.pause = false;
			MuteText.text = "";
		} else if (PlayerPrefs.GetInt ("SoundSettings") == 0) 
		{
			AudioListener.pause = true;
			MuteText.text = "/";
		}

		txtGraphics.text = GetQualityName (QualitySettings.GetQualityLevel());
	}



public	void CollectStar ()
	{
		GetComponent<AudioSource>().PlayOneShot (audPickup);
		StarsCollected += 1;

		if (StarsCollected == 1)
			imgStar1.SetActive (true);
		if (StarsCollected == 2)
			imgStar2.SetActive (true);
		if (StarsCollected == 3)
			imgStar3.SetActive (true);

	}

	public void CollectTime()
	{
		GetComponent<AudioSource>().PlayOneShot (audPickup);
	}


public void LevelWin ()
	{
		int lvlULTemp = PlayerPrefs.GetInt("LevelsUnlocked") + 1;
		PlayerPrefs.SetInt("LevelsUnlocked",lvlULTemp);

		Time.timeScale = 0;
		int tempStars = PlayerPrefs.GetInt ("Level" + LevelNumber.ToString () + "Stars");
		GetComponent<AudioSource>().PlayOneShot (audWin);
		if (tempStars < StarsCollected) 
		{
			PlayerPrefs.SetInt ("Level" + LevelNumber.ToString () + "Stars", StarsCollected);
		}

		PausePanel.SetActive (false);
		PlayingPanel.SetActive (false);
		WinPanel.SetActive (true);
		LosePanel.SetActive (false);
		SettingsPanel.SetActive (false);

		}


public	void LevelLose()
	{
		player.GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().PlayOneShot (audCrash);
		PausePanel.SetActive (false);
		PlayingPanel.SetActive (false);
		WinPanel.SetActive (false);
		LosePanel.SetActive (true);
		SettingsPanel.SetActive (false);
		Time.timeScale = 0;
		Debug.Log ("Level Lose");
	}

	/////////////////////////////
	////////////////////////////
	///////UI FUNCTIONS////////
	//////////////////////////


	public void PreviousLevel()
	{

		Application.LoadLevel (Application.loadedLevel - 1);
	}

	public void NextLevel()
	{

		if(Application.loadedLevel <10)
		{
		Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Application.LoadLevel (0);
		}
	}

	public void Restart()
	{

		Application.LoadLevel (Application.loadedLevel);
	}

	public void SwitchToMenu()
	{
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}

 	public void Pause()
	{
	PausePanel.SetActive (true);
	PlayingPanel.SetActive (false);
	WinPanel.SetActive (false);
	LosePanel.SetActive (false);
	SettingsPanel.SetActive (false);
	Time.timeScale = 0;

	}

	public void Unpause()
	{
		Time.timeScale = 1;
		PausePanel.SetActive (false);
		PlayingPanel.SetActive (true);
		WinPanel.SetActive (false);
		LosePanel.SetActive (false);
		SettingsPanel.SetActive (false);
	}

	public void Settings()
	{
		PausePanel.SetActive (false);
		PlayingPanel.SetActive (false);
		WinPanel.SetActive (false);
		LosePanel.SetActive (false);
		SettingsPanel.SetActive (true);
	}

	public void ChangeGraphics(string GraphicsSet)
	{
		if(GraphicsSet == "up")
		{ 
			QualitySettings.IncreaseLevel();
		}else if(GraphicsSet == "down") 
		{
			QualitySettings.DecreaseLevel();
		}
		txtGraphics.text = GetQualityName (QualitySettings.GetQualityLevel());
	}


	public void ToggleSound()
	{
		int tempSound = PlayerPrefs.GetInt ("SoundSettings");
		
		if (tempSound == 1) {
			PlayerPrefs.SetInt("SoundSettings",0);
			//audio.mute = true;
			AudioListener.pause = true;
			MuteText.text = "/";
		} else if (tempSound == 0) {
			PlayerPrefs.SetInt("SoundSettings",1);
			//audio.mute = false;
			MuteText.text = "";
			AudioListener.pause = false;
		}
	}

	
	public string GetQualityName(int qualityNo)
	{
		string[] names;
		names = QualitySettings.names;
		
		return names[qualityNo];
	}
}






















