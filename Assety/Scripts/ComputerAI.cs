using UnityEngine;
using System.Collections;

public class ComputerAI : MonoBehaviour
{

    public int player;   //0 - neutral; 1 - player; 2++ - AI
    public int numberOfPlanet;
    public int numberOfShips;
    public int numberOfShipId2;

    private bool canLoose = false;

    public int target_x;
    public int target_y;

    public int planetTarget_x;
    public int planetTarget_y;

    public int planetConquer_x;
    public int planetConquer_y;

    public GameObject target;
    public GameObject planetTarget;
    private GameObject playerInfo;
    public GameObject lastHex;
    public GameObject planetConquer;

    public GameObject startShip;

    private int switchId = 0;

    void Start()
    {

        playerInfo = GameObject.FindGameObjectWithTag("Player");
        if (lastHex == null)
        {

            foreach (GameObject hex in GameObject.FindGameObjectsWithTag("HexMap"))
            {
                if (player == 2)
                {
                    
                    if (hex.GetComponent<HexNumber>().hex_x == HexGrid.x - 1 && hex.GetComponent<HexNumber>().hex_y == HexGrid.y - 1)
                    {
                        Debug.Log("Player ID: " + player + " 1");
                        lastHex = hex;
                    }
                }
                else if (player == 3)
                {

                    if (hex.GetComponent<HexNumber>().hex_x == HexGrid.x - 1 && hex.GetComponent<HexNumber>().hex_y == 0)
                    {
                        Debug.Log("Player ID: " + player + " 2");
                        lastHex = hex;
                    }
                }
                else if (player == 4)
                {

                    if (hex.GetComponent<HexNumber>().hex_x == 0 && hex.GetComponent<HexNumber>().hex_y == HexGrid.y - 1)
                    {
                        Debug.Log("Player ID: " + player + " 3");
                        lastHex = hex;
                    }
                }
            }
        }
        if (lastHex)
        {

            transform.position = lastHex.transform.position;
            (Instantiate(startShip, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation) as GameObject).GetComponent<ShipStats>().shipOwner = player;
        }
    }

    void Update()
    {
        if(numberOfPlanet > 0)
        {
            canLoose = true;
        }
        if (canLoose == true && numberOfPlanet == 0)
        {
            foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
            {
                if(ship.GetComponent<ShipStats>().shipOwner == player)
                {
                    ship.GetComponent<ShipStats>().Death();
                }
            }
            canLoose = false;
            MapInfo.playerInGame -= 1;
        }

        
    }

    public void TurnOn()
    {

        //player = PlayerInfo.playerTurn; //Kogo jest obecnie tura

        UnitRenevMovePoint(); //Odnawianie punktow ruchu
        LookingForPlanet(); //Szukaj planety
        FindPlanetToConquer(); //Szukanie planety do podbicia

        FindEnemy();// Szukaj targetu
        AiBuyShips();

    }


    void LookingForPlanet()
    {
        if (planetTarget == null || planetTarget.GetComponent<PlanetEarth>().owner == player)
        {
            planetTarget = null;

            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("HexMap"))
            {
                if (planet.gameObject.GetComponent<PlanetEarth>())
                {
                    if (planet.gameObject.GetComponent<PlanetEarth>().owner == 0)
                    {
                        if (planetTarget == null || planetTarget.GetComponent<PlanetEarth>().owner == player)
                        {
                            planetTarget = planet;
                            planetTarget_x = planetTarget.GetComponent<HexNumber>().hex_x;
                            planetTarget_y = planetTarget.GetComponent<HexNumber>().hex_y;
                        }
                        else
                        {
                            if (Vector2.Distance(transform.position, planet.transform.position) <= Vector2.Distance(transform.position, planetTarget.transform.position) && planetTarget.GetComponent<PlanetEarth>().owner == 0)
                            {
                                planetTarget = planet;
                                planetTarget_x = planetTarget.GetComponent<HexNumber>().hex_x;
                                planetTarget_y = planetTarget.GetComponent<HexNumber>().hex_y;
                            }
                        }
                    }
                }
            }
        }
    }

    void UnitRenevMovePoint()
    {
        
        foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
        {
            if (ship)
            {

                if (ship.gameObject.GetComponent<ShipStats>().shipOwner == player)
                {
                    ship.GetComponent<ShipStats>().movePoints = 2;
                }
            }
        }
    }

    IEnumerator ShipMove()
    {
        Debug.Log(player + " ShipMove");
        int sCase = 0;
        if(target == null)
        {
            target_x = planetConquer_x;
            target_y = planetConquer_y;
        }

            foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
            {
                if (ship)
                {

                

                if (ship.gameObject.GetComponent<ShipStats>().shipOwner == player && ship.gameObject.GetComponent<ShipStats>().movePoints > 0)
                {
                    if (ship.gameObject.GetComponent<ShipStats>().shipID == 0 || ship.gameObject.GetComponent<ShipStats>().shipID == 1)
                    {
                        if (ship.gameObject.GetComponent<AiMoveJump>().targetPos_x != target_x || ship.gameObject.GetComponent<AiMoveJump>().targetPos_y != target_y)
                        {
                            if (ship.gameObject.GetComponent<AiMoveJump>().targetPos_x != planetConquer_x || ship.gameObject.GetComponent<AiMoveJump>().targetPos_y != planetConquer_y)
                            {
                                switch (sCase)
                                {
                                    case 0:
                                        if (target != null)
                                        {

                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_x = target_x;
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_y = target_y;
                                        }
                                        else
                                        {
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_x = planetConquer_x;
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_y = planetConquer_y;
                                        }
                                        sCase = 1;
                                        break;

                                    case 1:
                                        if (planetConquer != null)
                                        {
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_x = planetConquer_x;
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_y = planetConquer_y;
                                        }
                                        else
                                        {
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_x = target_x;
                                            ship.gameObject.GetComponent<AiMoveJump>().targetPos_y = target_y;
                                        }
                                        sCase = 0;
                                        break;
                                }

                            }
                            else
                            {

                            }
                        }

                            ship.gameObject.GetComponent<AiMoveJump>().Tour();
                            yield return new WaitForSeconds(0.6f);

                    }
                    else if (ship.gameObject.GetComponent<ShipStats>().shipID == 2)
                    {
                        ship.gameObject.GetComponent<AiMoveJump>().targetPos_x = planetTarget_x;
                        ship.gameObject.GetComponent<AiMoveJump>().targetPos_y = planetTarget_y;

                        ship.gameObject.GetComponent<AiMoveJump>().Tour();
                        yield return new WaitForSeconds(0.6f);
                    if (ship)
                    {
                        if (ship.gameObject.GetComponent<ShipStats>().movePoints > 0)
                        {

                            ship.gameObject.GetComponent<OccupationPlanet>().AiOccupation(player, planetTarget_x, planetTarget_y);
                        }
                    }
                    }
                }
            }
        }

        Debug.Log(player + " WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj WYkonaj ");
        playerInfo.GetComponent<PlayerInfo>().NextTurn();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ShipAttack()
    {
        Debug.Log(player + " ShipAttack");
        foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
        {
            if (ship)
            {

                if (ship == null)
                {

                    // break;
                }
                if (ship.gameObject.GetComponent<ShipStats>().shipOwner == player && ship.gameObject.GetComponent<ShipStats>().movePoints > 0)
                {
                    if (ship.gameObject.GetComponent<ShipStats>().shipID == 0 || ship.gameObject.GetComponent<ShipStats>().shipID == 1)
                    {

                        ship.gameObject.GetComponent<AiShipAttack>().AttackPlayerShip();
                        yield return new WaitForSeconds(0.6f);
                        //yield return null;

                    }
                }
        }
        }
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(ShipMove()); //RUCH

    }

    void FindEnemy()
    {

        foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
        {
            if (ship)
            {


                Debug.Log("Test A");

                if (ship.gameObject.GetComponent<ShipStats>().shipOwner != player)
                {
                    Debug.Log("Test C");
                    target = ship;
                    target_x = target.GetComponent<ShipStats>().pos_X;
                    target_y = target.GetComponent<ShipStats>().pos_Y;
                    break;
                }
            }
        }
        /*if (target != null)
        {
            Debug.Log("Test D"); 
            StartCoroutine(ShipAttack()); //Atak
        }
       else if (target == null)
        {
            Debug.Log("Test E");
            // playerInfo.GetComponent<PlayerInfo>().NextTurn();
            StartCoroutine(ShipAttack());
        }*/
        StartCoroutine(ShipAttack());
    }

    void FindPlanetToConquer()
    {
        if (planetConquer == null || planetConquer.GetComponent<PlanetEarth>().owner == player)
        {
            planetConquer = null;

            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("HexMap"))
            {
                if (planet.gameObject.GetComponent<PlanetEarth>())
                {
                    if (planet.gameObject.GetComponent<PlanetEarth>().owner != player && planet.gameObject.GetComponent<PlanetEarth>().owner > 0)
                    {
                        if (planetConquer == null || planetConquer.GetComponent<PlanetEarth>().owner == player)
                        {
                            planetConquer = planet;
                            planetConquer_x = planetConquer.GetComponent<HexNumber>().hex_x;
                            planetConquer_y = planetConquer.GetComponent<HexNumber>().hex_y;
                        }
                        else
                        {
                            if (Vector2.Distance(transform.position, planet.transform.position) <= Vector2.Distance(transform.position, planetConquer.transform.position) && planetConquer.GetComponent<PlanetEarth>().owner != player)
                            {
                                planetConquer = planet;
                                planetConquer_x = planetConquer.GetComponent<HexNumber>().hex_x;
                                planetConquer_y = planetConquer.GetComponent<HexNumber>().hex_y;
                            }
                        }


                    }
                }
            }
        }
        
    }

    public void AiBuyShips()
    {
        
        if (numberOfShipId2 < 1 && numberOfPlanet > 0)
        {
            int los = Random.Range(0, 4);
            if (los == 1)
            {
                Debug.Log("Tworze statek do zajmowania planet");
                foreach (GameObject ownPlanet in GameObject.FindGameObjectsWithTag("HexMap"))
                {
                    if (ownPlanet.gameObject.GetComponent<PlanetEarth>() && ownPlanet.gameObject.GetComponent<PlanetEarth>().owner == player && ownPlanet.gameObject.GetComponent<HexNumber>().occupied == false)
                    {
                        ownPlanet.gameObject.GetComponent<PlanetEarth>().ShipBuy(2);
                        break;
                    }
                }
            }
        }
        int planet = numberOfPlanet * 1 + 4;

        if(numberOfShips < planet && numberOfPlanet > 0)
        {
            foreach (GameObject ownPlanet in GameObject.FindGameObjectsWithTag("HexMap"))
            {
                int los = Random.Range(0, 100);
                if (los >= 0 && los <= 25  && numberOfShips < planet && numberOfPlanet > 0)
                {
                    if (ownPlanet.gameObject.GetComponent<PlanetEarth>() && ownPlanet.gameObject.GetComponent<PlanetEarth>().owner == player && ownPlanet.gameObject.GetComponent<HexNumber>().occupied == false)
                    {
                        switch (switchId)
                        {
                            case 0:
                                ownPlanet.gameObject.GetComponent<PlanetEarth>().ShipBuy(0);
                                switchId = 1;
                                break;
                            case 1:
                                ownPlanet.gameObject.GetComponent<PlanetEarth>().ShipBuy(0);
                                switchId = 2;
                                break;
                            case 2:
                                ownPlanet.gameObject.GetComponent<PlanetEarth>().ShipBuy(1);
                                switchId = 0;
                                break;
                        }
                        
                        
                    }
                }
            }
        }
    }


   
}
