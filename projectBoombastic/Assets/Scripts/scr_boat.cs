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

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void FixedUpdate()
    {
        counter += intervalSpeed;

        // Rotation
        float newAngle = angle * Mathf.Sin(counter);

        tf.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

        // Translation (Y-Axis)
        //
    }
}
