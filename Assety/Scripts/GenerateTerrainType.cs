using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateTerrainType : MonoBehaviour {

    private float chance;
    public Sprite earthSprite;
    public Sprite gloothSprite;

    void Start () {
        chance = Random.value;


        //Spawn planety typu Ziemia
        if(chance <= 0.05f)
        {
            PlanetEarth add = gameObject.AddComponent<PlanetEarth>();
            GameObject go = new GameObject("Earth");
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(0, 0, -1);
            go.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = earthSprite;
            
        }
        //Spawn planety typu Ziemia
        else if (chance >= 0.05f && chance <= 0.1f)
        {
            PlanetGlooth add = gameObject.AddComponent<PlanetGlooth>();
            GameObject go = new GameObject("Glooth");
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(0, 0, -1);
            go.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = gloothSprite;

        }
        else
        {
            Vacuum add = gameObject.AddComponent<Vacuum>();
        }

    }
	

}
