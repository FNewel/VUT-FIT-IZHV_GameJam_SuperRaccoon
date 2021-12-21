using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashLidBehavior : MonoBehaviour
{


    //TODO remove late
    private string collectibleName;

    public GameObject collectible;

    void Start(){
        collectibleName = collectible.GetComponent<SpriteRenderer>().sprite.name;
        if(PlayerPrefs.GetInt(collectibleName) > 0){
            StartCoroutine(OpenLid(0.05f));
            Destroy(collectible);
        }
    }


    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("Open");
            StartCoroutine(OpenLid(0.05f));
        }
    }

    IEnumerator OpenLid(float secs){
        yield return new WaitForSeconds(secs);
        GetComponent<Rigidbody2D>().AddForce(new Vector3(2,20,0)*10f);
        gameObject.tag = "Corpse";
        gameObject.layer = 9;
    }

}
