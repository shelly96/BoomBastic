using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_box_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //destroy box if it has fallen of the ship
        if (transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
