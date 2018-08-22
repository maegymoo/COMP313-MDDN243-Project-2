using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]
    public GameObject dBox;

    [SerializeField]
    public Text dText;

    [SerializeField]
    public bool active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(active&& Input.GetKeyDown(KeyCode.Return)){
            dBox.SetActive(false);
            active = false;
        }
	}

    public void ShowBox(string dialogue){
        active = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }

}
