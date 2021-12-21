using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    
    private GameObject gameManager;
    public bool lastLevel;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("EOL");
            if (lastLevel)
            {
                gameManager.GetComponent<GameManager>().GameWon();
            }
            else
            {
                gameManager.GetComponent<GameManager>().StartNextLevel();
            }
        }
    }
}
