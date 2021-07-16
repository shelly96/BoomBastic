using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public float speed = 5.0f; 
    private Rigidbody2D rb;

    public static int coin = 0;
    private Vector2 screenBounds;

    [SerializeField] private GameObject plusScore;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); 
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height,0.0f));
    }
  
    void Update(){
        if(transform.position.x < -screenBounds.x*2){
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other){

        Debug.Log("Player");
        if(other.transform.tag == "Player"){
            addScore(10);
            Destroy(this.gameObject);

            GameObject.Find("HeartSpawner").GetComponent<deployHeart>().setHeartWasDeployed(true);
            //play sound
            GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("coin");

            // Score notification
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
            GameObject tempPlusScore = Instantiate(plusScore);
            tempPlusScore.transform.position = currentPosition;
        }
    }

    public void addScore(int score) {
        if (score > 0)
        {
            coin += score;
        }
        else {
            if (coin + score < 0)
            {
                coin = 0;
            }
            else {
                coin += score;
            }
        }
    }
}
