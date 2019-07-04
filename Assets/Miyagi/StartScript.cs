using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

    [SerializeField]Text textStart;
    float alpha;
    float alphaspeed;

    int time;
	public void Start () {
        alpha = 1;
        time = 0;
        alphaspeed = 0.02f;
    }
	
	
	void Update () {

        if (textStart != false)
        {
            textStart.GetComponent<Text>().color = new Color(0, 0, 0, alpha);
            if (alpha <= 0.3 || alpha >= 1)
                alphaspeed *= -1;
            alpha += alphaspeed;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<PlayerControll>().startFlg = true;
            textStart.enabled = false;
        }
	}
}
