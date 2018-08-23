using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    GameObject targetobject;
    Vector2 target;
    public float speed;
    Player player;

    void Start () {
        player = FindObjectOfType<Player>();
        target = new Vector2(player.transform.position.x, player.transform.position.y);
    
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProjectile();
        }
    }

	private void OnTriggerStay2D(Collider2D other)
	{
        if(other.CompareTag("Player")){
            Debug.Log("hitplayer");
            player.TakeDamage();
            DestroyProjectile();
        }
	}

	void DestroyProjectile(){
        Destroy(gameObject);
    }
}
