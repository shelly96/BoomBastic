using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_fish_behavior : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private float movSpeed = 0.02f;
    [SerializeField] private float intervalSpeed = 0.02f;
    [SerializeField] private float angle = 0.02f;

    private Transform tf;

    [SerializeField] private float height = 0.15f;


    private Vector2 originalPos;
    private Vector3 originalRotation;
    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        tf = GetComponent<Transform>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        originalPos = new Vector2(tf.position.x, tf.position.y);
        originalRotation = new Vector3(tf.rotation.eulerAngles.x, tf.rotation.eulerAngles.y, tf.rotation.eulerAngles.z);

        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    // Update is called once per frame
    void Update()
    {
        counter += intervalSpeed*Time.deltaTime;

        // y position 
        float newY =  height* Mathf.Sin(counter) ;

        // x position
        float newX = counter * movSpeed ;

        // rotation
        float newAngle =  angle * Mathf.Cos(counter) ;

        
        tf.SetPositionAndRotation(new Vector3(originalPos.x - newX, originalPos.y + newY, 0), Quaternion.Euler(new Vector3(0, 0, newAngle)));

        if (tf.position.x < -screenBounds.x -2) {
           Destroy(this.gameObject);
        }
  
    }

    //take damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player") {
            GameObject.Find("Player").GetComponent<scr_player>().takeDamage();
        }
    }
}
