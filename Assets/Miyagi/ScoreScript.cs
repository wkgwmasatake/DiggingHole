using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

    [SerializeField]int BlockCount;
    float Score;

	// Use this for initialization
	void Start () {

        //ScoreScript ScoreConfig;
        //ScoreConfig = this.GetComponent<ScoreScript>();
        //ScoreConfig.AddScoreBlock();
        //ScoreConfig.AddScore();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            AddScore();
        }

    }

    public void AddScoreBlock()
    {
        BlockCount++;
    }

    public float AddScore()
    {
        if (BlockCount >= 1)
        {
            Score += 100;
            for (int i = 1; BlockCount > i; i++)
            {
                Score += 100 + (50 * i);
            }

            BlockCount = 0;
        }
        Debug.Log(Score);
        return Score;
    }

}
