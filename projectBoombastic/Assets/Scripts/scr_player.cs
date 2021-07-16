using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{

    //Health
    [SerializeField] public int healthPoints = 3;

    private Vector2 screenBounds;

    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 originalPos;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private bool inAir;


    // PickUp
    private Collider2D pickedUpObject;

    public Transform pickUpZone;
    [SerializeField] private float pickUpRange;
    [SerializeField] private float pickUpRadius;
    public LayerMask objectLayer;

    enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    };

    private Direction faceDirection = Direction.RIGHT;
    [SerializeField] private float throwStrength;



    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer.color = new Color(255, 255, 255);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        rb = GetComponent<Rigidbody2D>();
        originalPos = new Vector2(transform.position.x, transform.position.y);
        inAir = false;

    }

    // Update is called once per frame
    void Update()
    {
        // PickUp Object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pickedUpObject != null)
            {
                throwObject();
            }
            else
            {
                pickUpObject();
            }

        }

        // Pick Up Object Animation
        if (pickedUpObject == null)
        {
            animator.SetBool("holding", false);
        }
        else {
            animator.SetBool("holding", true);
        }

        // Detect when player has fallen off the boat 
        if (-transform.position.y > screenBounds.y +1) {
            //play sound
            if (!GameObject.Find("Boat").GetComponent<scr_boat>().isSinking)
            GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("water");

            healthPoints = 0;
            // this.gameObject.SetActive(false);
        }


        // Red sprite flash
        float tempColor = spriteRenderer.color.g;

        if (tempColor < 1)
        {
            tempColor += 1.5f * Time.deltaTime;

            spriteRenderer.color = new Color(255, tempColor, tempColor);
        }
        else {
            spriteRenderer.color = new Color(1, 1, 1);
        }

        // Reset Position (DEBUG)
        if (Input.GetKeyUp(KeyCode.F))
        {
            takeDamage();
        }

    }

    private void FixedUpdate()
    {
        move();
    }

    public void move()
    {

        float horizontalAxisRaw = Input.GetAxisRaw("Horizontal");
        float verticalAxisRaw = Input.GetAxisRaw("Vertical");

        // Vertical Movement
        if (verticalAxisRaw > 0 && !inAir)
        {
            inAir = true;
            animator.SetBool("inAir", inAir);

            // Reset velocity before each jump
            rb.velocity = Vector2.zero;

            // Apply jump force
            rb.AddForce(new Vector2(0, jumpForce* 1000 * Time.deltaTime));

        }

        // Horizontal Movement
        if (horizontalAxisRaw < 0)
        {
            // Set facing direction
            faceDirection = Direction.LEFT;

            // Flip sprite
            spriteRenderer.flipX = true;

            // Walking animation
            animator.SetFloat("movementSpeed", Mathf.Abs(horizontalAxisRaw));

            // Move player
            transform.position = transform.position + new Vector3(-1 * movementSpeed * Time.deltaTime, 0);

            // Move PickUpZone to the left
            pickUpZone.position = new Vector3(transform.position.x - pickUpRange, transform.position.y, 0);
        }
        else if (horizontalAxisRaw > 0)
        {
            // Set facing direction
            faceDirection = Direction.RIGHT;

            // Flip sprite
            spriteRenderer.flipX = false;

            // Walking animation
            animator.SetFloat("movementSpeed", Mathf.Abs(horizontalAxisRaw));

            // Move player
            transform.position = transform.position + new Vector3(1 * movementSpeed * Time.deltaTime, 0);

            // Move PickUpZone to the right
            pickUpZone.position = new Vector3(transform.position.x + pickUpRange, transform.position.y, 0);

        }
        else {

            //Reset Animation 
            animator.SetFloat("movementSpeed", Mathf.Abs(horizontalAxisRaw));

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.collider.name);
        string first_word = collision.collider.name.Split('_')[0];

        switch (first_word)
        {
            case "box":
                inAir = false;
                animator.SetBool("inAir", inAir);
                break;
            case "Boat":
                inAir = false;
                animator.SetBool("inAir", inAir);
                break;
            case "treasure":
                inAir = false;
                animator.SetBool("inAir", inAir);
                break;
        }


    }

    public void pickUpObject()
    {
        pickedUpObject = Physics2D.OverlapCircle(pickUpZone.position, pickUpRadius, objectLayer);

        if (pickedUpObject != null)
        {
            Debug.Log(pickedUpObject.name);

            // Pick up object
            string first_word = pickedUpObject.name.Split('_')[0];

            switch (first_word)
            {
                case "bomb":
                    pickedUpObject.GetComponent<scr_bomb_behaviour>().pickUp();
                    break;
            }

            pickedUpObject.GetComponent<Rigidbody2D>().mass = 0f;
            pickedUpObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        else
        {
            Debug.Log("no obj to pick up");

        }
    }



    public void throwObject()
    {
        // Determine throwing direction 
        Vector2 directionVector = new Vector2();

        switch (faceDirection)
        {
    
            case Direction.LEFT:
                directionVector = new Vector2(transform.position.x - throwStrength, transform.position.y+ throwStrength);
                break;
            case Direction.RIGHT:
                directionVector = new Vector2(transform.position.x + throwStrength, transform.position.y+ throwStrength);
                break;
        }


        // Throw object
        pickedUpObject.GetComponent<Rigidbody2D>().mass = 1f;
        pickedUpObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pickedUpObject.GetComponent<Rigidbody2D>().AddForce(directionVector, ForceMode2D.Impulse);
        pickedUpObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        pickedUpObject.GetComponent<scr_bomb_behaviour>().release();
        pickedUpObject = null;

    }

    public void takeDamage() {
        spriteRenderer.color = new Color(255, 0, 0);

        if (healthPoints > 0) {
            healthPoints--;
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pickUpZone.position, pickUpRadius);
    }


    public void reset()
    {
        transform.position = originalPos;
        rb.velocity = Vector2.zero;
    }


}
