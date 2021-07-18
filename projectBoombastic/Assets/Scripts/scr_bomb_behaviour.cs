using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomb_behaviour : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float shotAngle = 6f;
    [SerializeField] private float bombTimer = 5f;
    [SerializeField] private float explosionRadius = 2f; 
    [SerializeField] private float explosionPower = 275f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionDelay = 0.3f;
    private float explosionTime;
    private bool hitBoat = false;
    private bool firstContact = true;
    private bool pickedUp = false;
    private GameObject player;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed*shotAngle, -speed);
        explosionTime = Time.time + bombTimer;

        animator = GetComponent<Animator>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        // picked up 
        if (pickedUp) {
            //Debug.Log("Picked up!");
            transform.position = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 0.8f);
        }

        // bomb blinking animation 
        if (Time.time >= explosionTime - bombTimer * 0.5f) {
            animator.SetBool("exploding", true);
        }

        // bomb explosion
        if (Time.time >= explosionTime) {
            Explode();
        }

        // stop bomb on the boat
        if(hitBoat) {
            GetComponent<Rigidbody2D>().drag = 1.5f;
        }

        // destroy bombs under the sceene
        if(-transform.position.y > screenBounds.y) {
            // play sound if the boat isn't sinking
            if (!GameObject.Find("Boat").GetComponent<scr_boat>().isSinking)
                GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("water");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // get first word from a name
        string first_collision_word = collision.collider.name.Split('_')[0];

        if ((first_collision_word == "Boat" || first_collision_word == "box" || first_collision_word == "treasure") && pickedUp == false ) {
            hitBoat = true;
            // play sound for the first time
            if (firstContact ) {
                GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("wood");
                firstContact = false;
            }
            
        }
        
    }

    private void Explode() {
        
        // play explosion sound
        GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("explosion");

        // show explosion effect 
        GameObject ExplosionEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffect, 1.0f);

        // destroy bomb object
        Destroy(this.gameObject);
        //Debug.Log("BOOM!");
        
        // get nearby objects
        Collider2D[] bombColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            // add force and damage
            foreach (Collider2D nearbyObject in bombColliders) {
                
                //Debug.Log("Bomb hit: " + nearbyObject.name);
                
                // force direction for objects
                Vector2 direction = nearbyObject.transform.position - transform.position;

                // get first word from a name
                string first_word = nearbyObject.name.Split('_')[0];
                
                // add force or damage to the objects 
                switch (first_word) {
                    
                    case "Player":
                        nearbyObject.GetComponent<scr_player>().takeDamage();
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * explosionPower);
                        //Debug.Log("Player got some damage!");
                        break;
                    case "Boat":
                        nearbyObject.GetComponent<scr_boat>().takeDamage();
                        //Debug.Log("Boat got some damage!");
                        break;
                    case "box":
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        break;
                    case "treasure":
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        break;
                    case "bomb":
                        //destroy other bomb
                        nearbyObject.GetComponent<scr_bomb_behaviour>().reduceExplosionTime(this.explosionTime);
                        break;
                }
            }

    }

    // reduce explosionTime of hit bombs
    private void reduceExplosionTime (float newTime) {
        explosionTime = newTime + explosionDelay;
    }

    // show the radius in the Bomb Prefab
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void pickUp() {
        pickedUp = true;
        hitBoat = false;
        GetComponent<Rigidbody2D>().drag = 0;
    }

    public void release() {
        pickedUp = false;
        GetComponent<Rigidbody2D>().drag = 0;
    }

}