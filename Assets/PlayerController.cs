using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int MaxHealth = 100;
    
    [HideInInspector]
    private int _health;
    
    // Manages keeping the health variable in a safe range
    public int Health {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            if (_health <= 0) {
                _health = 0;
                Die();
            }
        }
    }

    //rigid body and animator object on player
    private Rigidbody2D rb;
    public Animator animator;
    //player variables
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    public float friction;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingLeft;
    private bool isAttack;
    public bool attackCooldown;
    public float attackDuration;
    private float attackTime;
    private Vector3 scale;
    public Inventory inventory;    
    public AudioClip SlashSound;


    // Jumping variables
    public float MaxJumpCount = 2;
    private int jumpCount = 0;
    private TimeSpan JumpCooldown = TimeSpan.FromSeconds(0.20);
    private DateTime lastJumpTime = DateTime.Now;


    void Die()
    {
        Debug.Log("Died");
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;

        rb = gameObject.GetComponent<Rigidbody2D>();
        inventory = new Inventory(); 
        facingLeft = true;
        
        attackCooldown = false;
        isAttack = false;
        attackTime = 0;
        //this is for flipping later
        scale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //gets wasd and arrow key inputs
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        //lets space also trigger 
        if(Input.GetKey("space")){ moveVertical = 1;}

        //attack handling
        if(!isAttack){
            if(Input.GetKey("z")){ 
                AudioSource.PlayClipAtPoint(SlashSound, transform.position);
                isAttack = true; attackTime = 0; attackCooldown = false;
            }
        }else{
            if(!Input.GetKey("z")){
                    attackCooldown = true;
            }
            if(attackTime < attackDuration){
                animator.SetBool("isAttacking", true);;
            }
            attackTime+=60*Time.deltaTime;
            if(attackTime >= attackDuration){
                animator.SetBool("isAttacking", false);
                if(attackCooldown){ isAttack = false; }
            }
            
        }

        
        
    } 

    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;

        //left right jump movement
        if(moveHorizontal > 0.1f){
            rb.AddForce(new Vector2(moveSpeed, 0f), ForceMode2D.Impulse);
            if(!facingLeft){ this.gameObject.transform.localScale = new Vector3(1*scale.x, scale.y, scale.z); }
            facingLeft = true;
        }
        if(moveHorizontal < -0.1f){
            rb.AddForce(new Vector2(-moveSpeed, 0f), ForceMode2D.Impulse);
            if(facingLeft){ this.gameObject.transform.localScale = new Vector3(-1*scale.x, scale.y, scale.z);}
            facingLeft = false;
        }
        if(moveVertical > 0.1f && (jumpCount < MaxJumpCount) && (DateTime.Now - lastJumpTime) > JumpCooldown){
            vel.y = 0;
            rb.velocity = vel;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            lastJumpTime = DateTime.Now;
            moveVertical = 0;
            jumpCount++;
        }

        vel = rb.velocity;
        //clamps speed at max speed
        if(vel.x > maxSpeed){ vel.x = maxSpeed; }
        if(vel.x < -maxSpeed){ vel.x = -maxSpeed; }

        //applies a simulated
        vel.x = vel.x*friction;
        rb.velocity = vel;

        //update animator
        animator.SetFloat("xSpeed", Mathf.Abs(moveHorizontal));
        animator.SetBool("isJumping", (jumpCount != 0));
        
    }

    void OnTriggerStay2D(Collider2D other) {
        jumpCount = 0;
        lastJumpTime = DateTime.MinValue; // Last jump time is irreleveant once we've landed
    }

    void OnTriggerExit2D(Collider2D other) {
        if (jumpCount == 0) jumpCount++;
    }
}
