using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public Transform block;

    public float waitTime = 1f;

    private float timer;

    private int blockCount;
    private int height;
    private char orientation = 'x';
    
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
        timer += Time.deltaTime;
    }

    void wait()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3 (0f, 10, -50);
            collision.gameObject.transform.rotation = Quaternion.identity;
        }

        if (collision.gameObject.tag == "block")
        {
            if (timer < waitTime)
                return;
            Debug.Log("block collision with floor " + orientation);
            Debug.Log("blockCount " + blockCount);
            Debug.Log("height " + height);
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (orientation == 'x')
            {
                collision.gameObject.transform.position = new Vector3(2 * blockCount - 2, height + 16.5f, 0);
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                blockCount++;
            }

            if (orientation == 'z')
            {
                collision.gameObject.transform.position = new Vector3(0, height + 16.5f, 2 * blockCount - 2);
                collision.gameObject.transform.rotation = Quaternion.identity;
                blockCount++;
            }
            if (blockCount == 3)
            {
                blockCount = 0;
                if (orientation == 'x')
                    orientation = 'z';
                else
                    orientation = 'x';
                ++height;
            }

        }

    }
}
