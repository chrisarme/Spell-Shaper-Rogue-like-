using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //public static PlayerScript instance;

    public Vector2 speed = new Vector2(40f, 40f);
    //public float jumpVelocity = 2f;
    //public Transform groundCheck;
    //public LayerMask whatIsGround;

    //private bool isGrounded;

    // Use this for initialization
    void Start ()
    {
        //instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButton("Jump") && isGrounded && Time.timeScale != 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpVelocity), ForceMode2D.Impulse);
            isGrounded = false;
        }*/

        // Check PassableGround tags


    }

    void FixedUpdate ()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, .1f, whatIsGround);
        
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(inputX * speed.x, inputY * speed.y);
    }
}
