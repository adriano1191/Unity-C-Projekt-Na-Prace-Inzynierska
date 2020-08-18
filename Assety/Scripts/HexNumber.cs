using UnityEngine;
using System.Collections;

public class HexNumber : MonoBehaviour {


    public int hex_x;
    public int hex_y;
    public bool inRange = false;
    public Color noTransparency;

    public bool occupied = false;

    public GameObject collidObj;


    // Use this for initialization
    void Start () {
        hexColor();
    }
    /*void FixedUpdate()
    {
         if(collidObj != null)
         {
             if (collidObj.GetComponent<ShipStats>().shipOwner == 1)
             {
                 GetComponentInChildren<SpriteRenderer>().color = new Color32(50, 250, 10, 255); //Green
             }
             else if (collidObj.GetComponent<ShipStats>().shipOwner > 1)
             {
                 GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 0, 0, 255); //Red

             }
         }
         else
         {
             GetComponentInChildren<SpriteRenderer>().color = noTransparency;
             if (inRange == true)
             {
                 GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 255); //Yellow
             }
             else
             {
                 GetComponentInChildren<SpriteRenderer>().color = noTransparency;
             }
         }

         if(collidObj == null)
         {
             occupied = false;
         }
    }*/

    // Update is called once per frame
   /* void Update () {
	if (inRange == true)
        {
            //GetComponentInChildren<SpriteRenderer>().color = new Color32(50, 250, 10, 255); //Green
        }
        else
        {
            //GetComponentInChildren<SpriteRenderer>().color = noTransparency;
        }
	}*/
    
    void OnTriggerEnter2D(Collider2D collid)
    {
        if (collid.tag == "Ship" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = true;

        }
        if (collid.tag == "Ship" && collid.GetType() == typeof(BoxCollider2D))
        {
            collidObj = collid.gameObject;
            occupied = true;
            collid.gameObject.GetComponent<AiMoveJump>().currentPos_x = hex_x;
            collid.gameObject.GetComponent<AiMoveJump>().currentPos_y = hex_y;

        }


        if (collid.tag == "ShipRange" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = true;

        }
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(BoxCollider2D))
        {

            occupied = true;
            collid.gameObject.GetComponent<AiMoveJump>().currentPos_x = hex_x;
            collid.gameObject.GetComponent<AiMoveJump>().currentPos_y = hex_y;

        }

        hexColor();
    }

    void OnTriggerExit2D(Collider2D collid)
    {
        if (collid.tag == "Ship" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = false;
        }
            if (collid.tag == "Ship" && collid.GetType() == typeof(BoxCollider2D))
        {
            collidObj = null;
            occupied = false;

        }
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = false;
        }
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(BoxCollider2D))
        {

            occupied = false;

        }
        hexColor();
    }
    
    void OnTriggerEnter2DChild(Collider2D collid)
    {
        if (collid.tag == "Ship" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = true;

        }
        hexColor();
    }
    void OnTriggerExit2DChild(Collider2D collid)
    {
        if (collid.tag == "Ship" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = false;
        }
        hexColor();
    }
    
    public void hexColor()
    {
        if(GetComponent<MouseSelect>().selected == true)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 255); //Yellow
        }
        else if(inRange == true && occupied == false)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 255); //Yellow
        }
        else if(occupied == true)
        {

                if (collidObj.GetComponent<ShipStats>().shipOwner == 1)
                {
                    GetComponentInChildren<SpriteRenderer>().color = new Color32(50, 250, 10, 255); //Green
                }
                else if (collidObj.GetComponent<ShipStats>().shipOwner == 2)
                {
                    GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 0, 0, 255); //Red

                }
                else if (collidObj.GetComponent<ShipStats>().shipOwner == 3)
                {
                    GetComponentInChildren<SpriteRenderer>().color = new Color32(200, 0, 200, 255); //purple

                }
                else if (collidObj.GetComponent<ShipStats>().shipOwner == 4)
                {
                    GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 30, 255, 255); //blue

                }


        }
        else if (GetComponent<PlanetEarth>())
        {
            if(GetComponent<PlanetEarth>().owner == 0)
            {
                GetComponentInChildren<SpriteRenderer>().color = noTransparency;
            }
            else if(GetComponent<PlanetEarth>().owner == 1)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(50, 250, 10, 255); //Green
            }
            else if (GetComponent<PlanetEarth>().owner == 2)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 0, 0, 255); //Red
            }
            else if (GetComponent<PlanetEarth>().owner == 3)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(200, 0, 200, 255); //purple
            }
            else if (GetComponent<PlanetEarth>().owner == 4)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 30, 255, 255); //blue
            }
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = noTransparency;
        }
    }
    
    }




