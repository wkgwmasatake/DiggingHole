﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;

public class StageScript : MonoBehaviour
{

    [SerializeField] GameObject[] block = new GameObject[Define.blocktype];
    int[,] Barray = new int[Define.yarray, Define.xarray];
    GameObject[,] Bobj = new GameObject[Define.yarray, Define.xarray];
    [SerializeField] GameObject player;
    Vector2[,] Bposition = new Vector2[Define.yarray, Define.xarray];

    int px, py;


    void Start()
    {

        px = 1;
        py = 0;

        player = Instantiate(player, new Vector2(-0.5f, 0), Quaternion.identity);
        for (int y = 0; y < Define.yarray; y++)
        {
            for (int x = 0; x < Define.xarray; x++)
            {
                Bposition[y, x] = new Vector2(x - 1.5f, -y);

                if (player.transform.position.x == Bposition[y, x].x && player.transform.position.y == Bposition[y, x].y)
                {
                    Barray[y, x] = 5;
                }
                else
                {
                    Barray[y, x] = Random.Range(0, Define.blocktype);
                    Bobj[y, x] = Instantiate(block[Barray[y, x]], Bposition[y, x], Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerMove(string type)
    {
        switch (type)
        {
            case "right":
                if (px < Define.xarray - 1)
                {
                    Barray[py, px] = 6;
                    Barray[py, px + 1] = 5;
                    player.transform.position = Bposition[py, px + 1];
                    px++;
                    StageUpdete();
                }
                break;

            case "left":
                if (px > 0)
                {
                    Barray[py, px] = 6;
                    Barray[py, px - 1] = 5;
                    player.transform.position = Bposition[py, px - 1];
                    px--;
                    StageUpdete();
                }
                break;

            case "buttom":
                if (py < Define.yarray - 1)
                {
                    if (py < 3)
                    {
                        py++;
                        Barray[py, px] = 6;
                        Barray[py, px] = 5;
                        player.transform.position = Bposition[py, px];
                    }
                    else
                    {
                        Barray[py, px] = 6;
                        Barray[py + 1, px] = 5;
                        for (int y = 0; y < Define.yarray; y++)
                        {
                            for (int x = 0; x < Define.xarray; x++)
                            {
                                if (y == Define.yarray - 1)
                                    Barray[y, x] = Random.Range(0, Define.blocktype);
                                else
                                {
                                    int box = Barray[y + 1, x];
                                    Barray[y, x] = box;
                                }
                            }
                        }

                    }
                    StageUpdete();

                }
                break;

        }
    }

    void StageUpdete()
    {
        for (int y = 0; y < Define.yarray; y++)
        {
            for (int x = 0; x < Define.xarray; x++)
            {
                if (Bobj[y, x] != null)
                    Destroy(Bobj[y, x]);

                if (Barray[y, x] != 5 && Barray[y, x] != 6)
                    Bobj[y, x] = Instantiate(block[Barray[y, x]], Bposition[y, x], Quaternion.identity);
            }
        }
    }
}
