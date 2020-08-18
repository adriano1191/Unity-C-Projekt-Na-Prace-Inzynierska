using UnityEngine;
using System.Collections;
//
//Nie używany skrypt do sterowania kamera
//
[RequireComponent(typeof(Rigidbody2D))]

public class CameraMove : MonoBehaviour {
    public Vector2 speed = new Vector2(50, 50);
    private Vector2 movement;
    public Vector2 maxMove;
    private Rigidbody2D rigidbodyComponent;

    public int Boundary = 50; // distance from edge scrolling starts
                              // public int speed = 5;
#pragma warning disable CS0649 // Field 'CameraMove.theScreenWidth' is never assigned to, and will always have its default value 0
    private int theScreenWidth;
#pragma warning restore CS0649 // Field 'CameraMove.theScreenWidth' is never assigned to, and will always have its default value 0
#pragma warning disable CS0649 // Field 'CameraMove.theScreenHeight' is never assigned to, and will always have its default value 0
    private int theScreenHeight;
#pragma warning restore CS0649 // Field 'CameraMove.theScreenHeight' is never assigned to, and will always have its default value 0

    public float inputX;
    public float inputY;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

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
        if(Input.mousePosition.x < theScreenWidth - Boundary && Input.mousePosition.x > 0 + Boundary && Input.mousePosition.y < theScreenHeight - Boundary && Input.mousePosition.y > 0 + Boundary)
        {
            inputX = 0;
            inputY = 0;
        }

        // 4 - Movement per direction
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;

    }
}
