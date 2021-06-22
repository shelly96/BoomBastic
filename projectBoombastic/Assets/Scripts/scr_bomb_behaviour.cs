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
    //[SerializeField] private GameObject explosionEffect;
    private float explosionTime;
    private bool hitBoat = false; 

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed*shotAngle, -speed);
        explosionTime = Time.time + bombTimer;
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(collision.collider.name);

        switch (collision.collider.name) {
            case "Wave_2":
                Destroy(this.gameObject);
                // TODO add sound 
                break;
            case "Boat":
                hitBoat = true;
                // TODO add sound
                break;
        }
    }

    private void Explode(){

        // show effect with no rotation
        //GameObject ExplosionEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //Destroy(ExplosionEffect, bombTimer+2);

        // get nearby objects
        Collider2D[] bombColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            // add force and damage
            foreach(Collider2D nearbyObject in bombColliders) {
                
                Debug.Log("Bomb hit: " + nearbyObject.name);
                
                //force direction for objects
                Vector2 direction = nearbyObject.transform.position - transform.position;
                
                switch (nearbyObject.name) {
                    case "Player":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        Debug.Log("Player got some damage!");
                        //TODO add player damage
                        break;
                    case "Boat":
                        //TODO add boat damage
                        Debug.Log("Boat got some damage!");
                        break;
                    case "Box":
                        //add force, but boat has no Rigidbody so you get an error
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionPower);
                        Debug.Log("Box was moved by the bomb");
                        break;
                }
            }

        //destroy bomb object
        Destroy(this.gameObject);
        Debug.Log("BOOM!");
        //TODO add sound  
    }

    // show the Radius in the Bomb Prefab
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
