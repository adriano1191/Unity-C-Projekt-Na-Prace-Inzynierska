using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseSelect : MonoBehaviour {

    public bool selected = false;
    public bool shipSelected = false;
    public Color transparency;
    public Color noTransparency;
    

    private bool clicket = true;

    static public Transform trSelect = null;

    //test
    public GameObject selObj;




    void Start () {
        selObj = GameObject.Find("SelectedObject");
    }


    void Update()
    {
        
        if (shipSelected == true && GetComponent<ShipStats>().shipOwner == 1)
        {

            //transform.position = new Vector3(trSelect.position.x, trSelect.position.y, -2);
            //StartCoroutine(PlayerShipMove());
            if (clicket == true)
            {
                
                clicket = false;
            }
            


        }

        if (selected && transform != trSelect)
        {
            selected = false;
            shipSelected = false;

            if (tag == "HexMap")
            {

                //GetComponentInChildren<SpriteRenderer>().color = noTransparency;
                GetComponent<HexNumber>().hexColor();


            }
            if (tag == "Ship")
            {
                GetComponentInChildren<SpriteRenderer>().color = noTransparency;

            }
        }



        if (selected == true)
        {
            if (tag == "HexMap")
            {
                GetComponent<HexNumber>().hexColor();
                /*GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
                int x;
                x = GetComponent<HexNumber>().hex_x;
                */
                // Debug.Log(x);
            }
            if (tag == "Ship")
            {
                shipSelected = true;
                 GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
                //transform.position = trSelect.transform.position;

            }


            
        }

        else
        {

        }



    }

    void OnMouseDown()
    {


        selected = true;
            trSelect = transform;
        if(shipSelected == true)
        {
            clicket = true;
        }
        //clicket = !clicket;
        selObj.GetComponent<whatIsSelected>().ChangeObject(transform);

        if (ShipGui.abilityActive == true)
        {
            selObj.GetComponent<whatIsSelected>().lastSelected.GetComponent<PlayerMoveAbility>().MovePlayerShip();//wykonanie ruchu

        }
        if (ShipGui.abilityAtackActive == true)
        {
            selObj.GetComponent<whatIsSelected>().lastSelected.GetComponent<PlayerShipAttack>().AttackPlayerShip();//wykonanie ataku

        }

        

    }




}
