using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public float distance;
    
    Vector3 offset = new Vector3(0, 0.3f, 0);

    void Start()
    {
        if (Display.displays.Length > 1)
            Display.displays[2].Activate();
    }

    void LateUpdate()
    {
        transform.position = player.transform.position - (player.transform.forward * distance) + offset;
        transform.LookAt(player.transform);
        transform.position += offset;
    }
}