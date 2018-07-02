using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public Transform block;

    // Use this for initialization
    void Start()
    {
        for (int y = 0; y < 16; y++)
        {
            for (int x = -1; x < 2; x++)
                Instantiate(block, new Vector3(2 * x, y + 0.5f, 0), Quaternion.Euler(0, 90, 0));
            y++;
            for (int z = -1; z < 2; z++)
                Instantiate(block, new Vector3(0, y + 0.5f, 2 * z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3 (0f, 10, -50);
            collision.gameObject.transform.rotation = Quaternion.identity;
        }
            
    }
}
