using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {

    private Transform bar;
    private float hp;
    private float maxHp;
    private float barSize;
    private float scale_x;
    private float sca_y;
    private int id;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Transform>();
        maxHp = GetComponentInParent<ShipStats>().hp;
        id = GetComponentInParent<ShipStats>().shipID;
        if (id == 0)
        {
            scale_x = 0.5f;
        }
        else if(id == 1)
        {
            scale_x = 0.7f;
        }
        else if (id == 2)
        {
            scale_x = 0.7f;
        }
    }
        //scala
        /*
         id 0: 0.5,  0.5
         if 1: 0.7   0.6
         */
	// Update is called once per frame
	void Update () {
        hp = GetComponentInParent<ShipStats>().hp;
        barSize = (hp / maxHp) * scale_x;

        bar.localScale = new Vector3(barSize, bar.localScale.y, bar.localScale.z);
	}
}
