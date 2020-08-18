using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class CameraMouseMove : MonoBehaviour
{

    public int Boundary = 15; // distance from edge scrolling starts
   // public int speed = 5;
    private int theScreenWidth;
    private int theScreenHeight;
    private Rigidbody2D rigidbodyComponent;
    public Vector2 speed = new Vector2(15, 15);
    private Vector2 movement;

    public bool MouseMove;
    public float inputX;
    public float inputY;

    public GameObject lastHex; //Informacje o ostatnim hexie
    public int x;
    public int y;
    void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastHex == null)
        {
            foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
            {
                if (hex.GetComponent<HexNumber>().hex_x == x - 1 && hex.GetComponent<HexNumber>().hex_y == y - 1)
                {
                    lastHex = hex;
                }
            }
        }
        //inputX = Input.GetAxis("Horizontal");
        // inputY = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") != 0)
        {
            inputX = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            inputY = Input.GetAxis("Vertical");
        }
        if (MouseMove == true)
        {
         if (Input.mousePosition.x > theScreenWidth - Boundary)
            {
                inputX = 1;
            
                //transform.position.x += speed * Time.deltaTime; // move on +X axis
            }
            if (Input.mousePosition.x < 0 + Boundary)
            {
                inputX = -1;
                // transform.position.x -= speed * Time.deltaTime; // move on -X axis
            }
            if (Input.mousePosition.y > theScreenHeight - Boundary)
            {
                inputY = 1;
                //  transform.position.z += speed * Time.deltaTime; // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + Boundary)
            {
                inputY = -1;
                //transform.position.z -= speed * Time.deltaTime; // move on -Z axis
            }


        }
        if (Input.mousePosition.x < theScreenWidth - Boundary && 
            Input.mousePosition.x > 0 + Boundary && 
            Input.mousePosition.y < theScreenHeight - Boundary && 
            Input.mousePosition.y > 0 + Boundary &&
            Input.GetAxis("Horizontal") == 0 &&
            Input.GetAxis("Vertical") == 0)
        {
            inputX = 0;
            inputY = 0;
        }

        
        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);
    }

    //Wspolrzedne myszki na ekranie
   /* void OnGUI() {
       GUI.Box(new Rect((Screen.width / 2) - 140, 5, 280, 25), "Mouse Position = " + Input.mousePosition);
        GUI.Box(new Rect((Screen.width / 2) - 70, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
        GUI.Box(new Rect(5, (Screen.height / 2) - 12, 140, 25), "Mouse Y = " + Input.mousePosition.y);

    }*/
    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = movement;

        //Blokowanie kamery
        if (Camera.main.transform.position.x <= 5)
        {
            Camera.main.transform.position = new Vector3(5.0f, Camera.main.transform.position.y, -10.0f);
        }
        if (Camera.main.transform.position.y <= 2)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 2.0f, -10.0f);
        }
        if(lastHex != null)
        {

        if (Camera.main.transform.position.x >= lastHex.transform.position.x - 5)
        {
            Camera.main.transform.position = new Vector3(lastHex.transform.position.x - 5.0f, Camera.main.transform.position.y, -10.0f);
        }
        if (Camera.main.transform.position.y >= lastHex.transform.position.y - 2)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, lastHex.transform.position.y - 2.0f, -10.0f);
        }

        }
    }
}
