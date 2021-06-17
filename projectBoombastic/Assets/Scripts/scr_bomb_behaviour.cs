using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomb_behaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float shotAngle = 5.0f;
    [SerializeField] private float bombTimer = 10.0f;
    private Vector2 screenBounds;
    private float startTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed*shotAngle, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(startTime != 0.0f){
            checkBombExplosion((startTime+bombTimer));
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
                startTime = Time.time;
                break;
        }
    }

    private void checkBombExplosion(float explosionTime){

       if(Time.time >= explosionTime) {
            Destroy(this.gameObject);
            Debug.Log("EXPLODE!");
            // TODO add sound
        }
    }
}
