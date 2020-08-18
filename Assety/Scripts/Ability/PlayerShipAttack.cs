using UnityEngine;
using System.Collections;

public class PlayerShipAttack : MonoBehaviour {

    private Transform current;
    private Transform last;
    private GameObject selectedObj;
    public GameObject animationShip;
    private float distance;

    private int dmg;


    void Start()
    {
        selectedObj = GameObject.Find("SelectedObject");
    }

    public void AttackPlayerShip()
    {
        Debug.Log("Atak wykonywanie");
        current = selectedObj.GetComponent<whatIsSelected>().selectedObject;
        last = selectedObj.GetComponent<whatIsSelected>().lastSelected;




        StartCoroutine(ShipAttack());
    }

    IEnumerator ShipAttack()
    {
        Debug.Log("ShipAttack wykonywanie");
        if (current.tag == "Ship")
        {
            Debug.Log(" if (current.tag == Ship)");
            if (current.GetComponentInChildren<ShipRange>().inRange == true)
            {
                ShipGui.abilityAtackActive = false;
                Debug.Log("current.GetComponentInChildren<ShipRange>().inRange == true");
                ShipStats shipstats = GetComponent<ShipStats>();

                dmg = Random.Range(shipstats.GetComponent<ShipStats>().dmg, shipstats.GetComponent<ShipStats>().maxDmg);
               // dmg = shipstats.GetComponent<ShipStats>().dmg;
               // animationShip.GetComponent<AnimationShip>().StartTeleport(last.position); Animacja
                yield return new WaitForSeconds(0.8f);
                // animationShip.GetComponent<AnimationShip>().EndtTeleport(current.position); animacja
                current.GetComponent<ShipStats>().RecivedDamage(dmg);

                //Odejmowanie punktow ruchu
                GetComponent<ShipStats>().movePoints = 0;



            }

        }
        else
        {
            Debug.Log("Obiekt poza zasiegiem");
        }
        last.GetComponentInChildren<ShipRange>().SeeRange(0); //wyłaczenie pokazywania zasiegu
        ShipGui.abilityAtackActive = false;//Dezaktywacja skilla ataku

    }
    void Update()
    {
        if (PlayerInfo.playerTurn != 1 && ShipGui.abilityAtackActive == true)
        {
            ShipGui.abilityAtackActive = false;
            GetComponentInChildren<ShipRange>().SeeRange(0);
        }
    }
}
