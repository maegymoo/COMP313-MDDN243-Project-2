using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {


    public GameObject dBox;
    public Text dText;

    [SerializeField]
    public string[] storyline;

    public bool active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ShowBox(int i){
        active = true;
        dBox.SetActive(true);
        dText.text = storyline[i];
    }

}
