using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_fish_behavior : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private float speed = -0.5f;
    private float angle = 0f;
    private float hight = 0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        //set fish direction and rotate prefab dependent on fly direction
        if (transform.position.x <= 0)
        {
            speed = -speed;
            this.transform.Rotate(0, 180, 0);
        }

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //flying angle
        switch (this.name)
        {
            case "hostileFish_3":
                angle = 0.3f;
                hight = 3f;
                break;
            case "hostileFish_2":
                angle = 1f;
                hight = 4f;
                break;
            case "hostileFish_1":
                angle = 2f;
                hight = 5f;
                break;
        }

        //update position
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = (float)(-angle * System.Math.Sqrt(newX) + hight);

        transform.position = new Vector3(newX, newY, 0);

        //destroy Fishes outside the view
        if (System.Math.Abs(transform.position.y) > screenBounds.y + 2 || System.Math.Abs(transform.position.x) > screenBounds.x + 2)
        {
            Destroy(this.gameObject);
        }
    }

    //take damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            player.GetComponent<scr_player>().takeDamage();
        }
    }
}
