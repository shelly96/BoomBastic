using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_movement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 originalPos;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private bool inAir;


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

    public void move() {

        float horizontalAxis = Input.GetAxis("Horizontal");

        // Vertical Movement
        if (Input.GetKey(KeyCode.Space) && !inAir) {
            inAir = true;
            rb.AddForce(new Vector2(0, jumpForce*100));
        }

        // Horizontal Movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-1 * movementSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(1 * movementSpeed * Time.deltaTime, 0);
        }

        // Reset Position (DEBUG)
        if (Input.GetKey(KeyCode.R)) 
        {
            reset();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);

        switch (collision.collider.name) {
            case "Boat":
                inAir = false;
                break;
            case "Wave_2":
                // TODO add sound 
                // TODO add player damage or GameOver screen?
                reset();
                break;
        }

        
    }

    public void reset()
    {
        transform.position = originalPos;
        rb.velocity = Vector2.zero;
    }


}
