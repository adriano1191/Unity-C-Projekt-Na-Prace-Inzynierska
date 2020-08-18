using UnityEngine;
using System.Collections;

public class ShipRange : MonoBehaviour {

    public int Range;
    public bool inRange = false;

    void Start()
    {
        //SeeRange(Range);
    }

    void Update()
    {

    }

    public void SeeRange(int setRange)
    {
        if (setRange == 0)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.4f;
        }
        else if (setRange == 1)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 1.2f;
        }
        else if (setRange == 2)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 2.6f;
        }
        else if (setRange >= 3)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 3.6f;
        }
    }

    void OnTriggerEnter2D(Collider2D collid)
    {
        
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = true;

        }
    }
    void OnTriggerExit2D(Collider2D collid)
    {
        
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = false;
        }
    }

    void OnTriggerStay2D(Collider2D collid)
    {
        
        if (collid.tag == "ShipRange" && collid.GetType() == typeof(CircleCollider2D))
        {

            inRange = true;

        }
        else
        {

            inRange = false;
        }
    }
}
