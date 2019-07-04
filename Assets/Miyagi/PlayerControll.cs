using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour {
    
    float time;
    public bool startFlg;
    public string buttomtype;
    private int playerlife;

    [SerializeField] Text lifetext;

	// Use this for initialization
	void Start () {
        time = 0;
        startFlg = false;
        playerlife = 10;
        lifetext.text = ""+playerlife;
    }
	
	// Update is called once per frame
	void Update () {

        if(startFlg == true)
        GetKeyType();

        if((time += Time.deltaTime) > 1)
        {
            playerlife -= 1;
            lifetext.text = "" + playerlife;
            time = 0;
        }
        if(playerlife == 0)
        {
            SceneManager.LoadScene("Main");
            GetComponent<StageScript>().Start();
            GetComponent<StartScript>().Start();
            Start();

        }

        Debug.Log(playerlife);
    }

    private void GetKeyType()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
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
