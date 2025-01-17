using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //---gm---
    public GM gm;

    //---movement---
    private Rigidbody2D rb;
    public float speed = 3f;
    public float moveDirection;
    bool facingRight = true;

    //---jumping---
    public float jumpForce = 500f;
    private int jumpCount;
    public int jumpCountMax;
    private bool isJumping = false;
    private bool isGrounded = true;
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask groundObjects;

    //---player UI---
    public PlayerCanvasController pCanvasController;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        pCanvasController = this.GetComponentInChildren<PlayerCanvasController>();
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }
    private void Start()
    {
        jumpCount = jumpCountMax;
    }

    void Update()
    {
        //inputs
        ProcessInputs();
        //flipping
        FlipAndAnimate();
    }

    //called multiple times a frame for smoother movement
    private void FixedUpdate()
    {
        //ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        //jumping
        if (isGrounded) jumpCount = jumpCountMax;
        //calls movement method
        Movement();
    }

    //processes player inputs
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0) isJumping = true;
    }

    //physics handling
    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }

    //for future use
    private void FlipAndAnimate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    public void FlipCharacter()
    {
        //does nothing... for now
    }

    //ground checking
    void OnTriggerEnter2D(Collider2D collision)
    {
        //cherry collecting!
        if (collision.gameObject.tag == "Cherry")
        {
            gm.AddCherry();
            Destroy(collision.gameObject);
            pCanvasController.CherryGet();
        }
    }
}
