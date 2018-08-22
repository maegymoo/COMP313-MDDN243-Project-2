using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMonster : MonoBehaviour {

	// Use this for initialization
    private Transform target;
    public float speed;
    public Princess princess;
    bool stealing;

    private void Start()
    {
        stealing = false;
        princess = FindObjectOfType<Princess>();
        target = princess.GetComponent<Transform>();
    }

	public void FixedUpdate()
	{
        if(stealing){
            StealPrincess();
            
        }
	}

	public void StealPrincess(){
        stealing = true;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Princess")
        {
            princess.Kidnap(speed);
            GetComponent<Collider2D>().enabled = false;
            goToBattlePosition();

        }
        if(other.gameObject.name == "Zone2Controller"){
            stealing = false;
            GetComponent<Collider2D>().enabled = true;

        }
    }

    void goToBattlePosition(){
        target = FindObjectOfType<Zone2Controller>().transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

}
