using UnityEngine;
using System.Collections;

public class ShipStats : MonoBehaviour {

    public string shipName = "";
    public int shipID; //0 maly statek; 1 duzy statek; 2 statek kolonizacyjny; 3 gwiazda smierci
    public int hp;
    public int absorb;
    public int movePoints;
    public int dmg;
    private int maxHp;
    public int maxDmg;
    public int cost;
    private int baseDmg;
    private int baseMaxDmg;
    private int reduceDmg;
    private int reduceMaxDmg;

    public int pos_X;
    public int pos_Y;

    public int shipOwner;

    public GameObject animationShip;

    public Texture2D hpBar;
    GameObject hex;
    void Start () {
	    if(shipOwner > 1)
        {
            foreach (GameObject ai in GameObject.FindGameObjectsWithTag("Ai"))
            {
                if (ai.GetComponent<ComputerAI>().player == shipOwner)
                {
                    ai.GetComponent<ComputerAI>().numberOfShips += 1;
                    if(shipID == 2)
                    {
                        ai.GetComponent<ComputerAI>().numberOfShipId2 += 1;
                    }
                }
            }
        }
        else if(shipOwner == 1)
        {
            PlayerInfo.numberOfShips += 1;
            PlayerInfo.generateGold -= cost;
        }

        movePoints = 0;
        baseDmg = dmg;
        baseMaxDmg = maxDmg;
        reduceDmg = dmg / 2;
        reduceMaxDmg = maxDmg / 2;
        maxHp = hp;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(shipOwner == 1)
        {

            if(PlayerInfo.numberOfGold < 0)
            {
                dmg = reduceDmg;
                maxDmg = reduceMaxDmg;
            }
            else
            {
                dmg = baseDmg;
                maxDmg = baseMaxDmg;
            }

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "HexMap" )
        {
             hex = other.gameObject;
            pos_X = hex.GetComponent<HexNumber>().hex_x;
            pos_Y = hex.GetComponent<HexNumber>().hex_y;
        }
            

    }


    void OnGUI()
    {
        /* if(gameObject.GetComponent<MouseSelect>().shipSelected == true)
         {

             GUI.Box(new Rect(0, (Screen.height) - 100, 200, 100), shipName);
             GUI.Label(new Rect(5, (Screen.height) - 80, 200, 20), "Health Poinst: " + hp);
             GUI.Label(new Rect(5, (Screen.height) - 65, 200, 20), "Damage: " + dmg);
             GUI.Label(new Rect(5, (Screen.height) - 50, 200, 20), "Move Points: " + movePoints);
         }*/

       // GUI.DrawTexture(new Rect(0, 0.25f,100 *  (hp / maxHp) * 100, 50), hpBar);

    }



    public void RecivedDamage(int damage)
    {
        animationShip.GetComponent<AnimationShip>().Explosion(transform.position);
        hp -= damage;
        Debug.Log(shipName + " " + "Otrzymał dmg: " + damage);
        if (hp <= 0)
        {
            Death();
        }
    }

   public void Death()
    {
        if (shipOwner > 1)
        {
            foreach (GameObject ai in GameObject.FindGameObjectsWithTag("Ai"))
            {
                if (ai.GetComponent<ComputerAI>().player == shipOwner)
                {
                    ai.GetComponent<ComputerAI>().numberOfShips -= 1;
                    if (shipID == 2)
                    {
                        ai.GetComponent<ComputerAI>().numberOfShipId2 -= 1;
                    }
                }
            }
        }
        else if (shipOwner == 1)
        {
            PlayerInfo.numberOfShips -= 1;
        }
        animationShip.GetComponent<AnimationShip>().DeathAnimation(transform.position);
        GetComponentInChildren<ShipRange>().SeeRange(0);
        hex.GetComponent<HexNumber>().occupied = false;
        hex.GetComponent<HexNumber>().inRange = false;
        hex.GetComponent<HexNumber>().hexColor();
        PlayerInfo.generateGold += cost;
        Destroy(gameObject);
    }

    public void HpBar()
    {

    }


}

