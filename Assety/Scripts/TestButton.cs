using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestButton : MonoBehaviour {

    public List<GameObject> objs = new List<GameObject>();

    public int target_x;
    public int target_y;
    public int move = 2;
    public int x_Check = 0;

    public int move_X; //wsporzedna x hexu na ktory ma sie przeniesc statek
    public int move_Y;

    GameObject targetObj;

    //Przenosi obiekt do podanych wspolrzednych
    void TeleportToHex()
    {
        foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
        {
            if (hex.GetComponent<HexNumber>().hex_x == target_x && hex.GetComponent<HexNumber>().hex_y == target_y)
            {
                transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y, -2);
            }
        }
    }

    //Szuka drogi do celu
   public void MoveToHex()
    {

        //List<GameObject> objs = GameObject.FindGameObjectsWithTag("tag");

        int current_X = gameObject.GetComponent<ShipStats>().pos_X; //obecna wspolrzedna X statku
        int current_Y = gameObject.GetComponent<ShipStats>().pos_Y; //obecna wspolrzedna Y statku

        //sprawdza czy na podanym polu znajduje sie jakis inny obiekt
        foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
        {
            int i = 0;
            if (hex.GetComponent<HexNumber>().hex_x == target_x && hex.GetComponent<HexNumber>().hex_y == target_y)
            {
                targetObj = hex;
                if (hex.GetComponent<HexNumber>().occupied == true)
                {
                    if (target_y % 2 == 0)
                    {
                        foreach (GameObject shex in GameObject.FindGameObjectsWithTag("HexMap"))
                        {

                        

                        if (shex.GetComponent<HexNumber>().hex_x == target_x && shex.GetComponent<HexNumber>().hex_y == target_y + 1)
                        {
                            objs[i] = shex;
                            i++;
                        }
                        if (shex.GetComponent<HexNumber>().hex_x == target_x + 1 && shex.GetComponent<HexNumber>().hex_y == target_y)
                        {
                            objs[i] = shex;
                            i++;
                        }
                        if (shex.GetComponent<HexNumber>().hex_x == target_x && shex.GetComponent<HexNumber>().hex_y == target_y - 1)
                        {
                            objs[i] = shex;
                            i++;
                        }
                        if (shex.GetComponent<HexNumber>().hex_x == target_x - 1 && shex.GetComponent<HexNumber>().hex_y == target_y - 1)
                        {
                            objs[i] = shex;
                            i++;
                        }
                        if (shex.GetComponent<HexNumber>().hex_x == target_x - 1 && shex.GetComponent<HexNumber>().hex_y == target_y)
                        {
                            objs[i] = shex;
                            i++;
                        }
                        if (shex.GetComponent<HexNumber>().hex_x == target_x - 1 && shex.GetComponent<HexNumber>().hex_y == target_y + 1)
                        {
                            objs[i] = shex;
                            i++;
                        }
                    }
                    }
                    else
                    {

                    }
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            //Debug.Log(objs[i].transform);
        }


        move_X = current_X; //wspolrzedna x hexu na ktory ma sie przeniesc statek
        move_Y = current_Y; //wspolrzedna y hexu na ktory ma sie przeniesc statek

    for(int i = 0; i < move; i++)
        {

        switch (x_Check)
        {
            case 0:
                //Skrpyt najpierw sprwadza wspolrzedna X
                if (current_Y % 2 == 0)
                {
                    if (target_x < current_X)
                    {
                        move_X--;
                        if (target_y < current_Y)
                        {
                            move_Y--;
                        }
                        if (target_y > current_Y)
                        {
                            move_Y++;
                        }
                    }
                    else if (target_x > current_X)
                    {
                        move_X++;

                    }
                    else if (target_x == current_X)
                    {
                        if (target_y < current_Y)
                        {
                            move_Y--;
                        }
                        if (target_y > current_Y)
                        {
                            move_Y++;
                        }
                    }
                }
                else if (current_Y % 2 != 0)
                {
                    if (target_x < current_X)
                    {
                        move_X--;

                    }
                    else if (target_x > current_X)
                    {
                        move_X++;
                        if (target_y < current_Y)
                        {
                            move_Y--;
                        }
                        if (target_y > current_Y)
                        {
                            move_Y++;
                        }

                    }
                    else if (target_x == current_X)
                    {
                        if (target_y < current_Y)
                        {
                            move_Y--;
                        }
                        if (target_y > current_Y)
                        {
                            move_Y++;
                        }
                    }
                }


                x_Check = 1;
                break;

            case 1:

                //Skrpyt najpierw sprwadza wspolrzedna Y
                if (current_Y % 2 == 0)
                {
                    if (target_y < current_Y)
                    {
                        move_Y--;
                        if (target_x < current_X)
                        {
                            move_X--;
                        }
                        if (target_x > current_X)
                        {
                            move_X++;
                        }
                    }
                    else if (target_y > current_Y)
                    {
                        move_Y++;

                    }
                    else if (target_y == current_Y)
                    {
                        if (target_x < current_X)
                        {
                            move_X--;
                        }
                        if (target_x > current_X)
                        {
                            move_X++;
                        }
                    }
                }
                else if (current_Y % 2 != 0)
                {
                    if (target_y < current_Y)
                    {
                        move_Y--;

                    }
                    else if (target_y > current_Y)
                    {
                        move_Y++;
                        if (target_x < current_X)
                        {
                            move_X--;
                        }
                        if (target_x > current_X)
                        {
                            move_X++;
                        }

                    }
                    else if (target_y == current_Y)
                    {
                        if (target_x < current_X)
                        {
                            move_X--;
                        }
                        if (target_x > current_X)
                        {
                            move_X++;
                        }
                    }
                }
                x_Check = 0;
           break;

        }
            current_X = move_X;
            current_Y = move_Y;
        }


        foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
        {
            if (hex.GetComponent<HexNumber>().hex_x == move_X && hex.GetComponent<HexNumber>().hex_y == move_Y)
            {
                transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y, -2);
                
            }
        }

    }




    void OnGUI()
    {
       
        if (GUI.Button(new Rect(50, 25, 50, 30), "Click"))
        {

            MoveToHex();

        }
    }
}
