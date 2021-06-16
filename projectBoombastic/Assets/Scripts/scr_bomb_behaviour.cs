using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomb_behaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float shotAngle = 5.0f;
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed*shotAngle, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy if bomb hit the waves
        if(-transform.position.y > screenBounds.y + 3)
        {
            Destroy(this.gameObject);
        }
    }
}
