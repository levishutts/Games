using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float speed = 10;

    public float sensitivity = 1;
    public float maxSpeed = 20;
    public float minSpeed = 5;
    private float totalTime;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        Vector3 input = new Vector3(-moveHorizontal, moveVertical, 0.0f);

        rb.transform.Rotate(sensitivity * input);
        rb.transform.Rotate(0, 0, -rb.transform.eulerAngles.z);

        if (Input.GetKey(KeyCode.Space))
        {
            if (speed < maxSpeed)
                speed = (Time.deltaTime * 100) + speed;
        }
        else
            if (speed > minSpeed)
                speed = speed - (Time.deltaTime * 100);

        rb.velocity = (transform.forward * speed);
    }
}