using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //rigid body and animator object on player
    private Rigidbody2D rb;
    public Animator animator;
    //player variables
    public AudioClip AttackSound;
    public AudioClip HitSound;
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    public float friction;
    public bool isJumping;
    public float knockback = 2f;
    public int damage = 1;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingLeft;
    public bool isAttack;
    public bool attackCooldown;
    public float attackDuration;
    private float attackTime;
    public bool isHit;
    public float hitCooldown = 1;
    private float hitTime;
    private Vector3 scale;
    public Inventory inventory;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        inventory = new Inventory(); 
        inventory.start();
        isJumping = false;
        facingLeft = true;
        isHit = false;
        hitTime = 0;
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
                isAttack = true; attackTime = 0; attackCooldown = false;
                AudioSource.PlayClipAtPoint(AttackSound, transform.position, 1f);
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

        //hit handling
        if(isHit){
            animator.SetBool("isHit", true);
            hitTime += Time.deltaTime;
            if(hitTime >= hitCooldown){
                hitTime = 0;
                isHit = false;
            }
        }else{
            animator.SetBool("isHit", false);
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
        if(moveVertical > 0.1f && !isJumping){
            vel.y = 0;
            rb.velocity = vel;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        vel = rb.velocity;
        //clamps speed at max speed
        if(vel.x > maxSpeed){ vel.x = maxSpeed; }
        if(vel.x < -maxSpeed){ vel.x = -maxSpeed; }

        //applies a simulated friction
        vel.x = vel.x*friction;
        rb.velocity = vel;

        //update animator
        animator.SetFloat("xSpeed", Mathf.Abs(moveHorizontal));
        animator.SetBool("isJumping", isJumping);
        
    }

}
