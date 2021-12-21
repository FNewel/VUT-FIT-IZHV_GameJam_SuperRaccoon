using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //[Header("Game length")]
    [HideInInspector] public float time;
    [HideInInspector] public float timeLeft;
    [Header("UI Elements")]
    public TMPro.TextMeshProUGUI timeText;
    private string text;
    public Canvas inGameC;
    public Canvas gameFailedC;
    public Canvas pauseMenuC;
    public Canvas getDamageC;
    public Canvas collectTimeC;
    public Canvas collectibleFoundC;
    public Canvas gameWonC;
    public TMPro.TextMeshProUGUI wonTimeText;
    public GameObject eventSystem;
    [Header("Sound Elements")]
    public AudioMixer masterMixer;
    public AudioSource gunShot;
    [Header("Values")]
    public float damage = 5f;
    public float addTime = 10f;
    public float lowTimeLimit = 15f;
    public string nextLevel;

    private bool ended = false;
    private float waitTime = 1.25f;
    [HideInInspector] public bool freezeTime = false;

    void Awake()
    {
        time = PlayerPrefs.GetFloat("timeleft");
        timeLeft = time;
    }
    private void Start()
    {
        masterMixer.SetFloat("Volume", PlayerPrefs.GetFloat("volumeLevel"));
        gameFailedC.GetComponent<Animation>().PlayQueued("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (!freezeTime)
        {
            timeLeft -= 1 * Time.deltaTime;
        }

        if((timeLeft <= lowTimeLimit) && (!inGameC.GetComponent<Animation>().isPlaying) && (!ended))
        {
            inGameC.GetComponent<Animation>().PlayQueued("LowTimeLoop");
        }

        if (timeLeft > 0)
        {
            text = Mathf.FloorToInt(timeLeft / 60).ToString("D2") + ":" + Mathf.FloorToInt(timeLeft % 60).ToString("D2");
            timeText.text = text;
        }
        else if(!ended)
        {
            timeText.text = "00:00";
            EndGame();
        }

        if (ended)
        {
            waitTime -= 1 * Time.deltaTime;
            if ((Input.anyKeyDown) && (waitTime <= 0))
            {
                ExitGame();
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape)) && (!ended))
        {
            PauseGame();
        }
    }
    void EndGame()
    {
        gunShot.Play();
        inGameC.GetComponent<Animation>().Stop();
        inGameC.GetComponent<Animation>().PlayQueued("InGameFadeOut");
        gameFailedC.GetComponent<Animation>().PlayQueued("FailedGameFadeIn");
        gameFailedC.GetComponent<Animation>().PlayQueued("ContinueTextLoop");
        Time.timeScale = 0.5f;
        ended = true;
    }
    public void ContinueGame()
    {
        pauseMenuC.gameObject.SetActive(false);
        eventSystem.SetActive(false);
        Time.timeScale = 1;
    }
    void PauseGame()
    {
        pauseMenuC.gameObject.SetActive(true);
        eventSystem.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        pauseMenuC.gameObject.SetActive(false);
        eventSystem.SetActive(false);
        SceneManager.LoadScene("MainMenu_Scene", LoadSceneMode.Single);
    }
    public void GetDamage()
    {
        if (!ended)
        {
            timeLeft -= damage;
            getDamageC.GetComponent<Animation>().Play("GetHit");
        }
    }
    public void CollectTime()
    {
        if (!ended)
        {
            timeLeft += addTime;
            collectTimeC.GetComponent<Animation>().Play("CollectTime");
        }
    }
    public void CollectibleFound()
    {
        if (!ended)
        {
            collectibleFoundC.GetComponent<Animation>().Play("CollectibleFound");
        }
    }
    public void StartNextLevel()
    {
        PlayerPrefs.SetFloat("timeleft", timeLeft);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
    public void GameWon()
    {
        int gamesWon = PlayerPrefs.GetInt("gamesWon");
        gamesWon++;
        PlayerPrefs.SetInt("gamesWon", gamesWon);
        wonTimeText.text = Mathf.FloorToInt(timeLeft / 60).ToString("D2") + ":" + Mathf.FloorToInt(timeLeft % 60).ToString("D2") + " UNTIL SUNRISE";

        float bestTime = PlayerPrefs.GetFloat("bestTime");
        if(timeLeft > bestTime)
        {
            PlayerPrefs.SetFloat("bestTime", timeLeft);
        }

        inGameC.GetComponent<Animation>().Stop();
        inGameC.GetComponent<Animation>().PlayQueued("InGameFadeOut");
        gameWonC.GetComponent<Animation>().PlayQueued("FailedGameFadeIn");
        gameWonC.GetComponent<Animation>().PlayQueued("ContinueTextLoop");
        Time.timeScale = 0.5f;
        ended = true;
    }
}
