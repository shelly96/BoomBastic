using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_box_behavior : MonoBehaviour
{
    private Vector2 screenBounds; 

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {

        //destroy box if it miss the water
        if(-transform.position.y > screenBounds.y + 5) {
            Destroy(this.gameObject);
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Box hit: " + collision.collider.name);

        switch (collision.collider.name) {
            case "Wave_2":
                Destroy(this.gameObject);
                // TODO add sound 
                break;
        }
    }
}
