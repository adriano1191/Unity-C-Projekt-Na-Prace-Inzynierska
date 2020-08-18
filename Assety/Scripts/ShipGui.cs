using UnityEngine;
using System.Collections;

public class ShipGui : MonoBehaviour {

    private string shipName = "";
    private int hp;
    private int absorb;
    private int movePoints;
    private int dmg;

    public int pos_X;
    public int pos_Y;

    private int shipOwner;
    private Color tmp;
    private Color color = new Color32(20, 55, 90, 255);  //Kolor Hexa
    private float alpha = 0.7f;

    public int sId;

    bool ability_1 = false;
    //Skille
    public static bool abilityActive= false;
    public static bool abilityAtackActive = false;

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
    private string specialMove;
    private string specialMove2;

    void Start()
    {

        stringcontent = "";
        sId = GetComponent<ShipStats>().shipID; //ID statku
        if (sId == 0)  //maly
        {

        }
        else if (sId == 1) //duzy
        {

        }
        else if (sId == 2) //kolonizacyjny
        {
            OccupationPlanet add = gameObject.AddComponent<OccupationPlanet>();
        }
        else if (sId == 3)  //gwiazda 
        {

        }


    }

    void Update()
    {
        ShipStats sStats = gameObject.GetComponent<ShipStats>();
        shipName = sStats.shipName;
        hp = sStats.hp;
        absorb = sStats.absorb;
        movePoints = sStats.movePoints;
        dmg = sStats.dmg;
        shipOwner = sStats.shipOwner;

        if (Input.GetKeyDown("escape") || Input.GetMouseButtonDown(1))
        {
            abilityActive = false;
            abilityAtackActive = false;

            GetComponentInChildren<ShipRange>().SeeRange(0);
        }

        //Tooltip
        if (stringcontent.Length > 2)
        {
            tooltipOn = true;
        }
        else
        {
            tooltipOn = false;
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
            if (sId == 0)  //maly
            {
                specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE0;
                specialMove2 = GUIStrings.TOOLTIP_SPECIAL_SECOND_MOVE0;
            }
            else if (sId == 1) //duzy
            {
                specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE1;
                specialMove2 = GUIStrings.TOOLTIP_SPECIAL_SECOND_MOVE1;
            }
            else if (sId == 2) //kolonizacyjny
            {
                specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE2;
                specialMove2 = GUIStrings.TOOLTIP_SPECIAL_SECOND_MOVE2;
            }
            else if (sId == 3)  //gwiazda 
            {
                specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE3;
                specialMove2 = GUIStrings.TOOLTIP_SPECIAL_SECOND_MOVE3;
            }

            //Statystyki statku
            GUI.Box(new Rect(0, native_height - 150, 300, 150), shipName, boxGuiStyle);
            GUI.Label(new Rect(5, native_height - 110, 200, 20), GUIStrings.LABEL_HEALTH_POINT + ": " + hp, labelGuiStyle);
            GUI.Label(new Rect(5, native_height - 80, 200, 20), GUIStrings.LABEL_BARRIER+": " + absorb, labelGuiStyle);
            GUI.Label(new Rect(5, native_height - 50, 200, 20), GUIStrings.LABEL_DMG+": " + dmg, labelGuiStyle);
            

            if (shipOwner == 1 && PlayerInfo.playerTurn == 1)
            {

                //Mozliwosci statku
                GUI.Box(new Rect((native_width) - 300, (native_height) - 150, 300, 150), GUIStrings.LABEL_SKILLS, boxGuiStyle);
                //Pierwsza linia

                // Skill 1
                if (abilityActive == false && movePoints > 0 && GUI.Button(new Rect((native_width) - 280, (native_height) - 90, 50, 50), new GUIContent("Move", GUIStrings.TOOLTIP_MOVE)))
                {


                    Debug.Log("Ability Active ");
                    abilityActive = true;
                    GetComponentInChildren<ShipRange>().SeeRange(GetComponent<ShipStats>().movePoints);
                    
                    //ability_1 = true;
                
                    


                }
                else
                {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 280, (native_height) - 90, 50, 50), new GUIContent("Move", GUIStrings.TOOLTIP_MOVE));
                    GUI.enabled = true;
                }

                // Skill 2
                if (abilityActive == false && movePoints > 0 && GUI.Button(new Rect((native_width) - 210, (native_height) - 90, 50, 50), new GUIContent("Attack", GUIStrings.TOOLTIP_ATTACK)))
                {
                    Debug.Log("Attack Active ");
                    abilityAtackActive = true;
                    GetComponentInChildren<ShipRange>().SeeRange(GetComponent<ShipStats>().movePoints);
                }
                else
                {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 210, (native_height) - 90, 50, 50), new GUIContent("Attack", GUIStrings.TOOLTIP_ATTACK));
                    GUI.enabled = true;
                }

                // Skill 3
                if (abilityActive == false && movePoints > 0 && GUI.Button(new Rect((native_width) - 140, (native_height) - 90, 50, 50), new GUIContent("Spec", specialMove)))
                {
                    

                    if(sId == 0)  //maly
                    {
                        specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE0;
                    }
                    else if (sId == 1) //duzy
                    {
                        specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE1;
                    }
                    else if (sId == 2) //kolonizacyjny
                    {
                        specialMove = GUIStrings.TOOLTIP_SPECIAL_MOVE2;
                        if (GetComponent<OccupationPlanet>())
                        {
                            Debug.Log("Przejmowanie planety ");
                            GetComponent<OccupationPlanet>().Occupation();
                        }
                    }
                    else if (sId == 3)  //gwiazda 
                    {


                    }
                }
                else
                {
                    GUI.enabled = false;
                    GUI.Button(new Rect((native_width) - 140, (native_height) - 90, 50, 50), new GUIContent("Spec", specialMove));
                    GUI.enabled = true;
                }
                if (GUI.Button(new Rect((native_width) - 70, (native_height) - 90, 50, 50), new GUIContent("X", specialMove2)))
                {

                }
                /*
                //Druga linia
                if (GUI.Button(new Rect((native_width) - 280, (native_height) - 60, 50, 50), "Build"))
                {

                }
                if (GUI.Button(new Rect((native_width) - 220, (native_height) - 60, 50, 50), "Build"))
                {

                }
                if (GUI.Button(new Rect((native_width) - 160, (native_height) - 60, 50, 50), "Build"))
                {

                }
                if (GUI.Button(new Rect((native_width) - 100, (native_height) - 60, 50, 50), "Build"))
                {

                }*/

            }

        }        //tooltip
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

