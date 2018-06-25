using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;
    public GameObject sun;
    private Rigidbody sunbody;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sun = GameObject.Find("Sun");
        sunbody = sun.GetComponent("Rigidbody") as Rigidbody;
        rb.AddForce(0.0f, 0.0f, 150.0f);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        Vector3 diff = sun.transform.position - rb.transform.position;
        float gravitationalForce = 1.25f;
        rb.AddForce(diff.normalized * gravitationalForce, ForceMode.Acceleration);
    }
}
