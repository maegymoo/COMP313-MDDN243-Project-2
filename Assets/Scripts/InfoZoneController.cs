using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoZoneController: MonoBehaviour {

    private DialogueManager manager;
    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            manager.ShowBox(9);
        }
    }

}
