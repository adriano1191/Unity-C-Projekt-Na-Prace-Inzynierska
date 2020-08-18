using UnityEngine;
using System.Collections;


public class PlanetEarth : MonoBehaviour
{

    public string planetName;
    public int hp;
    private int maxHp;
    public int gold;
    public int crystal;
    private Color tmp;
    public Color color = new Color32(20, 55, 90, 255);  //Kolor Hexa
    private float alpha = 0.7f;
    public int owner; //Kto jest wlascicielem planety domyslnie 1 == gracz

    public bool ownerTurn = false;
    //Budowanie
    private bool building1 = false;

    private bool building2 = false;

    private bool building3 = false;

    private bool building4 = false;

   
    //zmienne do tooltipow
    private bool tooltipOn;
    public string stringcontent;
    private float ssize;

    //zmienne gui
    float native_width = 1366;
    float native_height = 768;
    private GUIStyle boxGuiStyle = new GUIStyle();
    private GUIStyle labelGuiStyle = new GUIStyle();
    private GUIStyle buttonGuiStyle = new GUIStyle();

    private GameObject pref;
    private GameObject ship;



    public void Regeneration()
    {
        hp += 20;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void PlanetDMg()
    {
        //Przejmowanie planety

        if (GetComponent<HexNumber>().occupied == true && GetComponent<HexNumber>().collidObj != null)
        {
            GameObject ship = GetComponent<HexNumber>().collidObj;
            if (ship.GetComponent<ShipStats>().shipOwner != owner)
            {
                hp -= ship.GetComponent<ShipStats>().dmg;
                if (hp <= 0)
                {
                    if(owner > 1)
                    {
                        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("Ai"))
                        {
                            if(ai.GetComponent<ComputerAI>().player == owner)
                            {
                                ai.GetComponent<ComputerAI>().numberOfPlanet -= 1;
                            }
                        }
                    }
                    else if (owner == 1)
                    {
                        PlayerInfo.numberOfPlanet -= 1;
                    }
                    owner = ship.GetComponent<ShipStats>().shipOwner;
                    hp = 100;
                    if(owner > 1)
                    {
                        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("Ai"))
                        {
                            if (ai.GetComponent<ComputerAI>().player == owner)
                            {
                                ai.GetComponent<ComputerAI>().numberOfPlanet += 1;
                            }

                        }
                    }
                    else if (owner == 1)
                    {
                        PlayerInfo.numberOfPlanet += 1;
                    }
                }
            }
        }
    }

    void buildingOne()
    {
        if (owner == 1)
        {
            if(PlayerInfo.numberOfCrystals >= 250)
            {
                PlayerInfo.numberOfCrystals -= 250;
                PlayerInfo.generateGold = PlayerInfo.generateGold + gold;
                PlayerInfo.generateCrystals = PlayerInfo.generateCrystals + crystal;
                hp = hp + 100;
                building1 = true;

            }
        }
    }

    void buildingTwo()
    {
        if (PlayerInfo.numberOfCrystals >= 200)
        {
            PlayerInfo.numberOfCrystals -= 200;
            PlayerInfo.generateGold -= 10;
            hp = hp + 50;
            building2 = true;
        }
    }

    void buildingThree()
    {
        if (PlayerInfo.numberOfCrystals >= 150)
        {
            PlayerInfo.numberOfCrystals -= 150;
            PlayerInfo.generateGold -= 25;
            hp = hp + 50;
            building3 = true;
        }
    }

    void buildingFour()
    {
        if (PlayerInfo.numberOfCrystals >= 10000)
        {
            PlayerInfo.numberOfCrystals -= 10000;
            PlayerInfo.generateGold -= 100;
            building4 = true;
        }
    }


    // Kupowanie statkow
   public void ShipBuy(int idShip)
    {

        if (idShip == 0)
        {
            ship = pref.GetComponent<Ships>().ship0;
        }
        else if (idShip == 1)
        {
            ship = pref.GetComponent<Ships>().ship1;
        }
        else if (idShip == 2)
        {
            ship = pref.GetComponent<Ships>().ship2;
        }
        else if (idShip == 3)
        {
            ship = pref.GetComponent<Ships>().ship3;
        }
        (Instantiate(ship, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation) as GameObject).GetComponent<ShipStats>().shipOwner = owner;
        //Instantiate(ship, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
    }

    void Start()
    {
        pref = GameObject.FindGameObjectWithTag("Prefabs");

        GetComponentInChildren<SpriteRenderer>().color = color;
        buildingOne();
        stringcontent = "";

        hp = Random.Range(100, 300);
        maxHp = hp;
        gold = Random.Range(75, 120);
        crystal = Random.Range(20, 60);

        // transparency.a = 0.7f;
    }
    void Update()
    {


        //Tooltip
        if (stringcontent.Length > 2)
        {
            tooltipOn = true;
        }
        else
        {
            tooltipOn = false;
        }
        /*
        if(GetComponent<MouseSelect>().selected == false)
        {

            if(owner == 1)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(50, 250, 10, 255); //Green
            }
            else if (owner > 1)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 0, 0, 255); //Red
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(71, 71, 71, 255); //Grey
            }
        }
        */

        //nowa tura
        if (PlayerInfo.playerTurn != owner)
        {
            if(ownerTurn == false)
            {
                Regeneration();
            }
            ownerTurn = true;

        }

        if (PlayerInfo.playerTurn == owner && ownerTurn == true)
        {

            PlanetDMg();

            ownerTurn = false;
        }




    }
    
    void OnGUI()
    {



        //Dopasowanie rozdzielczosci 
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUIUtility.ScaleAroundPivot(new Vector2(rx, ry), Vector2.zero);
        //wyglad GUI

        boxGuiStyle = GUI.skin.box;
        boxGuiStyle.fontSize = 20; //change the font size
        boxGuiStyle.normal.textColor = Color.white;
        boxGuiStyle.alignment = TextAnchor.UpperCenter;

        labelGuiStyle = GUI.skin.label;
        labelGuiStyle.fontSize = 20; //change the font size
        labelGuiStyle.normal.textColor = Color.white;
        labelGuiStyle.alignment = TextAnchor.MiddleLeft;

        if (gameObject.GetComponent<MouseSelect>().selected == true)
        {

            //Statystyki planety
            GUI.Box(new Rect(0, native_height - 150, 300, 150),  planetName);
            GUI.Label(new Rect(5, native_height - 110, 200, 20), GUIStrings.LABEL_HEALTH_POINT+": " + hp);
            GUI.Label(new Rect(5, native_height - 80, 200, 20), GUIStrings.LABEL_GOLD+": " + gold);
            GUI.Label(new Rect(5, native_height - 50, 200, 20), GUIStrings.LABEL_CRYSTAL+": " + crystal);

        if(owner == 1)
        {

                //Mozliwosci planety
                GUI.Box(new Rect((native_width) - 300, (native_height) - 150, 300, 150), GUIStrings.LABEL_BUILD, boxGuiStyle);

                //Pierwsza linia
                // B1
                if (building1 == false && GUI.Button(new Rect((native_width) - 280, (native_height) - 120, 50, 50), new GUIContent("B1", GUIStrings.TOOLTIP_BUILDING0)))
                {

                    buildingOne(); 

            }
            else if (building1 == true)
            {
                GUI.enabled = false;
                GUI.Button(new Rect((native_width) - 280, (native_height) - 120, 50, 50), new GUIContent("B1", GUIStrings.TOOLTIP_BUILDING0));
                GUI.enabled = true;
            }
            // B2
            if (building2 == false && building1 == true && GUI.Button(new Rect((native_width) - 220, (native_height) - 120, 50, 50), new GUIContent("B2", GUIStrings.TOOLTIP_BUILDING1)))
                {
                    buildingTwo();
                }
            else
            {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 220, (native_height) - 120, 50, 50), new GUIContent("B2", GUIStrings.TOOLTIP_BUILDING1));
                    GUI.enabled = true;
             }
            // B3
            if (building3 == false && building1 == true && GUI.Button(new Rect((native_width) - 160, (native_height) - 120, 50, 50), new GUIContent("B3", GUIStrings.TOOLTIP_BUILDING2)))
                {
                    buildingThree();
            }
                else
                {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 160, (native_height) - 120, 50, 50), new GUIContent("B3", GUIStrings.TOOLTIP_BUILDING2));
                    GUI.enabled = true;
                }
            // B4
            if (building1 == true && building2 == true && building3 == true && building4 == false && GUI.Button(new Rect((native_width) - 100, (native_height) - 120, 50, 50), new GUIContent("B4", GUIStrings.TOOLTIP_BUILDING3)))
                {
                    buildingFour();
            }
                else
                {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 100, (native_height) - 120, 50, 50), new GUIContent("B4", GUIStrings.TOOLTIP_BUILDING3));
                    GUI.enabled = true;
                }

                //Druga linia
                // S1
            if (building2 == true && GUI.Button(new Rect((native_width) - 280, (native_height) - 60, 50, 50), new GUIContent("S1", GUIStrings.TOOLTIP_SHIP0)))
            {
                    if (GetComponent<HexNumber>().occupied != true && PlayerInfo.numberOfCrystals >= 100)
                    {

                        ShipBuy(0);
                        PlayerInfo.numberOfCrystals -= 100;

                    }
                }
            else
            {
                GUI.enabled = false;
                GUI.Button(new Rect((native_width) - 280, (native_height) - 60, 50, 50), new GUIContent("S1", GUIStrings.TOOLTIP_SHIP0));
                GUI.enabled = true;
            }
            // S2
            if (building2 == true && building3 == true && GUI.Button(new Rect((native_width) - 220, (native_height) - 60, 50, 50), new GUIContent("S2", GUIStrings.TOOLTIP_SHIP1)))
            {
                    if (GetComponent<HexNumber>().occupied != true && PlayerInfo.numberOfCrystals >= 250)
                    {

                        ShipBuy(1);
                        PlayerInfo.numberOfCrystals -= 250;

                    }
            }
            else
            {
                GUI.enabled = false;
                GUI.Button(new Rect((native_width) - 220, (native_height) - 60, 50, 50), new GUIContent("S2", GUIStrings.TOOLTIP_SHIP1));
                GUI.enabled = true;
            }

            // S3
            if (building3 == true && GUI.Button(new Rect((native_width) - 160, (native_height) - 60, 50, 50), new GUIContent("S3", GUIStrings.TOOLTIP_SHIP2)))
            {
                    if (GetComponent<HexNumber>().occupied != true && PlayerInfo.numberOfCrystals >= 500)
                    {

                        ShipBuy(2);
                        PlayerInfo.numberOfCrystals -= 500;

                    }
                }
            else
            {
                GUI.enabled = false;
                GUI.Button(new Rect((native_width) - 160, (native_height) - 60, 50, 50), new GUIContent("S3", GUIStrings.TOOLTIP_SHIP2));
                GUI.enabled = true;
            }
            // S4
            if (building4 == true && GUI.Button(new Rect((native_width) - 100, (native_height) - 60, 50, 50), new GUIContent("S4", GUIStrings.TOOLTIP_SHIP3)))
            {

            }
            else
            {
                GUI.enabled = false;
                GUI.Button(new Rect((native_width) - 100, (native_height) - 60, 50, 50), new GUIContent("S4", GUIStrings.TOOLTIP_SHIP3));
                GUI.enabled = true;
            }

            }

        }
        if (tooltipOn)
        {
            GUIContent content = new GUIContent(GUI.tooltip);
            GUIStyle style = GUI.skin.box;
            style.wordWrap = true;
            style.alignment = TextAnchor.MiddleLeft;

            // Compute how large the button needs to be.        
            ssize = style.CalcHeight(content, 100f);

            // make the Box double sized

            GUI.Box(new Rect((native_width) - 300, (native_height) - 150 - ssize, 300, ssize), GUI.tooltip);
            style.alignment = TextAnchor.UpperCenter;
        }

        stringcontent = GUI.tooltip;

    }




}
