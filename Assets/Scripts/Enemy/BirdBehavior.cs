using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{

    public Transform m_WallCheck;


    public float k_CheckRadius;

    public LayerMask m_WhatIsGround;
    
    public GameObject sprite;

    public float moveSpeed;

    public int[] ignoreLayers;


    // Start is called before the first frame update
    void Start()
    {

        foreach(int layer in ignoreLayers){
            Physics2D.IgnoreLayerCollision(8, layer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.tag != "Corpse")
        {   
           if (Physics2D.OverlapCircle(m_WallCheck.position, k_CheckRadius, m_WhatIsGround))
            {
                transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y - 180,0);
            }
        }else{
            GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            sprite.GetComponent<Animator>().enabled = false;
        }
    }

    void FixedUpdate(){
        if(gameObject.tag != "Corpse"){
            if(transform.rotation.eulerAngles.y == 180){
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }else{
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
