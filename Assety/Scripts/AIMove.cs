using UnityEngine;
using System.Collections;

public class AIMove : MonoBehaviour {

    
    public int targetPos_x;
    public int targetPos_y;

    public int movePos_x;
    public int movePos_y;

    public int fixedMovePos_x;
    public int fixedMovePos_y;

    public int currentPos_x;
    public int currentPos_y;

    public int moveRange;
    public int maxMovePoint;
    public int currentMovePoint;



    public ShipStats sStats;
    public GameObject moveHex;
    public GameObject fixedMoveHex;
    public GameObject lastMoveHex;

    private bool moveIsPossible = true;
    private bool canMove = true;

    public int i = 0;

    public float colliderMoveRange; // 0.1 / 0.8 / 2.0 / 3.3

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "HexMap")
        {
            GameObject hex = other.gameObject;
            //currentPos_x = hex.GetComponent<HexNumber>().hex_x;
           // currentPos_y = hex.GetComponent<HexNumber>().hex_y;
        }


    }
    void OnGUI()
    {

        if (GUI.Button(new Rect(50, 25, 50, 30), "Click"))
        {


            StartCoroutine(RangeAndMove());




        }
    }



    IEnumerator RangeAndMove()
    {
        /* //Dynamiczny zasieg ruchu
            if (moveRange == 0)
            {
                gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
            }
            else if (moveRange == 1)
            {
                gameObject.GetComponent<CircleCollider2D>().radius = 0.8f;
            }
            else if (moveRange == 2)
            {
                gameObject.GetComponent<CircleCollider2D>().radius = 2.0f;
            }
            else if (moveRange >= 3)
            {
                gameObject.GetComponent<CircleCollider2D>().radius = 3.3f;
            }
            */
        movePos_x = currentPos_x;
        movePos_y = currentPos_y;
        canMove = true;
        moveIsPossible = true;
        while (canMove == true && currentMovePoint > 0 && moveIsPossible == true)
        {

            canMove = true;
            gameObject.GetComponent<CircleCollider2D>().radius = 0.8f;
        yield return new WaitForSeconds(0.5f);
        FindMove();

            Move();

        }
        gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
    }

    void Move()
    {




        foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
        {
            if (hex.GetComponent<HexNumber>().hex_x == movePos_x && hex.GetComponent<HexNumber>().hex_y == movePos_y)
            {
                moveHex = hex;
                Debug.Log("Hex znaleziony" + moveHex.GetComponent<HexNumber>().hex_x + " " + moveHex.GetComponent<HexNumber>().hex_y);
                //jezeli cel ostateczny jest taki sam jak obecny cel to nic nie rob
                if(moveHex.GetComponent<HexNumber>().occupied == true && movePos_x == targetPos_x && movePos_y == targetPos_y)
                {
                    Debug.Log("Cel: " + movePos_x + " " + movePos_y + " jest okupowany i znajduje sie obok");
                    canMove = false;
                }
                else if (hex != lastMoveHex)
                {



                    if (moveHex.GetComponent<HexNumber>().occupied == true && (moveHex.GetComponent<HexNumber>().hex_x != currentPos_x || moveHex.GetComponent<HexNumber>().hex_y != currentPos_y))
                {
                    //i = 0;
                        int j = 0;

                    Debug.Log("Hex uccupied");
                    do
                    {
                        Debug.Log("Do While numer: " + i);
                        fixedMoveHex = moveHex;
                        fixedMovePos_x = movePos_x;
                        fixedMovePos_y = movePos_y;
                        Debug.Log("FixedHex");
                           
                            switch (i)
                        {
                            case 0:
                                Debug.Log("Case 0");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_y++;
                                }
                                else
                                {
                                    fixedMovePos_x++;
                                    fixedMovePos_y++;
                                }
                                    i = 1;            
                                break;

                            case 1:
                                Debug.Log("Case 1");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_x++;
                                }
                                else
                                {
                                    fixedMovePos_x++;
                                }
                                    i = 2;
                                break;

                            case 2:
                                Debug.Log("Case 2");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_y--;
                                }
                                else
                                {
                                    fixedMovePos_x++;
                                    fixedMovePos_y--;
                                }
                                    i = 3;
                                break;

                            case 3:
                                Debug.Log("Case 3");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_x--;
                                    fixedMovePos_y--;
                                }
                                else
                                {
                                    fixedMovePos_y--;
                                }
                                    i = 4;
                                break;

                            case 4:
                                Debug.Log("Case 4");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_x--;
                                }
                                else
                                {
                                    fixedMovePos_x--;
                                }
                                    i = 5;
                                break;

                            case 5:
                                Debug.Log("Case 5");
                                if (moveHex.GetComponent<HexNumber>().hex_y % 2 == 0)
                                {
                                    fixedMovePos_x--;
                                    fixedMovePos_y++;
                                }
                                else
                                {
                                    fixedMovePos_y++;
                                }
                                    i = 0;
                                break;

                        }
                        Debug.Log(j);
                        j++;
                        Debug.Log(j);
                            Debug.Log("I SIE KURWA ROWNA " + i);

                            foreach (GameObject fixHex in GameObject.FindGameObjectsWithTag("HexMap"))
                        {
                            

                            if (fixHex.GetComponent<HexNumber>().hex_x == fixedMovePos_x && fixHex.GetComponent<HexNumber>().hex_y == fixedMovePos_y && fixHex.GetComponent<HexNumber>().inRange == true && fixHex.GetComponent<HexNumber>().occupied != true)
                            {
                                Debug.Log("fixedMoveHex znaleziony");
                                fixedMoveHex = fixHex;
                                Debug.Log("fixedMoveHex znaleziony" + fixedMoveHex.GetComponent<HexNumber>().hex_x + " " + fixedMoveHex.GetComponent<HexNumber>().hex_y);
                                moveIsPossible = true;
                            }
                            else if( j > 5)
                            {
                                moveIsPossible = false;
                             }
                            
                        }
                            if (fixedMovePos_x < 0)
                            {
                                fixedMovePos_x = 0;
                            }
                            if (fixedMovePos_y < 0)
                            {
                                fixedMovePos_y = 0;
                            }
                        } while (fixedMoveHex.GetComponent<HexNumber>().occupied == true && j < 6);
                    if(moveIsPossible == true)
                    {
                        if(fixedMoveHex != lastMoveHex)
                            {

                                transform.position = new Vector3(fixedMoveHex.transform.position.x, fixedMoveHex.transform.position.y, -2);
                                lastMoveHex = fixedMoveHex;
                                Debug.Log("Ruch Wykonany");
                                currentMovePoint--; //odejmij punkt purchu
                                movePos_x = fixedMovePos_x;
                                movePos_y = fixedMovePos_y;


                            }
                        }
                    else
                    {
                        Debug.Log("Nie mozna wykonac ruchu");
                            moveIsPossible = false;
                        }
                    
                }
                else //jezeli move nie jest occupied
                {

                    transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y, -2);
                    lastMoveHex = hex;
                    currentMovePoint--; //odejmij punkt purchu

                    }
                }

            }
            
        }
        //gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
        //moveIsActive = false;
    }

    void FindMove()
    { 
        if(currentPos_y % 2 == 0)
        {
            if(currentPos_y < targetPos_y)
            {
                if(currentPos_x < targetPos_x)
                {
                    movePos_y++;
                    i = 0;
                }
                else if (currentPos_x == targetPos_x)
                {
                    movePos_y++;
                    i = 5;
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_x--;
                    movePos_y++;
                    i = 4;
                }
            }
            else if (currentPos_y == targetPos_y)
            {
                if (currentPos_x < targetPos_x)
                {
                    movePos_x++;
                    i = 0;
                }
                else if (currentPos_x == targetPos_x)
                {
                    //nic nie rob
                    i = 0;
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_x--;
                    i = 3;
                }
            }
            else if (currentPos_y > targetPos_y)
            {
                if (currentPos_x < targetPos_x)
                {
                    movePos_y--;
                    i = 1;
                }
                else if (currentPos_x == targetPos_x)
                {
                    movePos_y--;
                    i = 1;
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_x--;
                    movePos_y--;
                    i = 2;
                }
            }
        }
        else
        {
            if (currentPos_y < targetPos_y)
            {
                if (currentPos_x < targetPos_x)
                {
                    movePos_x++;
                    movePos_y++;
                    i = 5;
                }
                else if (currentPos_x == targetPos_x)
                {
                    movePos_y++;
                    i = 2; 
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_y++;
                    i = 4;
                }
            }
            else if (currentPos_y == targetPos_y)
            {
                if (currentPos_x < targetPos_x)
                {
                    movePos_x++;
                    i = 0;
                }
                else if (currentPos_x == targetPos_x)
                {
                    //jestesmy u celu
                    i = 0;
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_x--;
                    i = 3;
                }
            }
            else if (currentPos_y > targetPos_y)
            {
                if (currentPos_x < targetPos_x)
                {
                    movePos_x++;
                    movePos_y--; 
                    i = 1;
                }
                else if (currentPos_x == targetPos_x)
                {
                    movePos_y--;
                    i = 2;
                }
                else if (currentPos_x > targetPos_x)
                {
                    movePos_y--;
                    i = 2;
                }
            }
        }
        Debug.Log("Find Move: " + movePos_x + movePos_y);
        if (movePos_x < 0)
        {
            movePos_x = 0; Debug.Log("movePos X mial wartosc ujemna " + movePos_x + movePos_y);
        }
        if (movePos_y < 0)
        {
            movePos_y = 0; Debug.Log("movePos Y mial wartosc ujemna " + movePos_x + movePos_y);
        }
        if(movePos_x > (HexGrid.x - 1))
        {
            movePos_x = (HexGrid.x - 1); Debug.Log("movePos X byl z poza zakresu " + movePos_x + movePos_y);
        }
        if(movePos_y > (HexGrid.y - 1))
        {
            movePos_y = (HexGrid.y - 1); Debug.Log("movePos Y byl z poza zakresu " + movePos_x + movePos_y);
        }



    }

	void Start () {
        sStats = gameObject.GetComponent<ShipStats>();
        //Debug.Log(sStats.shipName);

	}
	

	void Update ()
    {

        //else
        //{
          //  gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
        //}
    }
}
