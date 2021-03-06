using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHeart : MonoBehaviour
{
    public float speed = 2.0f; 
    private Rigidbody2D rb;

    public static int heart = 0;
    private Vector2 screenBounds;

    [SerializeField] private GameObject plusScore;
    private GameObject player; 

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); 
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height,0.0f));
        player = GameObject.Find("Player");
    }
  
    void Update(){
        if(transform.position.x < -screenBounds.x*2){
            Destroy(this.gameObject);
        }
    }

    // once the collider hits the player, the function is triggered
    private void OnTriggerEnter2D(Collider2D other){

        Debug.Log("Player");
        if(other.transform.tag == "Player"){

            // heathpoints erhöhen
            if(player.GetComponent<scr_player>().healthPoints<3){
                player.GetComponent<scr_player>().healthPoints += 1;
            }

            Destroy(this.gameObject);

            //play sound
            GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("heal");

        }
    }

}
