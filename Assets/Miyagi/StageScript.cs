using System.Collections;
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
    int tmpx, tmpy;

    void Start()
    {


        px = 1;
        py = 0;
        tmpx = 1;
        tmpy = 0;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(Barray[py, px]);
        }
    }

    public void PlayerMove(string type)
    {
        switch (type)
        {
            case "right":
                if (px < Define.xarray - 1)
                {
                    px++;
                    if(Barray[py, px] != 6)
                    CheckBlock();
                    
                    px--;
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
                    px--;
                    if (Barray[py, px] != 6)
                        CheckBlock();
                    
                    px++;
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
                        if (Barray[py, px] != 6)
                            CheckBlock();

                        Barray[py-1, px] = 6;
                        Barray[py, px] = 5;
                        player.transform.position = Bposition[py, px];
                    }
                    else
                    {
                        py++;
                        if (Barray[py, px] != 6)
                            CheckBlock();
                        py--;
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

    private void CheckBlock()
    {
        bool checkFlg1 = false;
        bool checkFlg2 = false;
        bool checkFlg3 = false;
        bool checkFlg4 = false;

        //Debug.Log("player" + Barray[py, px]);
        //if(py != 0 && Barray[py - 1, px] == Barray[py, px])
        //Debug.Log("上" + Barray[py-1, px]  + (py != 0 && Barray[py - 1, px] == Barray[py, px]));
        if (py != 0 && Barray[py - 1, px] == Barray[py, px])
        {
            tmpy = py - 1; tmpx = px;
            CheckBlock2(tmpy,tmpx,1, Barray[py, px]);
            checkFlg1 = true;
           
        }
        //Debug.Log("下" + Barray[py + 1, px] + (Barray[py + 1, px] == Barray[py, px]));
        if (Barray[py + 1, px] == Barray[py, px])
        {
            tmpy = py + 1; tmpx = px;
            CheckBlock2(tmpy, tmpx, 2, Barray[py, px]);
            checkFlg2 = true;
            
            Debug.Log("!!!");
        }

        //Debug.Log("右" + Barray[py , px + 1] + (px < Define.xarray - 1 && Barray[py, px + 1] == Barray[py, px]));
        if (px < Define.xarray -1 && Barray[py, px + 1] == Barray[py, px])
        {
            tmpy = py; tmpx = px + 1;
            CheckBlock2(tmpy, tmpx, 3, Barray[py, px]);
            checkFlg3 = true;
            
        }
        //if(px > 0 && (Barray[py, px - 1] == Barray[py, px]))
        //Debug.Log("左" + Barray[py , px - 1] + (px > 0 && (Barray[py, px - 1] == Barray[py, px])));
        if (px > 0 && Barray[py, px - 1] == Barray[py, px])
        {
            tmpy = py; tmpx = px - 1;
            CheckBlock2(tmpy, tmpx, 4, Barray[py, px]);
            checkFlg4 = true;
            
        }

        if(checkFlg1 == true)
        {
            checkFlg1 = false;
            Barray[py - 1, px] = 6;
        }
        if (checkFlg2 == true)
        {
            checkFlg2 = false;
            Barray[py + 1, px] = 6;
        }
        if (checkFlg3 == true)
        {
            checkFlg3 = false;
            Barray[py, px + 1] = 6;
        }
        if (checkFlg4 == true)
        {
            checkFlg4 = false;
            Barray[py, px - 1] = 6;
        }
    }

    //private void CheckBlock2()
    //{
    //    //Debug.Log("player-1-" + Barray[tmpy, tmpx]);
    //    //Debug.Log("下" + Barray[tmpy + 1, tmpx] + (Barray[tmpy + 1, tmpx] == Barray[tmpy, tmpx]));
    //    if (Barray[tmpy + 1, tmpx] == Barray[tmpy, tmpx])
    //    {
            
    //        Barray[tmpy + 1, tmpx] = 6;
    //    }
    //   // Debug.Log("上-1-" + Barray[tmpy - 1, tmpx] + (tmpy != 0 && Barray[tmpy - 1, tmpx] == Barray[tmpy, tmpx]));
    //    if (tmpy != 0 && Barray[tmpy - 1, tmpx] == Barray[tmpy, tmpx])
    //    {
            
    //        Barray[tmpy - 1, tmpx] = 6;
    //    }
    //    //Debug.Log("右-1-" + Barray[tmpy, tmpx + 1] + (tmpx < Define.xarray - 1 && Barray[tmpy, tmpx + 1] == Barray[tmpy, tmpx]));
    //    if (tmpx < Define.xarray - 1 && Barray[tmpy, tmpx + 1] == Barray[tmpy, tmpx])
    //    {
            
    //        Barray[tmpy, tmpx + 1] = 6;
    //    }
    //    //if(tmpx > 0)
    //    //Debug.Log("左1-1" + Barray[tmpy, tmpx - 1] + (tmpx > 0 && Barray[tmpy, tmpx - 1] == Barray[tmpy, tmpx]));
    //    if (tmpx > 0 && Barray[tmpy, tmpx - 1] == Barray[tmpy, tmpx])
    //    {
    //        Barray[tmpy, tmpx - 1] = 6;
    //    }
    //    
    //    
    //}
    void CheckBlock2(int y, int x, short type, int color)
    {
        
        //下のブロックが同じ色か
        if ((Barray[y + 1, x] == Barray[y, x]) && (type != 1) && (Barray[y + 1, x] != 6))
        {
            Barray[y + 1, x] = 6;
            CheckBlock2(y + 1, x, 2, color);
            
        }
        //無限ループ止めるよう
        else if((type != 1) && (Barray[y, x] == 6) && (Barray[y + 1, x] == color) && (Barray[y+1, x ] != 6))
        {
            Barray[y + 1, x] = 6;
            CheckBlock2(y + 1, x, 2, color);
          
        }


        //上のブロックが同じ色か
        if ((y != 0) && (Barray[y - 1, x] == Barray[y, x]) && (type != 2) && (Barray[y - 1, x] != 6))
        {
            Barray[y - 1, x] = 6;
            CheckBlock2(y - 1, x, 1, color);
            
        }
        //無限ループ止めるよう
        else if ((y != 0) && (type != 2) && (Barray[y, x] == 6) && (Barray[y - 1, x] == color) && (Barray[y-1, x] != 6))
        {
            Barray[y - 1, x] = 6;
            CheckBlock2(y - 1, x, 1, color);
            
        }


        //右のブロックが同じ色か
        if ((x < Define.xarray - 1) && (Barray[y, x + 1] == Barray[y, x]) && (type != 4) && (Barray[y, x + 1] != 6))
        {
            Barray[y, x + 1] = 6;
            CheckBlock2(y, x + 1, 3, color);

        }
        //無限ループ止めるよう
        else if ((x < Define.xarray - 1) && (type != 4) && (Barray[y, x] == 6) && (Barray[y, x + 1] == color) && (Barray[y, x + 1] != 6))
        {
            Barray[y, x + 1] = 6;
            CheckBlock2(y, x + 1, 3, color);
        }


        //左のブロックが同じ色か
        if ((x > 0) && (Barray[y, x - 1] == Barray[y, x]) && (type != 3) && (Barray[y , x-1] != 6))
        {
            Barray[y, x - 1] = 6;
            CheckBlock2(y, x - 1, 4, color);
            
        }
        //無限ループ止めるよう
        else if ((x > 0) && (type != 3) && (Barray[y, x] == 6) && (Barray[y, x - 1] == color) && (Barray[y, x - 1] != 6))
        {
            Barray[y, x - 1] = 6;
            CheckBlock2(y, x - 1, 4, color);
            
        }

        
    }

}
