using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{



    public GameObject trashLid;

    private Vector3 newPosition;

    private float moveSpeed = 0.5f;

    private Vector3 newScale;

    private bool isScaling = false;

    private GameObject player;

    private bool unpacked = false;

    public string collectibleName;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Position above the trashcan and scaled up
        newPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        newScale = new Vector3(1, 1, 1);
        //Find player (need his position later)
        player = GameObject.Find("Player");
        collectibleName = GetComponent<SpriteRenderer>().sprite.name;
        gameManager = GameObject.Find("GameManager");
    }

    void FixedUpdate()
    {
        //After the lid was opened but before the item was unpacked
        if(trashLid.tag == "Corpse" && unpacked == false){
            
            //Move to the new position and start coroutine to take the object
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * moveSpeed);
            PlayerPrefs.SetInt(collectibleName, 1);
            StartCoroutine(takeItem(transform, newScale, 2f));
        }
    }

    IEnumerator takeItem(Transform item, Vector3 toScale, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isScaling)
        {
            yield break; ///exit if this is still running
        }
        isScaling = true;
        gameManager.GetComponent<GameManager>().CollectibleFound();
        float counter = 0;

        //Get the current scale of the item
        Vector3 startScaleSize = item.localScale;

        //Scale the item
        while (counter < duration)
        {
            counter += Time.deltaTime;
            item.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }

        counter = 0;
        
        //Make the item fly towards the player
        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, counter / duration);
            //Once the item is at the player's position, break
            if(transform.position == player.transform.position){
                break;
            }
            yield return null;
        }

        isScaling = false;
        //Set as unpacked and destroy the item
        unpacked = true;
        Destroy(transform.gameObject);
        
    }
}
