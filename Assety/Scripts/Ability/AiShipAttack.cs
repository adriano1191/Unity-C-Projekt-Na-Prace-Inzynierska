using UnityEngine;
using System.Collections;

public class AiShipAttack : MonoBehaviour {

    public void AttackPlayerShip()
    {
        Debug.Log("Ai Atak wykonywanie");

        StartCoroutine(AiAttack());
    }

    IEnumerator AiAttack()
    {

        if (GetComponent<ShipStats>().movePoints > 0)
        {
            Debug.Log("Ai movePoints > 0");

            foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
            {
                if (ship)
                {

                
                GetComponentInChildren<ShipRange>().SeeRange(GetComponent<ShipStats>().movePoints);

                yield return new WaitForSeconds(0.0f);
                if (ship.GetComponentInChildren<ShipRange>().inRange == true && ship.GetComponent<ShipStats>().shipOwner != GetComponent<ShipStats>().shipOwner)
                {
                    int dmg;
                    ShipStats shipstats = GetComponent<ShipStats>();

                    dmg = Random.Range(shipstats.GetComponent<ShipStats>().dmg, shipstats.GetComponent<ShipStats>().maxDmg);
                    //dmg = shipstats.GetComponent<ShipStats>().dmg;
                    // animationShip.GetComponent<AnimationShip>().StartTeleport(last.position); Animacja
                    //yield return new WaitForSeconds(0.2f);
                    // animationShip.GetComponent<AnimationShip>().EndtTeleport(current.position); animacja
                    ship.GetComponent<ShipStats>().RecivedDamage(dmg);
                    shipstats.movePoints -= 1;

                    GetComponent<ShipStats>().movePoints = 0;
                    Debug.Log("Ai Atak Wykonano! ");
                }
            }
                Debug.Log("Ai foreach wykonano");
            }
            GetComponentInChildren<ShipRange>().SeeRange(0);
            //yield return null;
        }
    }

}
