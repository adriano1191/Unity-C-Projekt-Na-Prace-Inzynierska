using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    private float minSize = 3;
    private float maxSize = 10;
    private float size = 5;
    private float sensitivity = 5f;
#pragma warning disable CS0169 // The field 'CameraZoom.cameraComponent' is never used
    private Camera cameraComponent;
#pragma warning restore CS0169 // The field 'CameraZoom.cameraComponent' is never used

    void Update()
    {
        size -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        if(size > maxSize)
        {
            size = maxSize;
        }
        if(size < minSize)
        {
            size = minSize;
        }
       // Camera.main.fieldOfView = fov;
        Camera.main.orthographicSize = size;




    }
}
