using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour{
    [SerializeField] float moveSpeed = 0.9f;
    [SerializeField] float jumpForce = 300f;  // Jump force
    private Rigidbody rb;                    // Rigidbody component
    private bool isGrounded; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        MovePlayer1();
        Jump(); 
    }

    void MovePlayer1 () {

        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float yValue = 0;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate (xValue, yValue, zValue);
    }

        void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)  // Check for jump input and if the player is grounded
        {
            rb.AddForce(Vector3.up * jumpForce);        // Apply a force upwards
            isGrounded = false;                         // Set isGrounded to false after jumping
        }
    }

        private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))     // Make sure to tag your ground object with "Ground"
        {
            isGrounded = true;                         // Set isGrounded to true when touching the ground
        }
    }
}
