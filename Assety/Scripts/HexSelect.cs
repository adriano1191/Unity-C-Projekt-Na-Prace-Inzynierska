using UnityEngine;
using System.Collections;

public class HexSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

if (Input.GetMouseButtonDown(0))
        {
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Map")
                {
                    Transform selected = hit.transform;
                   // gc.unselectAll();
                    selected.SendMessage("selectMapHex");
                }
            }
        }
    }
}
