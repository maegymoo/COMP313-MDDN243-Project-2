using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMonster : MonoBehaviour {

	// Use this for initialization
    private Transform player;
    public float speed;
    public Transform princess;
    bool stealing;
    public float stoppingDistance;
    public float retreatDistance;

    public GameObject projectile;
    private float timeBtwnShots;
    public float startTimeBtwnShots;

    private void Start()
    {
        timeBtwnShots = startTimeBtwnShots;
        stealing = false;
        princess = FindObjectOfType<Princess>().GetComponent<Transform>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
    }

	public void FixedUpdate()
    {
        if(stealing){
            StealPrincess();  
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GetComponent<Collider2D>().enabled = true;
            ChasePlayer();
            Shoot();
        }
    }

    private void ChasePlayer(){
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    private void Shoot(){
        if(timeBtwnShots <=0){
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        } else {
            timeBtwnShots -= Time.deltaTime;  
        }
    }

	public void StealPrincess(){
        stealing = true;
        transform.position = Vector2.MoveTowards(transform.position, princess.position, speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Princess")
        {
            FindObjectOfType<Princess>().Kidnap(speed);
            GetComponent<Collider2D>().enabled = false;
            goToBattlePosition();

        }
        if(other.gameObject.name == "Zone2Controller"){
            stealing = false;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    void goToBattlePosition(){
        Transform pos = FindObjectOfType<Zone2Controller>().transform;
        transform.position = Vector2.MoveTowards(transform.position, pos.position, speed * Time.deltaTime);
        speed = 4;
    }

}
