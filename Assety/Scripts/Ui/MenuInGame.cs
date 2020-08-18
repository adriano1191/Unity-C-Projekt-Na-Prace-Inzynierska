using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour {

    float native_width = 1366;
    float native_height = 768;

    private GUIStyle menuButtonGuiStyle = new GUIStyle();
    private GUIStyle LabelGuiStyle = new GUIStyle();

    private string currentSite = "MainMenu";
    private string language = "pl";
    private string[] languages = new string[] { "pl", "en" };
    private string[] languagesFullName;
    public string theReader;
    public TextAsset mydata;

    public static bool turnMenu = false;

    //Options
    public bool optionsMenu = false;

    //Rozdzielczosc
    private int resId = 0;
    private Resolution[] resolutions;
    private int fullId = 0;
    private bool fullScreen;
    private string fullScreenText;
    private int langId;

    // Use this for initialization
    void Start () {

        GetResolutions();
        DefaultSettings();
        language = languages[PlayerPrefs.GetInt("langId")];
        _generateLabels();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.depth = -10;

        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUIUtility.ScaleAroundPivot(new Vector2(rx, ry), Vector2.zero);



        LabelGuiStyle = GUI.skin.label;
        LabelGuiStyle.fontSize = 30; //change the font size
        LabelGuiStyle.alignment = TextAnchor.MiddleLeft;

        GUIStyle resLabelGuiStyle = GUI.skin.label;
        resLabelGuiStyle.alignment = TextAnchor.MiddleCenter;
        resLabelGuiStyle.fontSize = 30;

        if (GUI.Button(new Rect(native_width - 100, 0, 100, 40),GUIStrings.BTN_OPTIONS)){
            turnMenu = true;
        }

        if(turnMenu == true)
        {
            GUI.Box(new Rect(native_width / 2 - 150, native_height / 2 - 200, 300, 400), "");

            GUI.enabled = false;
            if (GUI.Button(new Rect(native_width / 2 - 90, native_height / 2 - 180 , 180, 50), GUIStrings.BTN_SAVE_GAME))
            {

            }
            if (GUI.Button(new Rect(native_width / 2 - 90, native_height / 2 - 120, 180, 50), GUIStrings.BTN_LOAD_GAME))
            {

            }
            GUI.enabled = true;
            if (GUI.Button(new Rect(native_width / 2 - 90, native_height / 2 - 60, 180, 50), GUIStrings.BTN_OPTIONS))
            {
                optionsMenu = true;
                turnMenu = false;
            }
            if (GUI.Button(new Rect(native_width / 2 - 90, native_height / 2 - 0, 180, 50), GUIStrings.BTN_END_GAME))
            {
                optionsMenu = false;
                turnMenu = false;
                SceneManager.LoadScene("MainMenu");
            }
            if (GUI.Button(new Rect(native_width / 2 - 90, native_height / 2 + 100, 180, 50), GUIStrings.BTN_BACK_TO_GAME))
            {
                turnMenu = false;
            }

        }

        if (optionsMenu == true)
        {

            GUI.Box(new Rect(100, 100, native_width - 200, native_height - 200), "");
            menuButtonGuiStyle = GUI.skin.button;
            menuButtonGuiStyle.fontSize = 40; //change the font size
            //Rozdzielczosc
            GUI.Label(new Rect(150, 150, 300, 50), GUIStrings.LABEL_RESOLUTIONS, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 150, 50, 50), "<", menuButtonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (resId <= 0)
                {
                    resId = resolutions.Length - 1;
                }
                else
                {

                    resId--;
                }
            }
            if (GUI.Button(new Rect(900, 150, 50, 50), ">", menuButtonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (resId == resolutions.Length - 1)
                {
                    resId = 0;
                }
                else
                {

                    resId++;
                }

            }
            GUI.Label(new Rect(650, 150, 260, 50), resolutions[resId].width.ToString() + " X " + resolutions[resId].height.ToString(), resLabelGuiStyle);

            //fullScreen
            if (fullId == 0)
            {
                fullScreenText = GUIStrings.LABEL_TURN_OFF;
            }
            else
            {
                fullScreenText = GUIStrings.LABEL_TURN_ON;
            }
            GUI.Label(new Rect(150, 210, 300, 50), GUIStrings.LABEL_FULLSCREEN, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 210, 50, 50), "<", menuButtonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (fullId <= 0)
                {
                    fullId = 1;
                }
                else
                {

                    fullId--;
                }
            }
            if (GUI.Button(new Rect(900, 210, 50, 50), ">", menuButtonGuiStyle))
            {
                //Screen.SetResolution(640, 480, true);
                if (fullId >= 1)
                {
                    fullId = 0;
                }
                else
                {

                    fullId++;
                }

            }



            GUI.Label(new Rect(650, 210, 260, 50), fullScreenText, resLabelGuiStyle);


            //jezyk
            languagesFullName = new string[] { GUIStrings.LANGUAGE_PL, GUIStrings.LANGUAGE_EN };
            GUI.Label(new Rect(150, 270, 300, 50), GUIStrings.LABEL_LANGUAGE, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 270, 50, 50), "<", menuButtonGuiStyle))
            {
                if (langId <= 0)
                {
                    langId = languagesFullName.Length - 1;
                }
                else
                {

                    langId--;
                }
            }
            if (GUI.Button(new Rect(900, 270, 50, 50), ">", menuButtonGuiStyle))
            {
                if (langId >= languagesFullName.Length - 1)
                {
                    langId = 0;
                }
                else
                {

                    langId++;
                }

            }


            GUI.Label(new Rect(650, 270, 260, 50), languagesFullName[langId], resLabelGuiStyle);
            //zapisanie zmian
            if (GUI.Button(new Rect(native_width / 2 - 210, native_height - 200, 200, 50), GUIStrings.BTN_SAVE, menuButtonGuiStyle))
            {
                SaveChange();
                turnMenu = true;
                optionsMenu = false;
            }
            if (GUI.Button(new Rect(native_width / 2 + 10, native_height - 200, 200, 50), GUIStrings.BTN_CANCEL, menuButtonGuiStyle))
            {
                // mainMenu = true;
                turnMenu = true;
                optionsMenu = false;
            }
        }

    }

    private void _generateLabels()
    {
        try
        {

            string line;
            mydata = Resources.Load(language) as TextAsset;
            line = mydata.text;
            string[] fLines = Regex.Split(line, "\n");

            for (int i = 0; i < fLines.Length; i++)
            {
                string valueLine = fLines[i];
                string[] values = Regex.Split(valueLine, ";");
                typeof(GUIStrings).GetProperty(values[0]).SetValue(null, values[1], null);

            }



        }
        catch (Exception e)
        {
            Debug.Log("Error");
        }
    }

    void SaveChange()
    {
        if (fullId == 0)
        {
            fullScreen = false;
        }
        else
        {
            fullScreen = true;
        }

        Screen.SetResolution(resolutions[resId].width, resolutions[resId].height, fullScreen);
        language = languages[langId];
        _generateLabels();
        PlayerPrefs.SetInt("resId", resId);
        PlayerPrefs.SetInt("fullId", fullId);
        PlayerPrefs.SetInt("langId", langId);
    }
    void GetResolutions()
    {
        resolutions = Screen.resolutions;

    }
    void DefaultSettings()
    {


            resId = PlayerPrefs.GetInt("resId");
            fullId = PlayerPrefs.GetInt("fullId");
            langId = PlayerPrefs.GetInt("langId");
            SaveChange();
        
        //PlayerPrefs.SetInt("First", 1);
        //firstRun = PlayerPrefs.GetInt("First");
    }
}
