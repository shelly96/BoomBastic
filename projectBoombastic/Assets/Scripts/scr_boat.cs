using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_boat : MonoBehaviour
{
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
        counter += intervalSpeed;

        // Rotation
        float newAngle = angle * Mathf.Sin(counter);

        tf.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

        // Translation (Y-Axis)

        float newHeight = height * Mathf.Sin(counter);

        tf.position = new Vector3(originalPos.x, originalPos.y + newHeight, 0);

        if (counter >= Mathf.PI*2) {
            counter = 0f;
        }
        
    }
}
