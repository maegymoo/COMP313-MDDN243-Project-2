using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordZoneController : MonoBehaviour {
    
    private DialogueManager manager;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D other){
        Debug.Log("Triggered");
        if (other.gameObject.name == "Player")
        {
            manager.ShowBox(15);
            animator.SetTrigger("hassword");
        }
    }
}
