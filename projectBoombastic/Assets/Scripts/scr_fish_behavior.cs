using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_fish_behavior : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float shotAngle = -6f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * shotAngle, speed);

    }

    // Update is called once per frame
    void Update()
    {
        //rotate prefab dependent on fly direction (y 180)

        //damage

        //destroy Fishes outside the view
        if (System.Math.Abs(transform.position.y) > screenBounds.y + 2 || System.Math.Abs(transform.position.x) > screenBounds.x + 2)
        {
            Destroy(this.gameObject);
        }
    }
}
