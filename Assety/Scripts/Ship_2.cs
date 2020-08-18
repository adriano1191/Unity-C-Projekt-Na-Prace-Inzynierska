using UnityEngine;
using System.Collections;

public class Ship_2 : MonoBehaviour {

    public int owner; //Kto jest wlascicielem planety domyslnie 1 == gracz
    private bool skill1 = false;
#pragma warning disable CS0414 // The field 'Ship_2.skill2' is assigned but its value is never used
    private bool skill2 = false;
#pragma warning restore CS0414 // The field 'Ship_2.skill2' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Ship_2.skill3' is assigned but its value is never used
    private bool skill3 = false;
#pragma warning restore CS0414 // The field 'Ship_2.skill3' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Ship_2.skill4' is assigned but its value is never used
    private bool skill4 = false;
#pragma warning restore CS0414 // The field 'Ship_2.skill4' is assigned but its value is never used

    private bool skillUsed = false;



    void PlanetOccupation(RaycastHit2D sHit)
    {


        if (sHit.collider.gameObject.GetComponent<PlanetEarth>())    //czy collider posiada skrypt PlanetEarth
        {
            if (sHit.collider.gameObject.GetComponent<HexNumber>().inRange == true) //czy obiekt jest w zasiegu
            {
                if(sHit.collider.gameObject.GetComponent<PlanetEarth>().owner != owner)
                {

                    sHit.collider.gameObject.GetComponent<PlanetEarth>().owner = owner;      //jezeli tak to ustaw wartosc owenra
                    Debug.Log("Target Position: " + sHit.collider.gameObject.transform.position);
                    
                }

            }
        }
        else
        {
            //Debug.Log("To nie Ziemia");
        }
        skill1 = false;
        skillUsed = false;
        gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
    }



    void Update()
    {
        if (skillUsed == true)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                //Pobranie danych klikanego obiektu
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                Debug.Log("Nazwa: " + hit.collider.gameObject.transform.name);
                
                if (hit.collider != null)   //sprawdzanie istnienia collidera
                {
                    if(skill1 == true)
                    {
                        PlanetOccupation(hit);
                    }
                }

            }
        }
    }

    void OnMouseDown()
    {
       
    }

    //sprawdzanie zasiegu
    void OnTriggerEnter2D(Collider2D collid)
    {
        if(collid.tag == "HexMap")
        {

        collid.gameObject.GetComponent<HexNumber>().inRange = true;

        }
    }

    void OnTriggerExit2D(Collider2D collid)
    {
        if (collid.tag == "HexMap")
        {

            collid.gameObject.GetComponent<HexNumber>().inRange = false;

        }
    }


    void OnGUI()
    {


        if (gameObject.GetComponent<MouseSelect>().selected == true)
        {


            if (owner == 1)
            {

                //Mozliwosci planety
                GUI.Box(new Rect((Screen.width) - 200, (Screen.height) - 100, 200, 100), "Build");
                //Pierwsza linia
                if (skill1 == false && GUI.Button(new Rect((Screen.width) - 180, (Screen.height) - 80, 35, 35), "Base"))
                {

                    gameObject.GetComponent<MouseSelect>().selected = false;
                    gameObject.GetComponent<MouseSelect>().shipSelected= false;

                    gameObject.GetComponent<CircleCollider2D>().radius = 0.7f;
                    






                    skill1 = true;
                    skillUsed = true;




                }
                if (skill1 == true)
                {
                    GUI.Box(new Rect((Screen.width) - 180, (Screen.height) - 80, 35, 35), "Builded");
                }

                if (GUI.Button(new Rect((Screen.width) - 140, (Screen.height) - 80, 35, 35), "Build"))
                {

                }
                if (GUI.Button(new Rect((Screen.width) - 100, (Screen.height) - 80, 35, 35), "Build"))
                {

                }
                if (GUI.Button(new Rect((Screen.width) - 60, (Screen.height) - 80, 35, 35), "Build"))
                {

                }

                //Druga linia
                if (GUI.Button(new Rect((Screen.width) - 180, (Screen.height) - 40, 35, 35), "Build"))
                {

                }
                if (GUI.Button(new Rect((Screen.width) - 140, (Screen.height) - 40, 35, 35), "Build"))
                {

                }
                if (GUI.Button(new Rect((Screen.width) - 100, (Screen.height) - 40, 35, 35), "Build"))
                {

                }
                if (GUI.Button(new Rect((Screen.width) - 60, (Screen.height) - 40, 35, 35), "Build"))
                {

                }

            }

        }

    }

}
