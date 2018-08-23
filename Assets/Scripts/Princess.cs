using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour {

    private Rigidbody2D rb;
    bool kidnapped;
    float speed;



	void Start () {
        kidnapped = false;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(kidnapped){
            goToBigBoss();
        }
	}

    public void TurnOnGravity(){
        rb.gravityScale = 1;
    }

    public void Kidnap(float speed){
        kidnapped = true;
        this.speed = speed;
    }

    public void goToBigBoss(){
        rb.gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        Transform target = FindObjectOfType<BigBoss>().transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

	public void OnCollisionStay2D(Collision2D collision)
	{
        if(collision.gameObject.name == "Big Boss"){
            kidnapped = false;
            rb.gravityScale = 1;
            GetComponent<Collider2D>().enabled = true;

        }
	}

}
	

