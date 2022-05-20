using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpcollider : MonoBehaviour
{

    void OnTriggerStay2D(Collider2D other) {
        this.transform.parent.GetComponent<PlayerController>().isJumping = false;
    }

    void OnTriggerExit2D(Collider2D other) {
        this.transform.parent.GetComponent<PlayerController>().isJumping = true;
    }
}
