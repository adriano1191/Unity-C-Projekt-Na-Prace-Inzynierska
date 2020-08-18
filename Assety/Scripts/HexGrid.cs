using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour
{
    public Transform spawnThis;

    public static int x = 5;
    public static int y = 5;
    public float radius = 0.5f;
    public bool useAsInnerCircleRadius = true;
    private int map;

    public float offsetX, offsetY;

    public float unitLength;

    void Start()
    {
        map = MapInfo.mapSize;
        map = PlayerPrefs.GetInt("mapSize");
        if (map == 0)
        {
            x = 40;
            y = 25;
        }
        if (map == 1)
        {
            x = 80;
            y = 50;
        }
        if (map == 2)
        {
            x = 120;
            y = 80;
        }
        Camera.main.GetComponent<CameraMouseMove>().x = x;
        Camera.main.GetComponent<CameraMouseMove>().y = y;

        unitLength = (useAsInnerCircleRadius) ? (radius / (Mathf.Sqrt(3) / 2)) : radius;
        
        offsetX = unitLength * Mathf.Sqrt(3);
        offsetY = unitLength * 1.5f;
        


        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {

                    Vector2 hexpos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexpos.x, hexpos.y, 0);
                Instantiate(spawnThis, pos, Quaternion.identity);

            
        }

        }

    }

    Vector2 HexOffset(int x, int y)
    {
        Vector2 position = Vector2.zero;
        spawnThis.GetComponent<HexNumber>().hex_x = x;
        spawnThis.GetComponent<HexNumber>().hex_y = y;
        if (y % 2 == 0)
        {
            position.x = x * offsetX;
            position.y = y * offsetY;
        }
        else
        {
            position.x = (x + 0.5f) * offsetX;
            position.y = y * offsetY;
        }

        return position;
        
    }


}