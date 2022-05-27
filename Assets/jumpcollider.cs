﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpcollider : MonoBehaviour
{

    void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Collided with " + other.gameObject.name);
        this.transform.parent.GetComponent<PlayerController>().Land();
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Left collider");
        this.transform.parent.GetComponent<PlayerController>().LeavePlatform();
    }
}
