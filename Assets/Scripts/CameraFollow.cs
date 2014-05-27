using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private static GameObject player;
    private static GameObject square;
    private static GameObject heavyCircle;
    private static GameObject lightCircle;
    private float cameraDistance = -10.0f;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        square = player.transform.Find("square").gameObject;
        heavyCircle = player.transform.Find("heavyCircle").gameObject;
        lightCircle = player.transform.Find("lightCircle").gameObject;
    }
    
    void Update()
    {
        if (square.activeSelf)
        {
            transform.position = new Vector3(square.transform.position.x, square.transform.position.y, cameraDistance);
        }
        else if (heavyCircle.activeSelf)
        {
            transform.position = new Vector3(heavyCircle.transform.position.x, heavyCircle.transform.position.y, cameraDistance);
        }
        else if (lightCircle.activeSelf)
        {
            transform.position = new Vector3(lightCircle.transform.position.x, lightCircle.transform.position.y, cameraDistance);
        }
        else
        {
            // do nothing
        }
    } 
}
