using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordZoneController : MonoBehaviour {
    
    private DialogueManager manager;
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D other){
        Debug.Log("Triggered");
        if (other.gameObject.name == "Player")
        {
            manager.ShowBox(15);
            player.PickUpSword();
            GetComponent<Renderer>().enabled = false;
        }
    }
}
