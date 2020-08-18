using UnityEngine;
using System.Collections;

public class MapInfo : MonoBehaviour {

    public static int mapSize;
    public static int numberOfPlayers;
    public static int playerInGame;

    public GameObject Ai;

    void Start()
    {
        mapSize = PlayerPrefs.GetInt("mapSize");
        numberOfPlayers = PlayerPrefs.GetInt("numberOfBots") + 1;
        playerInGame = numberOfPlayers - 1;
        
        for(int i = 0; i <= numberOfPlayers -2; i++)
        {
            (Instantiate(Ai, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation) as GameObject).GetComponent<ComputerAI>().player = i + 2;
        }
    }
    //small = 0
    //medium = 1
    //big = 2

}
