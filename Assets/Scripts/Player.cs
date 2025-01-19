using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //---gm---
    public GM gm;
    public GameObject gmPrefab;

    //---movement---
    private Rigidbody2D rb;
    public float speed = 3f;
    public float moveDirection;

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

    //---questline stuff---
    bool canGrabKey;
    bool canGrabBandana;
    bool canGrabBoots;
    
    //---checks for quest diversion---
    bool hasGrabbedKey;
    bool hasGrabbedBandana;
    bool hasGrabbedBoots;

    public string[] questDialogue;

    //---animation---
    public Animator anim;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
        }
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
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        }
        //inputs
        ProcessInputs();
        if (gm.hasObtainedDragon && jumpCountMax == 1) jumpCountMax = 2;
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (canInteract)
            {
                currentInteractive.FeedThroughMethod();
                rb.velocity = new Vector2(0, 0);
                anim.SetTrigger("Interact");
                if (pCanvasController.talkText.IsActive()) pCanvasController.HideTalkText();
            }
            if (canGrabKey && !hasGrabbedKey)
            {
                if (pCanvasController.grabText.IsActive()) pCanvasController.HideGrabText();
                gm.UIController.RunKeyLine();
                currentInteractive.FeedThroughMethod();
                return;
            }
            if (canGrabBandana)
            {
                if (pCanvasController.grabText.IsActive()) pCanvasController.HideGrabText();
            }
            if (canGrabBoots)
            {
                if (pCanvasController.grabText.IsActive()) pCanvasController.HideGrabText();
            }
            if (gm.hasObtainedDog && canDig)
            {
                //Debug.Log("Doggy dug something up!");
                currentInteractive.FeedThroughMethod();
                //add later
                //anim.SetTrigger("Dig");
                if (pCanvasController.digText.IsActive()) pCanvasController.HideDigText();
            }
            if (gm.hasObtainedBear)
            {
                //Debug.Log("Doggy dug something up!");
                currentInteractive.FeedThroughMethod();
                //add later
                //anim.SetTrigger("Dig");
                if (pCanvasController.breakText.IsActive()) pCanvasController.HideBreakText();
            }
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
        GameObject _coll = collision.gameObject;
        GameObject _mainObject;
        if(_coll.transform.parent != null) _mainObject = _coll.transform.parent.gameObject;
        if (_coll.tag == "LoadZone")
        {
            LoadZone loadScript = _coll.GetComponent<LoadZone>();
            if (loadScript.passPositionThru) gm.LoadSceneAndPosition(loadScript.buildIndexToLoad, loadScript.positionToLoadAt, loadScript.flipX);
            else gm.LoadScene(loadScript.buildIndexToLoad);
        }
        //cherry collecting!
        else if (_coll.tag == "Cherry")
        {
            gm.AddCherry();
            _coll.GetComponent<Cherry>().OnPickup();
            pCanvasController.CherryGet();
            anim.SetTrigger("Yippee");
        }
        else if (_coll.tag == "Interactive")
        {
            canInteract = true;
            currentInteractive = _coll.GetComponent<InteractiveObject>();
            pCanvasController.ShowTalkText();
        }
        else if (_coll.tag == "Key")
        {
            canGrabKey = true;
            gm.finishedDogQuest = true;
            currentInteractive = _coll.GetComponent<InteractiveObject>();
            pCanvasController.ShowGrabText();
        }
        else if (_coll.tag == "Bandana")
        {
            canGrabBandana = true;
            gm.finishedBearQuest = true;
            currentInteractive = _coll.GetComponent<InteractiveObject>();
            pCanvasController.ShowGrabText();
        }
        else if (_coll.tag == "Boots")
        {
            canGrabBoots = true;
            gm.finishedDragonQuest = true;
            currentInteractive = _coll.GetComponent<InteractiveObject>();
            pCanvasController.ShowGrabText();
        }
        else if (_coll.tag == "DigSpot" && gm.hasObtainedDog)
        {
            canDig = true;
            currentInteractive = collision.gameObject.GetComponent<InteractiveObject>();
            pCanvasController.ShowDigText();
        }
        else if (_coll.tag == "BreakableWall" && gm.hasObtainedBear)
        {
            canCrush = true;
            currentInteractive = collision.gameObject.GetComponent<InteractiveObject>();
            pCanvasController.ShowBreakText();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        currentInteractive = null;
        if (pCanvasController.talkText.IsActive()) pCanvasController.HideTalkText();
        if (pCanvasController.digText.IsActive()) pCanvasController.HideDigText();
        if (pCanvasController.breakText.IsActive()) pCanvasController.HideBreakText();
        if (canGrabKey)
        {
            canGrabKey = false;
            pCanvasController.HideGrabText();
        }
        if (canGrabBandana)
        {
            canGrabBandana = false;
            pCanvasController.HideGrabText();
        }
        if (canGrabBoots)
        {
            canGrabBoots = false;
            pCanvasController.HideGrabText();
        }

    }

    /*private IEnumerator Land()
    {
        yield return new WaitForSeconds(.2f);
        anim.SetTrigger("Land");
    }*/
}
