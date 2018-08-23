using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionZoneController : MonoBehaviour {

    private DialogueManager manager;
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            manager.ShowBox(21);
            player.DrinkPotion();
            Destroy(gameObject);
        }
    }
}
