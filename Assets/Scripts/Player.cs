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

    //---abilities---
    public bool canDig;
    public bool canCrush;
    public bool canFly;

    //---animation---
    public Animator anim;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        pCanvasController = this.GetComponentInChildren<PlayerCanvasController>();
        anim = GetComponentInChildren<Animator>();
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
    }

    //called multiple times a frame for smoother movement
    private void FixedUpdate()
    {
        //ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        //jumping
        if (isGrounded)
        {
            jumpCount = jumpCountMax;
            anim.SetBool("IsGrounded", true);
        }
        else anim.SetBool("IsGrounded", false);
        //calls movement method
        Movement();

        if (rb.velocity.x == 0 && !isJumping)
        {
            anim.SetBool("IsWalking", false);
        }
    }

    //processes player inputs
    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canInteract)
        {
            currentInteractive.FeedThroughMethod();
            anim.SetTrigger("Interact");
            pCanvasController.HideTalkText();
        }
        if (Input.GetKeyDown(KeyCode.Return) && gm.hasObtainedDog)
        {
            //Debug.Log("Doggy dug something up!");
            currentInteractive.FeedThroughMethod();
            //add later
            //anim.SetTrigger("Dig");
            pCanvasController.HideDigText();
        }
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            isJumping = true;
            anim.SetTrigger("Jump");
        }
        //if (rb.velocity.x > 0 || rb.velocity.x < 0) anim.SetBool("IsWalking", true);
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
        else anim.SetBool("IsWalking", true);
        isJumping = false;
        FlipAndAnimate();
    }
    //flipping
    private void FlipAndAnimate()
    {
        if (rb.velocity.x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
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
            anim.SetTrigger("Yippee");
        }
        else if (collision.gameObject.tag == "Interactive")
        {
            canInteract = true;
            currentInteractive = collision.gameObject.GetComponent<InteractiveObject>();
            pCanvasController.ShowTalkText();
        }
        else if (collision.gameObject.tag == "DigSpot" && gm.hasObtainedDog)
        {
            canDig = true;
            currentInteractive = collision.gameObject.GetComponent<InteractiveObject>();
            pCanvasController.ShowDigText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        currentInteractive = null;
        if (pCanvasController.talkText.IsActive()) pCanvasController.HideTalkText();
        if (pCanvasController.digText.IsActive()) pCanvasController.HideDigText();
    }

    /*private IEnumerator Land()
    {
        yield return new WaitForSeconds(.2f);
        anim.SetTrigger("Land");
    }*/
}
