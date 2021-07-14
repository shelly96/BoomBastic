using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_box_behavior : MonoBehaviour
{
    private Vector2 screenBounds;

    // Score notification
    [SerializeField] private GameObject minusScore;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
  
        //destroy box if it miss the water
        if (-transform.position.y > screenBounds.y) {
            //play sound
            if (!GameObject.Find("Boat").GetComponent<scr_boat>().isSinking)
            {
                GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("water");

                // Score notification
                Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y + 2f);
                GameObject tempMinusScore = Instantiate(minusScore);
                tempMinusScore.transform.position = currentPosition;

                // decrease Score
                GameObject.Find("Player").GetComponent<CollectCoin>().addScore(-10);
            }
            Destroy(this.gameObject);
        }
    }

}
