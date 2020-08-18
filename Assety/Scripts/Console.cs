using UnityEngine;
using System.Collections;
using System;

public class Console : MonoBehaviour
{

    public bool toggleConsole = false;
    private string lastText = "spawn.enemy";
    public string text = "";
    public string textArea;

    public GameObject prefabShip;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("`"))
        {
            toggleConsole = !toggleConsole;
            Debug.Log(toggleConsole);
        }
    }
    void Cheats(string cheat)
    {
        if(cheat == "spawn.enemy")
        {
            Transform pos = MouseSelect.trSelect;
            if (pos != null)
            {
                Debug.Log("Spawn");
                (Instantiate(prefabShip, new Vector3(pos.position.x, pos.position.y, -2), Quaternion.identity) as GameObject).GetComponent<ShipStats>().shipOwner = 2;

            }
            else
            {
                textArea += "\nHex not Selected";
            }
        }
        if (cheat == "spawn.player")
        {
            Transform pos = MouseSelect.trSelect;
            if (pos != null)
            {
                Debug.Log("Spawn");
                (Instantiate(prefabShip, new Vector3(pos.position.x, pos.position.y, -2), Quaternion.identity) as GameObject).GetComponent<ShipStats>().shipOwner = 1;

            }
            else
            {
                textArea += "\nHex not Selected";
            }
        }
        if(cheat == "gold")
        {
            PlayerInfo.numberOfGold += 250;
        }
        if (cheat == "gold.gen")
        {
            PlayerInfo.generateGold += 50;
        }
        if (cheat == "crys")
        {
            PlayerInfo.numberOfCrystals += 2500;
        }
        if (cheat == "crys.gen")
        {
            PlayerInfo.generateCrystals += 50;
        }
    }

    

    void OnGUI()
    {
        if (toggleConsole)
        {
            //GUI.Box(new Rect(5, 20, Screen.width / 3, Screen.height / 2), "");
            GUI.TextArea(new Rect(5, 20, Screen.width / 3, Screen.height / 2), "Console: " + textArea);
            //GUI.SetNextControlName("MyTextField");
            text = GUI.TextField(new Rect(5, ((Screen.height / 2) + 20), Screen.width / 3, 20), text, 25);
            if (text != "" && Event.current.keyCode == KeyCode.Return)
            {
                bool atLeastOneNotspace = false;

                for (int i = 0; i < text.Length; i++)
                {
                    if (!(text[i] == ' '))
                    {
                        atLeastOneNotspace = true;
                        break;
                    }
                }

                if (!atLeastOneNotspace)
                {
                    text = "";
                }
                else
                {
                    Cheats(text);
                    lastText = text;
                    textArea += "\n" + text;
                    text = "";
                }

            }

            if (Event.current.keyCode == KeyCode.UpArrow)
            {
                text = lastText;
            }
        }
    }


}