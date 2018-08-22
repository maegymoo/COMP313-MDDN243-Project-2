using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager manager;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            manager.ShowBox(dialogue);
        }
    }

}
