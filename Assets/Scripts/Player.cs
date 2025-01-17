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
    public int jumpCount;
    public int jumpCountMax;
    private bool isJumping = false;
    private bool isGrounded = true;
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask groundObjects;

    //---player UI---
    public PlayerCanvasController pCanvasController;

    //---interactive stuff---
    public bool canInteract;
    public InteractiveObject currentInteractive;

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
        if (Input.GetKeyDown(KeyCode.Return) && canInteract)
        {
            currentInteractive.FeedThroughMethod();
            pCanvasController.HideTalkText();
        }
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    //physics handling
    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        if (isJumping)
        {
            jumpCount--;
            rb.AddForce(new Vector2(0f, jumpForce));
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
        if (collision.gameObject.tag == "Interactive")
        {
            canInteract = true;
            currentInteractive = collision.gameObject.GetComponent<InteractiveObject>();
            pCanvasController.ShowTalkText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        currentInteractive = null;
        if (pCanvasController.talkText.IsActive()) pCanvasController.HideTalkText();
    }
}
