using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public Transform block;

    public float waitTime = 1f;

    private float timer;

    private int blockCount = 2;
    private char orientation = 'z';
    private GameObject current;
    private Transform lastBlock;
    private Vector3 tempBlock;
    private Quaternion tempRot;
    private int blocks;

    // Use this for initialization
    void Start()
    {
        //build tower
        int count = 0;
        Transform clone = null;
        for (int y = 0; y < 16; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                ++count;
                clone = Instantiate(block, new Vector3(2 * x, y + 0.5f, 0), Quaternion.Euler(0, 90, 0));
                clone.name = "block" + count;
            }
                
            y++;
            for (int z = -1; z < 2; z++)
            {
                ++count;
                clone = Instantiate(block, new Vector3(0, y + 0.5f, 2 * z), Quaternion.identity);
                clone.name = "block" + count;
            }
                
        }
        lastBlock = clone;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        //use lastblock position for checking stablization
        Debug.Log(lastBlock.position);
    }

    void wait()
    {

    }

    //Check collisions with the floor
    void OnCollisionEnter(Collision collision)
    {
        //Reset Player position if they hit the floor
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3 (0f, 10, -50);
            collision.gameObject.transform.rotation = Quaternion.identity;
        }

        //Stack blocks as they hit the floor
        if (collision.gameObject.tag == "block")
        {
            if (timer < waitTime)
                return;

            //remove any physics from the block
            current = collision.gameObject;
            current.GetComponent<Rigidbody>().velocity = Vector3.zero;
            current.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            //set temporary values from last block placed
            tempBlock = lastBlock.position;
            tempRot = lastBlock.rotation;

            //change orientation and height of block for new row
            if (blockCount == 2)
            {
                if (orientation == 'x')
                {
                    orientation = 'z';
                    tempBlock += (Vector3.left * 2) + (Vector3.back * 4);
                    tempRot = Quaternion.identity;
                }
                else
                {
                    orientation = 'x';
                    tempBlock += (Vector3.back * 2) + (Vector3.left * 4);
                    tempRot = Quaternion.Euler(0, 90, 0);
                }
                tempBlock += (Vector3.up * 1.3f);
                blockCount = -1;
            }

            //place block
            if (orientation == 'x')
            {
                tempBlock += (Vector3.right * 2);
                current.transform.rotation = tempRot;
                current.transform.position = tempBlock;
            }
            else
            {
                tempBlock += (Vector3.forward * 2);
                current.transform.rotation = tempRot;
                current.transform.position = tempBlock;
            }

            blockCount++;
            lastBlock = collision.gameObject.transform;
        }
    }
}
