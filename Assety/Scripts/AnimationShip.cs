using UnityEngine;
using System.Collections;

public class AnimationShip : MonoBehaviour {

    public GameObject startTeleportAnimation; 
    public GameObject endTeleportAnimation;
    public GameObject expolsionAnimation;
    public GameObject expolsionSmall;
    public GameObject explosionBig;

    public static AnimationShip instance;

    void Start()
    {
        instance = this;


    }

    public void StartTeleport(Vector3 position)
    {
        Instantiate(startTeleportAnimation, position, Quaternion.identity);

    }

    public void EndtTeleport(Vector3 position)
    {
        Instantiate(endTeleportAnimation, position, Quaternion.identity);
    }

    public void Explosion(Vector3 position)
    {
        Instantiate(expolsionAnimation, position, Quaternion.identity);
    }
    public void DeathAnimation(Vector3 position)
    {
        GameObject newObject = Instantiate(expolsionAnimation, position, Quaternion.identity) as GameObject;
        newObject.transform.localScale = new Vector3(3, 3, 3);
    }
}

