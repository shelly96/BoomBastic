using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{

    private Rigidbody2D rb;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = new Vector2(transform.position.x, transform.position.y);
        inAir = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        move();
    }

    public void move()
    {

        float horizontalAxis = Input.GetAxis("Horizontal");

        // Vertical Movement
        if (Input.GetKey(KeyCode.W) && !inAir)
        {
            inAir = true;

            // Reset velocity before each jump
            rb.velocity = Vector2.zero;

            // Apply jump force
            rb.AddForce(new Vector2(0, jumpForce * 100));
        }

        // Horizontal Movement
        if (Input.GetKey(KeyCode.A))
        {
            // Set facing direction
            faceDirection = Direction.LEFT;

            // Move player
            transform.position = transform.position + new Vector3(-1 * movementSpeed * Time.deltaTime, 0);

            // Move PickUpZone to the left
            pickUpZone.position = new Vector3(transform.position.x - pickUpRange, transform.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Set facing direction
            faceDirection = Direction.RIGHT;

            // Move player
            transform.position = transform.position + new Vector3(1 * movementSpeed * Time.deltaTime, 0);

            // Move PickUpZone to the right
            pickUpZone.position = new Vector3(transform.position.x + pickUpRange, transform.position.y, 0);

        }
        else if (Input.GetKey(KeyCode.W))
        {
            // Set facing direction
            faceDirection = Direction.UP;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Set facing direction
            faceDirection = Direction.DOWN;
        }


        // PickUp Item
        if (Input.GetKeyUp(KeyCode.Space))
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

        // Reset Position (DEBUG)
        if (Input.GetKey(KeyCode.R))
        {
            reset();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.collider.name);

        switch (collision.collider.name)
        {
            case "box_large(Clone)":
                inAir = false;
                break;
            case "box_medium(Clone)":
                inAir = false;
                break;
            case "box_small(Clone)":
                inAir = false;
                break;
            case "BoatBody":
                inAir = false;
                break;
            case "Wave_2":
                // TODO add sound 
                // TODO add player damage or GameOver screen?
                reset();

                //reduce lives
                scr_game_controller.damage = true;

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

            switch (pickedUpObject.name)
            {
                case "bomb_1(Clone)":
                    pickedUpObject.GetComponent<scr_bomb_behaviour>().pickUp();
                    break;
                case "bomb_2(Clone)":
                    pickedUpObject.GetComponent<scr_bomb_behaviour>().pickUp();
                    break;
             /*   case "box_small(Clone)":
                    pickedUpObject.GetComponent<scr_box_behavior>().pickUp();
                    break;
                case "box_medium(Clone)":
                    pickedUpObject.GetComponent<scr_box_behavior>().pickUp();
                    break;
                case "box_large(Clone)":
                    pickedUpObject.GetComponent<scr_box_behavior>().pickUp();
                    break;*/
            }

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
          
          /*  case Direction.UP:
                directionVector = new Vector2(transform.position.x, transform.position.y - 10);
                break;
            case Direction.DOWN:
                directionVector = new Vector2(transform.position.x, transform.position.y + 10);
                break;*/
            case Direction.LEFT:
                directionVector = new Vector2(transform.position.x - 10, transform.position.y+10);
                break;
            case Direction.RIGHT:
                directionVector = new Vector2(transform.position.x + 10, transform.position.y+10);
                break;
        }


        // Throw object
        pickedUpObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        pickedUpObject.GetComponent<Rigidbody2D>().AddForce(directionVector, ForceMode2D.Impulse);
        pickedUpObject.GetComponent<scr_bomb_behaviour>().release();
        pickedUpObject = null;

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
