using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;

    public float maxSpeed = 4f;
    public float moveSpeed = 1f;
    public float minDistance = 1f;
    public float maxDistance = 7f;
    private bool facingLeft;
    public float friction = 0.6f;
    public float jumpForce = 8f;
    public float knockback = 10;
    //The time since last jump
    private float alphaLevel = 1;
    private float _timeSinceJump = 0f;

    public float MaxVerticalDistance = 1f;
    private Vector3 scale;

    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isDead", false);
        facingLeft = true;
        _rb = GetComponent<Rigidbody2D>();
        //this is for flipping later
        scale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player == null) 
        {
            Player = GameObject.FindWithTag("Player");
        }

        // Move towards the player
        var target = Player.transform;
        var distance = Vector3.Distance(transform.position, target.position);
        if(this.gameObject.GetComponent<EnemyHandler>().IsAlive){
            if (minDistance < distance && distance < maxDistance)
            {
                var dir = (target.position - transform.position).normalized;
                //Check if dir is facing left or right
                MoveHorizontally(dir);
                if (distance < MaxVerticalDistance)
                {
                    MoveVertically(dir);
                }
            }
        }else{
            //set to dead
            animator.SetBool("isDead", true);
            gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
            alphaLevel -= Time.deltaTime;
        }
    }

    
    void MoveVertically(Vector2 dir)
    {
        

        _timeSinceJump += Time.deltaTime;
        if (dir.y > 0)
        {
            if (_timeSinceJump > 2.0f)
            {
                _rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
                _timeSinceJump = 0;
            }
        }
    
    }

    void MoveHorizontally(Vector2 dir)
    {
        int dirx;
        if (dir.x < 0)
        {
            if(!facingLeft){ this.gameObject.transform.localScale = new Vector3(1*scale.x, scale.y, scale.z); }
            facingLeft = true;
            dirx = -1;
        } else {
            if(facingLeft){ this.gameObject.transform.localScale = new Vector3(-1*scale.x, scale.y, scale.z);}
            facingLeft = false;
            dirx = 1;
        }

        _rb.AddForce(new Vector2(moveSpeed*dirx, 0f), ForceMode2D.Impulse);

        Vector2 vel = _rb.velocity;
        //clamps speed at max speed
        if(vel.x > maxSpeed){ vel.x = maxSpeed; }
        if(vel.x < -maxSpeed){ vel.x = -maxSpeed; }

        //applies a simulated friction
        vel.x = vel.x*friction;
        _rb.velocity = vel;
    }


}

