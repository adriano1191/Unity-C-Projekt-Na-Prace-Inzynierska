using UnityEngine;
using System.Collections;

public class PlayerMoveAbility : MonoBehaviour {

    private Transform current;
    private Transform last;
    private GameObject selectedObj;
    public GameObject animationShip;
    private float distance;
    void Start()
    {
        selectedObj = GameObject.Find("SelectedObject");
    }

    public void MovePlayerShip()
    {
        Debug.Log("MovePlayerShip wykonywanie");
        current = selectedObj.GetComponent<whatIsSelected>().selectedObject;
        last = selectedObj.GetComponent<whatIsSelected>().lastSelected;

        


        StartCoroutine(PlayerShipMove());
    }

    IEnumerator PlayerShipMove()
    {

        if(current.tag == "HexMap")
        {

            if (current.GetComponent<HexNumber>().inRange == true && current.GetComponent<HexNumber>().occupied != true)
            {
                ShipGui.abilityActive = false;//Dezaktywacja skilla ruchu
                distance = selectedObj.GetComponent<whatIsSelected>().distance;
                animationShip.GetComponent<AnimationShip>().StartTeleport(last.position);
                yield return new WaitForSeconds(0.8f);
                animationShip.GetComponent<AnimationShip>().EndtTeleport(current.position);
                last.transform.position = new Vector3(current.position.x, current.position.y, -2);

                //Odejmowanie punktow ruchu

                ShipStats shipstats = GetComponent<ShipStats>();

                if (distance > 0 && distance <= 1.1)
                {
                    shipstats.movePoints -= 1;
                }
                else if (distance > 1.1 && distance <= 2)
                {
                    shipstats.movePoints -= 2;
                }
                else if (distance > 2.1 && distance <= 3.1)
                {
                    shipstats.movePoints -= 3;
                }

            }

        }
           else
           {
            Debug.Log("Obiekt poza zasiegiem");
           }
        last.GetComponentInChildren<ShipRange>().SeeRange(0); //wyłaczenie pokazywania zasiegu
        ShipGui.abilityActive = false;//Dezaktywacja skilla ruchu

    }

    void Update()
    {
        if(PlayerInfo.playerTurn != 1 && ShipGui.abilityActive == true)
        {
            ShipGui.abilityActive = false;
            GetComponentInChildren<ShipRange>().SeeRange(0);
        }
    }
}
