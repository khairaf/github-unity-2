using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        // using input manager from unity/can handle joystick too
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && IsGounded())
        {
            Jupm();
        }
        // manual
        // if (Input.GetKeyDown("space"))
        // {
        //     rb.velocity = new Vector3(0, 5, 0);
        // }

        // if (Input.GetKey("up"))
        // {
        //     rb.velocity = new Vector3(0, 0, 5);
        // }

        // if (Input.GetKey("right"))
        // {
        //     rb.velocity = new Vector3(5, 0, 0);
        // }

        //  if (Input.GetKey("left"))
        // {
        //     rb.velocity = new Vector3(-5, 0, 0);
        // }

        //  if (Input.GetKey("down"))
        // {
        //     rb.velocity = new Vector3(0, 0, -5);
        // }
    }

    void Jupm()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            // make player to die
            Debug.Log("enemy die");
            Destroy(collision.transform.parent.gameObject);
            Jupm();
        }
    }

    bool IsGounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);

    }
}
