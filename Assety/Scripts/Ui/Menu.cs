using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class Menu : MonoBehaviour
{

    float native_width = 1366;
    float native_height = 768;

    private int firstRun;

    private GUIStyle buttonGuiStyle = new GUIStyle();
    private GUIStyle LabelGuiStyle = new GUIStyle();

    private string currentSite = "MainMenu";
    private string language = "pl";
    private string[] languages = new string[] { "pl", "en" };
    private string[] languagesFullName;
    public string theReader;
    public TextAsset mydata;

    public bool mainMenu = true;
    public bool optionsMenu = false;

    //Rozdzielczosc
    private int resId = 0;
    private Resolution[] resolutions;
    private int fullId = 0;
    private bool fullScreen;
    private string fullScreenText;
    private int langId;
    // Use this for initialization
    void Start()
    {
        GetResolutions();
        DefaultSettings();
        _generateLabels();
        

    }

    // Update is called once per frame
    void Update()
    {
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
        if (mainMenu == true)
        {

            GUI.Box(new Rect(native_width / 2 - 650, native_height / 2 - 200, 400, 520), "");
            if (GUI.Button(new Rect(native_width / 2 - 600, native_height / 2 - 170, 300, 100), GUIStrings.BTN_NEW_GAME, buttonGuiStyle))
            {
                mainMenu = false;
                PlayerPrefs.SetInt("mapSize", 0);
                
                GetComponent<NewGameGui>().newGame = true;
            }
            GUI.enabled = false;
            if (GUI.Button(new Rect(native_width / 2 - 600, native_height / 2 - 50, 300, 100), GUIStrings.BTN_LOAD_GAME, buttonGuiStyle))
            {

            }
            GUI.enabled = true;
            if (GUI.Button(new Rect(native_width / 2 - 600, native_height / 2 + 70, 300, 100), GUIStrings.BTN_OPTIONS, buttonGuiStyle))
            {
                    mainMenu = false;
                    optionsMenu = true;
               }
            if (GUI.Button(new Rect(native_width / 2 - 600, native_height / 2 + 190, 300, 100), GUIStrings.BTN_EXIT, buttonGuiStyle))
            {
                Application.Quit();
            }
        }
        if (optionsMenu == true)
        {



            GUI.Box(new Rect(100, 100, native_width - 200, native_height - 200), "");

            //Rozdzielczosc
            GUI.Label(new Rect(150, 150, 300, 50), GUIStrings.LABEL_RESOLUTIONS, LabelGuiStyle);
            if (GUI.Button(new Rect(600, 150, 50, 50), "<", buttonGuiStyle))
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
            if (GUI.Button(new Rect(900, 150, 50, 50), ">", buttonGuiStyle))
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
            if (GUI.Button(new Rect(600, 210, 50, 50), "<", buttonGuiStyle))
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
            if (GUI.Button(new Rect(900, 210, 50, 50), ">", buttonGuiStyle))
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
            languagesFullName = new string[] {GUIStrings.LANGUAGE_PL, GUIStrings.LANGUAGE_EN }; 
        GUI.Label(new Rect(150, 270, 300, 50), GUIStrings.LABEL_LANGUAGE, LabelGuiStyle);
        if (GUI.Button(new Rect(600, 270, 50, 50), "<", buttonGuiStyle))
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
        if (GUI.Button(new Rect(900, 270, 50, 50), ">", buttonGuiStyle))
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
            if (GUI.Button(new Rect(native_width / 2 - 210, native_height - 200, 200, 50), GUIStrings.BTN_SAVE, buttonGuiStyle))
            {
                SaveChange();
                mainMenu = true;
                optionsMenu = false;
            }
            if (GUI.Button(new Rect(native_width / 2 + 10, native_height - 200, 200, 50), GUIStrings.BTN_CANCEL, buttonGuiStyle))
            {
                mainMenu = true;
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

    void GetResolutions()
    {
        resolutions = Screen.resolutions;

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

    void DefaultSettings()
    {
        if (!Application.isEditor)
        {

            firstRun = PlayerPrefs.GetInt("firstRun");
        }
        if(firstRun == 0)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            resId = resolutions.Length - 1;
            fullId = 1;
            langId = 0;

            PlayerPrefs.SetInt("firstRun", 1);
            PlayerPrefs.SetInt("resId", resId);
            PlayerPrefs.SetInt("fullId", fullId);
            PlayerPrefs.SetInt("langId", langId);

        }
        else
        {
          resId  = PlayerPrefs.GetInt("resId");
          fullId = PlayerPrefs.GetInt("fullId");
          langId = PlayerPrefs.GetInt("langId");
            SaveChange();
        }
        //PlayerPrefs.SetInt("First", 1);
        //firstRun = PlayerPrefs.GetInt("First");
    }
}
