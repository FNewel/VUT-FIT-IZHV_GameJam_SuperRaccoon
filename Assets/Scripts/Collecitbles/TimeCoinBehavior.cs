using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCoinBehavior : MonoBehaviour
{

    private GameObject gameManager;

    private bool collected;


    void Start(){
        gameManager = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !collected){
            collected = true;
            Debug.Log("TimeAdd");
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().CollectTime();
        }
    }

}
