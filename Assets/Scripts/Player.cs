using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    private float horizontal;
    private Animator animator;
   
    [SerializeField]
    private float speed;

    private bool facingRight;

	// Use this for initialization
	void Start () {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void fixedUpdate () {
        
        horizontal = Input.GetAxis("Horizontal");
        Movement(horizontal);
        Animation();
		
	}
    private void Movement(float horizontal) {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal){
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Animation() {
        
    }
}
