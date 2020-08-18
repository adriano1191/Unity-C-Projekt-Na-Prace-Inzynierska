using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {

    public GameObject backGround;
    private float x = 0;
    private float y = 0;
    private float addX = 38f;
    private float addY = 22f;
    private int N;
    private int map = 1; //od 1 do 2

    void Start () {

        map = MapInfo.mapSize;
        map = PlayerPrefs.GetInt("mapSize");
        if (map == 0)
        {
            N = 2;
        }
        if (map == 1)
        {
            N = 3;
        }
        if (map == 2)
        {
            N = 4;
        }

        for (int i = 0; i < N; i++)
        {

            for (int j = 0; j < N; j++)
            {
            Instantiate(backGround, new Vector3(x, y, 10), Quaternion.identity);
            x = x + addX;
            }
            x = 0;
            y = y + addY;
        }
        //Instantiate(backGround, new Vector3(0, 0, 10), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
