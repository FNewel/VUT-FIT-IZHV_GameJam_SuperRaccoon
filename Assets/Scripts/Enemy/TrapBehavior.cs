using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject gameManager;

    public Animator animator;


    void Start(){
        gameManager = GameObject.Find("GameManager");
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            gameManager.GetComponent<GameManager>().GetDamage();
            gameObject.tag = "Corpse";
            gameObject.layer = 9;
            animator.SetBool("Snapped", true);
            this.GetComponent<AudioSource>().Play();
        }
    }
}
