using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour
{
    [Header("Stats texts")]
    public TMPro.TextMeshProUGUI gamesPlayedText;
    public TMPro.TextMeshProUGUI gamesWonText;
    public TMPro.TextMeshProUGUI bestTimeText;
    public TMPro.TextMeshProUGUI collectiblesText;
    [Header("Collectibles")]
    public GameObject[] found;
    public GameObject[] notFound;

    private int gamesPlayed;
    private int gamesWon;
    private float bestTime;
    private int collectibles = 0;
    private int index = 0;
    void Awake()
    {
        setAllStats();
    }
    public void setAllStats()
    {
        collectibles = 0;
        index = 0;

        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        gamesPlayedText.text = "GAMES PLAYED: " + gamesPlayed.ToString("D3");

        gamesWon = PlayerPrefs.GetInt("gamesWon");
        gamesWonText.text = "GAMES WON: " + gamesWon.ToString("D3");

        bestTime = PlayerPrefs.GetFloat("bestTime");
        if (gamesWon != 0)
            bestTimeText.text = Mathf.FloorToInt(bestTime / 60).ToString("D2") + ":" + Mathf.FloorToInt(bestTime % 60).ToString("D2") + " UNTIL SUNRISE!";
        else
            bestTimeText.text = "NO GAME WON";

        CheckAllCollectibles();
        collectiblesText.text = collectibles.ToString("D2") + "/12";
    }
    void CheckCollectible(string name)
    {
        int cValue = PlayerPrefs.GetInt(name);
        if(cValue == 0)
        {
            notFound[index].SetActive(true);
            found[index].SetActive(false);
        }
        else
        {
            found[index].SetActive(true);
            notFound[index].SetActive(false);
            collectibles++;
        }
        index++;
    }
    void CheckAllCollectibles()
    {
        CheckCollectible("cake");
        CheckCollectible("btc");
        CheckCollectible("crowbar");
        CheckCollectible("halo");
        CheckCollectible("hammer");
        CheckCollectible("lightsaber");
        CheckCollectible("mushroom");
        CheckCollectible("pickaxe");
        CheckCollectible("pizza");
        CheckCollectible("ring");
        CheckCollectible("sweetroll");
        CheckCollectible("tetris");
    }
}
