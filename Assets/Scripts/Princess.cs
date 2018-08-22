using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour {

    private Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnOnGravity(){
        rb.gravityScale = 1;
    }
}
