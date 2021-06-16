using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_wave : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    [SerializeField] double endXPosition;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();

        //reset 
        if (transform.position.x < endXPosition) {
           reset();
        }
    }

    void move() 
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    private void reset()
    {
        transform.position = startingPosition;
    }
}
