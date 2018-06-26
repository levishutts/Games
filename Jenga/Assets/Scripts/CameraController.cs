using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public float distance;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = player.transform.position - (player.transform.forward * distance);
        transform.LookAt(player.transform);
    }
}