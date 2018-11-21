using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public Transform block;
    public GameObject Player;

    public Canvas YouLoseCanvas;
    public Canvas WaitingCanvas;

    public float waitTime = 2f;

    private float timer;
    private float stableTime;
    private bool stable;

    private int blockCount = 2;
    private char orientation = 'z';
    private GameObject current;
    private Transform lastBlock;
    private Vector3 tempBlock;
    private Vector3 stablePos;
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

        YouLoseCanvas.enabled = false;
        WaitingCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        stableTime += Time.deltaTime;
        if (stableTime > 2)
        {
            if ((lastBlock.position - stablePos).sqrMagnitude < 0.001f)
            {
                Rigidbody rb = Player.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                stable = true;
                WaitingCanvas.enabled = false;
            }
            stablePos = lastBlock.position;
            stableTime = 0;
        }
    }

    //Check collisions with the floor
    void OnCollisionEnter(Collision collision)
    {
        //Reset Player position if they hit the floor
        if (collision.gameObject.tag == "Player")
        {
            Player.transform.position = new Vector3(-50, 10, -50);
            Player.transform.rotation = Quaternion.Euler(0, 45, 0);
            Rigidbody rb = Player.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        //Stack blocks as they hit the floor
        if (collision.gameObject.tag == "block")
        { 
            if (timer < waitTime)
            {
                return;
            }

            if (stable == false)
            {
                WaitingCanvas.enabled = false;
                YouLoseCanvas.enabled = true;
                this.enabled = false;
            }

            stable = false;
            WaitingCanvas.enabled = true;

            //reset Player position and pause movement until tower is stable
            Player.transform.position = new Vector3(-50, 10, -50);
            Player.transform.rotation = Quaternion.Euler(0,45,0);
            Rigidbody rb = Player.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezeAll;

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
            stablePos = lastBlock.position;

            stableTime = 0;
        }
    }
}
