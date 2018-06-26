using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        rb.velocity = (transform.forward * speed);

        Vector3 input = new Vector3(-moveHorizontal, moveVertical, 0.0f);
    
        rb.transform.Rotate(input);
        rb.transform.Rotate(0, 0, -rb.transform.eulerAngles.z);
    }
}