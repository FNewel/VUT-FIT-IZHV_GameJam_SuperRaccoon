using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    [Header("Canvas list")]
    public Canvas mainMenuC;
    public Canvas statsC;
    public Canvas settingsC;
    public Canvas creditsC;
    [Header("Objects")]
    public GameObject eventSystem;
    public AudioMixer masterSound;
    public Image sliderBase;
    private float soundLevel;
    private Slider soundSlider;
    public Image collectiblesPanel;
    public Image tutorialPanel;
    [Header("Time")]
    public float time = 900f;

    private int gamesPlayed;
    private int timesClicked;

    private void Awake()
    {
        Time.timeScale = 1f;
        soundLevel = PlayerPrefs.GetFloat("volumeLevel");
        soundSlider = sliderBase.GetComponent<Slider>();
        soundSlider.minValue = -80;
        soundSlider.maxValue = 20;
        soundSlider.value = soundLevel;

        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        timesClicked = 0;
    }
    private void Start()
    {
        masterSound.SetFloat("Volume", soundLevel);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
            eventSystem.gameObject.SetActive(false);
            eventSystem.gameObject.SetActive(true);
        }

    }
    public void PlayGame()
    {
        gamesPlayed++;
        PlayerPrefs.SetFloat("timeleft", time);
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    public void EnterStats()
    {
        mainMenuC.gameObject.SetActive(false);
        statsC.gameObject.SetActive(true);
    }

    public void EnterSettings()
    {
        mainMenuC.gameObject.SetActive(false);
        settingsC.gameObject.SetActive(true);
    }
    public void EnterCredits()
    {
        mainMenuC.gameObject.SetActive(false);
        creditsC.gameObject.SetActive(true);
    }
    public void ReturnToMenu()
    {
        mainMenuC.gameObject.SetActive(true);
        statsC.gameObject.SetActive(false);
        settingsC.gameObject.SetActive(false);
        creditsC.gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit button pressed");
    }
    public void AddSound()
    {
        if (soundLevel < soundSlider.maxValue)
        {
            soundLevel += 5;
            PlayerPrefs.SetFloat("volumeLevel", soundLevel);
            masterSound.SetFloat("Volume", soundLevel);
            soundSlider.value = soundLevel;
        }
    }
    public void SubSound()
    {
        if (soundLevel > soundSlider.minValue)
        {
            soundLevel -= 5;
            PlayerPrefs.SetFloat("volumeLevel", soundLevel);
            masterSound.SetFloat("Volume", soundLevel);
            soundSlider.value = soundLevel;
        }
    }
    public void EraseProgress()
    {
        if(timesClicked == 0)
        {
            PlayerPrefs.SetInt("gamesPlayed", 0);
            PlayerPrefs.SetInt("gamesWon", 0);
            PlayerPrefs.SetFloat("bestTime", 0f);
            SetCollectiblesValue(0);
            timesClicked++;
            gamesPlayed = 0;
            this.gameObject.GetComponent<StatsScreen>().setAllStats();
        }
        else if(timesClicked == 2)
        {
            SetCollectiblesValue(1);
            timesClicked = 0;
            this.gameObject.GetComponent<StatsScreen>().setAllStats();
        }
        else
        {
            timesClicked++;
        }
    }
    void SetCollectiblesValue(int value)
    {
        PlayerPrefs.SetInt("btc", value);
        PlayerPrefs.SetInt("cake", value);
        PlayerPrefs.SetInt("crowbar", value);
        PlayerPrefs.SetInt("halo", value);
        PlayerPrefs.SetInt("hammer", value);
        PlayerPrefs.SetInt("lightsaber", value);
        PlayerPrefs.SetInt("mushroom", value);
        PlayerPrefs.SetInt("pickaxe", value);
        PlayerPrefs.SetInt("pizza", value);
        PlayerPrefs.SetInt("ring", value);
        PlayerPrefs.SetInt("sweetroll", value);
        PlayerPrefs.SetInt("tetris", value);
    }
    public void Tutorial()
    {
        if (tutorialPanel.gameObject.activeSelf)
        {
            tutorialPanel.gameObject.SetActive(false);
        }
        else
        {
            tutorialPanel.gameObject.SetActive(true);
        }
    }
    public void ShowCollectibles()
    {
        if (collectiblesPanel.gameObject.activeSelf)
        {
            collectiblesPanel.gameObject.SetActive(false);
        }
        else
        {
            collectiblesPanel.gameObject.SetActive(true);
        }
    }
    public void EraseTimesClicked()
    {
        timesClicked = 0;
    }
}
