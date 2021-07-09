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
    private float explosionTime;
    private bool hitBoat = false;
    private bool pickedUp = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed*shotAngle, -speed);
        explosionTime = Time.time + bombTimer;

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

        //bomb explosion
        if(Time.time >= explosionTime) {
            Explode();
        }

        //stop bomb on the boat
        if(hitBoat) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -screenBounds.y);
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
            case "Boat":
                hitBoat = true;
                // TODO add sound
                break;
        }
    }

    private void Explode(){

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
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        Debug.Log("Player got some damage!");
                        //player damage
                        scr_game_controller.damage = true;
                        break;
                    case "Boat":
                        //TODO add boat damage
                        Debug.Log("Boat got some damage!");
                        scr_game_controller.damage = true;
                        break;
                    case "box":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        break;
                    case "bomb":
                        //destroy other bomb
                        Debug.Log("BoomBoomBoom");
                        nearbyObject.GetComponent<scr_bomb_behaviour>().Explode();
                        break;
                }
            }
    }

    // show the Radius in the Bomb Prefab
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void pickUp() {
        pickedUp = true;
    }

    public void release() {
        pickedUp = false;
    }

}