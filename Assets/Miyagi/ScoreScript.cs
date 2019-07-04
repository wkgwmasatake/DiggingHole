using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    [SerializeField]int BlockCount;
    float Score;
    public Text scoreText; // スコアの UI

    // Use this for initialization
    void Start () {

        //ScoreScript ScoreConfig;
        //ScoreConfig = this.GetComponent<ScoreScript>();
        //ScoreConfig.AddScoreBlock();
        //ScoreConfig.AddScore();

        // UI を初期化
        Score = 0;
        SetCountText();

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
                // UI の表示を更新
                SetCountText();
            }

            BlockCount = 0;
            
        }
        Debug.Log(Score);
        return Score;
    }

    // UI の表示を更新する
    void SetCountText()
    {
        // スコアの表示を更新
        scoreText.text = "Count: " + Score.ToString();

    }

}
