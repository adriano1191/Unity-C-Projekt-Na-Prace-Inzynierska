using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGameGui : MonoBehaviour {

    float native_width = 1366;
    float native_height = 768;
    //Wielkosc mapy
    private int mapSizeId = 0;
    private int[] mapSize = new int[] {0, 1, 2};
    private string[] mapSizeString;

    //Liczba graczy
    private int numberOfBots = 1;
    private int maxBots = 3;

    //Poziom Trudnosci
    private int difId;

    public bool newGame = false;

    private GUIStyle buttonGuiStyle = new GUIStyle();
    private GUIStyle LabelGuiStyle = new GUIStyle();
    // Use this for initialization
    void Start () {
       
	}
	
    void OnGUI()
    {

        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUIUtility.ScaleAroundPivot(new Vector2(rx, ry), Vector2.zero);

        buttonGuiStyle = GUI.skin.button;
        buttonGuiStyle.fontSize = 40; //change the font size

        LabelGuiStyle = GUI.skin.label;
        LabelGuiStyle.fontSize = 30; //change the font size
        LabelGuiStyle.alignment = TextAnchor.MiddleLeft;

        GUIStyle resLabelGuiStyle = GUI.skin.label;
        resLabelGuiStyle.alignment = TextAnchor.MiddleCenter;
        resLabelGuiStyle.fontSize = 30;
        if (newGame == true)
        {
            mapSizeString = new string[] { GUIStrings.LABEL_MAP_SIZE_SMALL, GUIStrings.LABEL_MAP_SIZE_MEDIUM, GUIStrings.LABEL_MAP_SIZE_BIG };
            GUI.Box(new Rect(100, 100, native_width - 200, native_height - 200), "");

            //Wielkosc mapy
            GUI.Label(new Rect(150, 150, 300, 50), GUIStrings.LABEL_MAP_SIZE, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 150, 50, 50), "<", buttonGuiStyle))
            {
                if (mapSizeId <= 0)
                {
                    mapSizeId = mapSize.Length - 1;
                    Debug.Log(mapSizeId);
                    PlayerPrefs.SetInt("mapSize", mapSizeId);
                }
                else
                {

                    mapSizeId--;
                    Debug.Log(mapSizeId);
                    PlayerPrefs.SetInt("mapSize", mapSizeId);
                }
            }
            if (GUI.Button(new Rect(900, 150, 50, 50), ">", buttonGuiStyle))
            {
                if (mapSizeId == mapSize.Length - 1)
                {
                    mapSizeId = 0;
                    Debug.Log(mapSizeId);
                    PlayerPrefs.SetInt("mapSize", mapSizeId);
                }
                else
                {

                    mapSizeId++;
                    Debug.Log(mapSizeId);
                    PlayerPrefs.SetInt("mapSize", mapSizeId);
                }

            }
            GUI.Label(new Rect(650, 150, 260, 50), mapSizeString[mapSizeId], resLabelGuiStyle);

            //Liczba graczy
            GUI.Label(new Rect(150, 210, 300, 50), GUIStrings.LABEL_AI_NUMBER, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 210, 50, 50), "<", buttonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (numberOfBots <= 1)
                {
                    numberOfBots = maxBots;
                }
                else
                {

                    numberOfBots--;
                }
            }
            if (GUI.Button(new Rect(900, 210, 50, 50), ">", buttonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (numberOfBots >= maxBots)
                {
                    numberOfBots = 1;
                }
                else
                {

                    numberOfBots++;
                }

            }

            GUI.Label(new Rect(650, 210, 260, 50), numberOfBots.ToString(), resLabelGuiStyle);

            //Poziom Trudnosci

            GUI.Label(new Rect(150, 270, 300, 50), GUIStrings.LABEL_DIFFICULTY, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 270, 50, 50), "<", buttonGuiStyle))
            {

            }
            if (GUI.Button(new Rect(900, 270, 50, 50), ">", buttonGuiStyle))
            {


            }


            GUI.Label(new Rect(650, 270, 260, 50), GUIStrings.LABEL_NOT_AVAILABLE, resLabelGuiStyle);

            //NOWA GRA
            if (GUI.Button(new Rect(native_width / 2 - 210, native_height - 200, 200, 50), GUIStrings.BTN_PLAY, buttonGuiStyle))
            {
                GetComponent<Menu>().mainMenu = true;
                newGame = false;
                StartGame();
            }
            if (GUI.Button(new Rect(native_width / 2 + 10, native_height - 200, 200, 50), GUIStrings.BTN_CANCEL, buttonGuiStyle))
            {
                GetComponent<Menu>().mainMenu = true;
                newGame = false;
            }
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("mapSize", mapSizeId);
        PlayerPrefs.SetInt("numberOfBots", numberOfBots);
        SceneManager.LoadScene("Game");
    }

    }

