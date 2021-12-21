using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 20f;

    float horizontalMove = 0f;

    public bool jump = false;

    public bool crouch = false;

    public bool sprint = false;

    public Animator animator;

    private Vector3 knockDirection;

    private Rigidbody2D player_rb;

    private GameObject enemy;

    private Rigidbody2D enemy_rb;

    private GameObject gameManager;

    



    void Start(){
        gameManager = GameObject.Find("GameManager");
        player_rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 9);
        Physics2D.IgnoreLayerCollision(3, 12);
        Physics2D.IgnoreLayerCollision(8, 12);

    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetButtonDown("Sprint")){
            runSpeed = 35.0f;
            
        }else if (Input.GetButtonUp("Sprint")){
            runSpeed = 20.0f;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(Input.GetButtonDown("Crouch")){
            crouch = true;
            animator.SetBool("Crouching", crouch);
            
        }else if (Input.GetButtonUp("Crouch")){
            crouch = false;
            animator.SetBool("Crouching", crouch);
        }
        
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove/10));
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
    }


    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            this.GetComponent<AudioSource>().Play();
            //Debug.Log("Hit");
            enemy = other.gameObject;
            enemy_rb = enemy.GetComponent<Rigidbody2D>();

            gameManager.GetComponent<GameManager>().GetDamage();

            knockDirection = player_rb.transform.position - other.transform.position;
            player_rb.AddForce( knockDirection.normalized * 150f);
            controller.m_AirControl = false;

        }else if(other.gameObject.tag == "EnemyTop"){
            jump = true;
            Debug.Log("Kill");
            StartCoroutine(KillEnemy(0.05f , other.gameObject.transform.parent.gameObject));
        }else if(other.gameObject.tag == "TrashLid"){
            jump = true;
        }
    }

    IEnumerator KillEnemy(float secs, GameObject enemy){
        
        
        yield return new WaitForSeconds(secs);
        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x, 0.4f ,enemy.transform.localScale.z);
        foreach(Transform child in enemy.transform)
        {
            child.gameObject.tag = "Corpse";
            child.gameObject.layer = 9;
        }
        enemy.tag = "Corpse";
        enemy.layer = 9;

        
    }
    
    /*void AnimateCharacter()  -----som si požičal z izhv, nemazat, tu budu animacie pre mývala
    {
        
        mInput = controller;
        if(mInput.move.x == 1)
		    transform.localRotation = Quaternion.Euler(transform.localRotation.x,0,transform.localRotation.z);
		else if (mInput.move.x == -1)
		    transform.localRotation = Quaternion.Euler(transform.localRotation.x,180,transform.localRotation.z);


        var animator = transform.Find("PlayerSprite").GetComponent<Animator>();
	    if (animator != null)
	    {
			var currentVerticalSpeed = mController.velocity.y;
			var currentHorizontalSpeed = new Vector3(mController.velocity.x, 0.0f, mController.velocity.z).magnitude;
			
			// Property values: 
			var speed = currentHorizontalSpeed;
			var moveSpeed = Math.Abs(mTargetHorSpeed / MoveSpeedAnimation);
			var crouch = mInput.crouch;
			var grounded = mController.isGrounded;
			var jump = mInput.jump;
			var falling = !mController.isGrounded && mFallTimeoutDelta <= 0.0f;

			
			animator.SetFloat("Speed", speed);
			animator.SetBool("Crouch", crouch);
		    animator.SetBool("Grounded", grounded);
		    animator.SetBool("Jump", jump);
		    animator.SetBool("Fall", falling);
		    animator.SetFloat("MoveSpeed", moveSpeed);
		    
	    }*/
}
