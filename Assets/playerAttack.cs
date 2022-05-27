using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other) {
        //get player script
        PlayerController player = this.transform.parent.GetComponent<PlayerController>();
        //get player position
        Vector2 playerPos = this.transform.position;
        if(other.gameObject.tag == "Enemy" && player.isAttack == true){
            Debug.Log("attacking");
            if(!other.gameObject.GetComponent<GroundFollow>().isAlive){
                return;
            }
            //get enemy postion 
            Vector2 enemyPos = other.gameObject.transform.position;
            //get knockback vector
            Vector2 dir = (playerPos - enemyPos).normalized;
            //apply knockback
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*-player.knockback, ForceMode2D.Impulse);
            //damage other player
            other.gameObject.GetComponent<EnemyHandler>().TakeDamage(player.damage);
            
        }
    }
}
