using UnityEngine;
using System.Collections;

public class OccupationPlanet : MonoBehaviour {

    private int owner;
    public bool canOccupation = false;
    public GameObject planet;
	// Use this for initialization
	void Start () {
        owner = GetComponent<ShipStats>().shipOwner;
	}
	

   public void Occupation()
    {
        
        if (canOccupation == true)
        {
            
            if (planet.GetComponent<PlanetEarth>().owner == 0)
            {

                planet.GetComponent<PlanetEarth>().owner = owner;
                 PlayerInfo.numberOfPlanet += 1;
                planet.GetComponent<HexNumber>().occupied = false;
                planet.GetComponent<HexNumber>().inRange = false;
                planet.GetComponent<HexNumber>().hexColor();
                Destroy(gameObject);
            }
        }
    }

    public void AiOccupation(int AiOwner, int tar_x, int tar_y)
    {

        if (canOccupation == true)
        {

            if (planet.GetComponent<PlanetEarth>().owner == 0)
            {
                if(GetComponent<ShipStats>().pos_X == tar_x && GetComponent<ShipStats>().pos_Y == tar_y)
                {
                    foreach (GameObject ai in GameObject.FindGameObjectsWithTag("Ai"))
                    {
                        if (ai.GetComponent<ComputerAI>().player == AiOwner)
                        {
                            ai.GetComponent<ComputerAI>().numberOfPlanet += 1;
                            ai.GetComponent<ComputerAI>().numberOfShipId2 -= 1;
                        }
                    }

                    planet.GetComponent<PlanetEarth>().owner = AiOwner;
                    planet.GetComponent<HexNumber>().occupied = false;
                    planet.GetComponent<HexNumber>().inRange = false;
                    planet.GetComponent<HexNumber>().hexColor();
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collid)
    {
        if (collid.tag == "HexMap" )
        {
            
            if (collid.gameObject.GetComponent<PlanetEarth>() )
            {
                
                canOccupation = true;
                    planet = collid.gameObject;
            }
            else
            {
                canOccupation = false;
                planet = null;
            }

        }
        else
        {
            canOccupation = false;
            planet = null;
        }

    }

    void OnTriggerExit2D(Collider2D collid)
    {
       // canOccupation = false;
       // planet = null;
    }
    }
