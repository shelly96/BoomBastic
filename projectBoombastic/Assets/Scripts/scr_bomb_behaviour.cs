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
    [SerializeField] private float explosionPower = 150f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionDelay = 0.3f;
    private float explosionTime;
    private bool hitBoat = false;
    private bool pickedUp = false;
    private GameObject player;

    // Animation
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

        //picked up 
        if (pickedUp) {
            Debug.Log("Picked up!");
            transform.position = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 0.8f);
        }

        //bomb animation
        if (Time.time >= explosionTime - bombTimer* 0.5f)
        {
            animator.SetBool("exploding", true);
        }

        //bomb explosion
        if (Time.time >= explosionTime) {
            Explode();
        }

        //stop bomb on the boat
        if(hitBoat) {
            GetComponent<Rigidbody2D>().drag = 1.5f;
        }

        //destroy bombs that miss the water
        if(-transform.position.y > screenBounds.y +5) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch (collision.collider.name) {
            case "Wave_2":
                // TODO add sound 
                break;
            case "BoatBody":
                hitBoat = true;
                // TODO add sound
                break;
        }
    }

    private void Explode(){
        //play sound
        GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("explosion");

        // show explosion effect 
        GameObject ExplosionEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffect, 1.0f);

        //destroy bomb object
        Destroy(this.gameObject);
        Debug.Log("BOOM!");
        //TODO add sound  
        
        // get nearby objects
        Collider2D[] bombColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            // add force and damage
            foreach(Collider2D nearbyObject in bombColliders) {
                
                Debug.Log("Bomb hit: " + nearbyObject.name);
                
                //force direction for objects
                Vector2 direction = nearbyObject.transform.position - transform.position;

                string first_word = nearbyObject.name.Split('_')[0];
                
                switch (first_word) {
                    
                    case "Player":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<scr_player>().takeDamage();
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        Debug.Log("Player got some damage!");
                        break;
                    case "Boat":
                         //TODO add boat damage

                        nearbyObject.GetComponent<scr_boat>().takeDamage();
                        Debug.Log("Boat got some damage!");

                        break;
                    case "box":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        break;
                    case "treasure":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        break;
                    case "bomb":
                        //destroy other bomb
                        Debug.Log("BoomBoomBoom");
                        nearbyObject.GetComponent<scr_bomb_behaviour>().reduceExplosionTime(this.explosionTime);
                        break;
                }
            }
    }

    private void reduceExplosionTime (float newTime) {
        explosionTime = newTime + explosionDelay;
    }

    // show the Radius in the Bomb Prefab
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