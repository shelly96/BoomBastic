using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_boat : MonoBehaviour
{

    private int shipHits = 0;
    private float sinkingDifference = 0;
    [SerializeField] private float sinkingSpeed = 0.01f;

    [SerializeField] private int maxShipHits = 5;
    private bool isSinking = false;

    [SerializeField] private float angle;
    [SerializeField] private float height;
    [SerializeField] private float intervalSpeed;

    private Transform tf;

    private float counter;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        counter = 0;
        originalPos = new Vector2(tf.position.x, tf.position.y);
    }

    private void FixedUpdate()
    {


        if (!isSinking)
        {

            if (sinkingDifference <= shipHits * 0.3f)
            {
                sinkingDifference += sinkingSpeed;
            }

        }
        else {
            sinkingDifference += sinkingSpeed*4;
        }


        counter += intervalSpeed;

        // Rotation
        float newAngle = angle * Mathf.Sin(counter);

        tf.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

        // Translation (Y-Axis)

        float newHeight = height * Mathf.Sin(counter);

        tf.position = new Vector3(originalPos.x, originalPos.y - sinkingDifference + newHeight, 0);

        if (counter >= Mathf.PI * 2)
        {
            counter = 0f;
        }
    }

    public void takeDamage() {
        if (shipHits < maxShipHits)
        {
            shipHits++;

            Debug.Log(shipHits);
        }
        else {
            isSinking = true;
        }
            
    }

}
