using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private GameObject player;
    private DialogueManager manager;
    public float xMin;
    public float xMax;
    public float yMin;

    internal static Vector3 ScreenToWorldPoint(Vector3 mousePosition)
    {
        throw new NotImplementedException();
    }

    public float yMax;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        manager = FindObjectOfType<DialogueManager>();

	}
	
	// Update is called once per frame
	void LateUpdate () {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
	}

	private void OnTriggerStay(Collider other)
	{
        if(other.gameObject.name == "Sword"){
            manager.ShowBox(16);
        }
        if(other.gameObject.name == "Potion"){
            manager.ShowBox(22);
        }
        if(other.gameObject.name == "Ground Monster"){
            manager.ShowBox(20);
        }


	}
}
