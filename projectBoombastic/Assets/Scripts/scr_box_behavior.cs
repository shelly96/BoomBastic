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
        if (-transform.position.y > screenBounds.y + 5) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        
        switch (trigger.name) {
            case "Wave_2":
                // TODO add sound 
                break;
        }
    }

}
