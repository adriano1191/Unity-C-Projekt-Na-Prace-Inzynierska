using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {

    public static int numberOfGold;
    public static int numberOfCrystals;
    public static int generateGold;
    public static int generateCrystals;
    public static int numberOfPlanet;
    public static int numberOfShips;

    public static int playerTurn;
    public int maxPlayer;
    public float timer;
    private string endTurn = GUIStrings.LABEL_END_TURN;

    //zmienne do tooltipow
    private bool tooltipOn;
    public string stringcontent;
    private float ssize;

    float native_width = 1366;
    float native_height = 768;
    private GUIStyle newGuiStyle = new GUIStyle();
    private GUIStyle buttonGuiStyle = new GUIStyle();

    private bool canLoose = false;

    private bool bWin = false;
    private bool bLoose = false;
    // Use this for initialization
    void Start () {
        stringcontent = "";
        //maxPlayer = MapInfo.numberOfPlayers;
        maxPlayer = PlayerPrefs.GetInt("numberOfBots") + 1;
        playerTurn = 1;
        numberOfCrystals = 1000;
        numberOfGold = 250;
        generateGold = 10;
        generateCrystals = 0;

        

    }
	
	// Update is called once per frame
	void Update () {

        if (playerTurn != 1)
        {
            timer += Time.deltaTime;
            if(timer >= 0)
            {
                endTurn = "   " + GUIStrings.LABEL_WAIT+ "   ";
            }
            if (timer > 1)
            {
                endTurn = "   " + GUIStrings.LABEL_WAIT + ".  ";
            }
            if (timer > 2)
            {
                endTurn = "   " + GUIStrings.LABEL_WAIT + ".. ";
            }
            if(timer > 3)
            {
                endTurn = "   " + GUIStrings.LABEL_WAIT + "...";
            }
            if(timer > 4)
            {
                timer = 0;
            }
        }
        else
        {
            endTurn = GUIStrings.LABEL_END_TURN;
        }
        if (stringcontent.Length > 2)
        {
            tooltipOn = true;
        }
        else
        {
            tooltipOn = false;
        }

           //zasady przegranej
        if(numberOfPlanet > 0)
        {
            canLoose = true;
        }

        if(canLoose == true && numberOfPlanet == 0)
        {
            Debug.Log("Przegrałeś!!");
            EndGame(1);
        }

        if(MapInfo.playerInGame <= 0)
        {
            Debug.Log("Wygrałeś!!");
            EndGame(0);
        }

    }

    void OnGUI()
    {
        //Dopasowanie rozdzielczosci 
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUIUtility.ScaleAroundPivot(new Vector2(rx, ry), Vector2.zero);
        newGuiStyle.fontSize = 20; //change the font size
        newGuiStyle.normal.textColor = Color.white;
        newGuiStyle.alignment = TextAnchor.MiddleLeft;

        //Koniec gry
        if(bWin == true || bLoose == true)
        {
            if (bWin == true)
            {
                GUI.Box(new Rect(native_width / 2 - 150, native_height / 2 - 75, 300, 150), GUIStrings.LABEL_VICTORY);

                if (GUI.Button(new Rect(native_width / 2 - 75, native_height / 2 - 0, 150, 50), GUIStrings.BTN_EXIT))
                {
                    GetComponent<MenuInGame>().optionsMenu = false;
                    MenuInGame.turnMenu = false;
                    bWin = false;
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                GUI.Box(new Rect(native_width / 2 - 150, native_height / 2 - 75, 300, 150), GUIStrings.LABEL_DEFEAT);

                if (GUI.Button(new Rect(native_width / 2 - 75, native_height / 2 - 0, 150, 50), GUIStrings.BTN_EXIT))
                {
                    GetComponent<MenuInGame>().optionsMenu = false;
                    MenuInGame.turnMenu = false;
                    bLoose = false;
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }


        GUI.Box(new Rect(0, 0, (native_width /2 ) - 80, 40), "");
        GUI.Label(new Rect(5, 0, 100, 40), new GUIContent(GUIStrings.LABEL_GOLD+": "+ numberOfGold + " " + generateGold, GUIStrings.TOOLTIP_GOLD), newGuiStyle);
        GUI.Label(new Rect(225, 0, 100, 40), new GUIContent(GUIStrings.LABEL_CRYSTAL+": " + numberOfCrystals + " " + generateCrystals, GUIStrings.TOOLTIP_CRYSTAL), newGuiStyle);
        GUI.Label(new Rect(470, 0, 100, 40), new GUIContent(GUIStrings.LABEL_PLANETS + ": " + numberOfPlanet, GUIStrings.TOOLTIP_PLANETS), newGuiStyle);

        GUI.Box(new Rect((native_width / 2) + 80, 0, native_width, 40), "");
        buttonGuiStyle = GUI.skin.button;
        buttonGuiStyle.fontSize = 30;
        if (playerTurn != 1 || MenuInGame.turnMenu == true)
        {

            GUI.enabled = false;

            GUI.Button(new Rect((native_width / 2) - 79, 0, 160, 80), new GUIContent(endTurn, GUIStrings.TOOLTIP_END_TURN), buttonGuiStyle);
            GUI.enabled = true;
            buttonGuiStyle.fontSize = 15;
        }
        else
        {



            if (GUI.Button(new Rect((native_width / 2) - 79, 0, 160, 80), new GUIContent(endTurn, GUIStrings.TOOLTIP_END_TURN), buttonGuiStyle))
            {

                NextTurn();

            }
            buttonGuiStyle.fontSize = 15;
            // GUI.Label(tooltipPos, GUI.tooltip);

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

        GUI.Label(new Rect(5, 25, 200, 100),"Liczba graczy: " + MapInfo.numberOfPlayers + " Obecna tura: " + playerTurn);
        
    }

    public void NextTurn()
    {

        playerTurn++;
        Debug.Log(playerTurn + "NEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURNNEXTTURN");
        if (playerTurn <= 0) //Ruch Neutralnych
        {
            TurnNeutral();
        }
        if(playerTurn == 1)// Ruch Gracza
        {
            TurnPlayer();
        }
        if(playerTurn > 1 && playerTurn <= maxPlayer) //Ruch Komputera
        {
            TurnAi();
        }
        if (playerTurn > maxPlayer)
        {

            playerTurn = 0;
            TurnNeutral();
        }
    }

    void TurnNeutral()
    {
        playerTurn = 0;
        NextTurn();
    }
    void TurnPlayer()
    {

        //Regeneracja punktow ruchu statkow gracza
        foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
        {
            if (ship.GetComponent<ShipStats>().shipOwner == 1)
            {
                ship.GetComponent<ShipStats>().movePoints = 2;
            }
        }

        //Generowanie zasobow
        Resourcesenerate();
    }
    void TurnAi()
    {
        if(playerTurn > maxPlayer)
        {
            playerTurn = 0;
            NextTurn();
        }
        else
        {

        foreach (GameObject Ai in GameObject.FindGameObjectsWithTag("Ai"))
        {

            if (Ai.GetComponent<ComputerAI>().player == playerTurn)
            {

                Ai.GetComponent<ComputerAI>().TurnOn();
            }
        }
        }
    }

    void Resourcesenerate()
    {
        numberOfGold = numberOfGold + generateGold;
        numberOfCrystals = numberOfCrystals + generateCrystals;
    }

    void EndGame(int i)
    {
        if(i == 0)
        {
            bWin = true;
            bLoose = false;
        }
        else
        {
            bLoose = true;
            bWin = false;
        }
    }
}
