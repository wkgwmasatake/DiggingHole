using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {
    
    float time = 0;

    public string buttomtype;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.D)) {
            buttomtype = "right";
            GetComponent<StageScript>().PlayerMove(buttomtype);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttomtype = "left";
            GetComponent<StageScript>().PlayerMove(buttomtype);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            buttomtype = "buttom";
            GetComponent<StageScript>().PlayerMove(buttomtype);
        }


    }
}
