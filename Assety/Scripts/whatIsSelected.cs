using UnityEngine;
using System.Collections;


public enum State
{
    walking, idle, attacking, eating, sleeping
}


public class whatIsSelected : MonoBehaviour {


    public Transform selectedObject;
    public Transform lastSelected;

    public float distance;

    public State current;

   public void ChangeObject(Transform trans)
    {
        if(selectedObject != null)
        {
            lastSelected = selectedObject;
           
        }
        selectedObject = trans;

        if (lastSelected != null)
        {

        distance = Vector2.Distance(selectedObject.transform.position, lastSelected.transform.position);
        }
    }

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
