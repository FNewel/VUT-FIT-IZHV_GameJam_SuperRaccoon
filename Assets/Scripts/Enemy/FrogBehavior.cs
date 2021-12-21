using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{

    public Transform m_WallCheck;

    public Transform m_GroundCheck;

    public float k_CheckRadius;

    public LayerMask m_WhatIsGround;

    public float jumpForce;

    public GameObject sprite;


    private Vector3 jumpDirection;

    private bool abtToJump = true;

    public Animator animator;

    public int[] ignoreLayers;


    // Start is called before the first frame update
    void Start()
    {
        abtToJump = true;

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
                if(transform.rotation.eulerAngles.y == 180){
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(1,0,0)*jumpForce);
                }else{
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(-1,0,0) *jumpForce);
                }

                
            }
        }else{
            sprite.GetComponent<Animator>().enabled = false;
        }

        if (Physics2D.OverlapCircle(m_GroundCheck.position, k_CheckRadius, m_WhatIsGround)){
            animator.SetBool("Grounded", true);
        }else{
            animator.SetBool("Grounded", false);
        }
    }

    void FixedUpdate(){
        if(gameObject.tag != "Corpse" && abtToJump){
                StartCoroutine(FrogJump(3));

        }
    }
    IEnumerator FrogJump(float secs){

        abtToJump = false;

        Debug.Log("Jump");

        yield return new WaitForSeconds(secs);

        if(transform.rotation.eulerAngles.y == 180)
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0.8f,1.2f,0) * jumpForce);
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-0.8f,1.2f,0) * jumpForce);

        abtToJump = true;
    }
}