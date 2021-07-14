using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_box_manager : MonoBehaviour
{

    [SerializeField] private List<GameObject> boxPrefabs;
    [SerializeField] private int counter;
    private float x_coordinate;
    private float y_coordinate;

    // Start is called before the first frame update
    void Start()
    {
        x_coordinate = 8;
        y_coordinate = -2;

        //spawn boxes in the begining
        while (counter > 0)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-x_coordinate, x_coordinate), y_coordinate);
            GameObject box = Instantiate<GameObject>(boxPrefabs[Random.Range(0, boxPrefabs.Count)]);
            box.transform.position = spawnPos;

            //set next box higher to prevent collision
            y_coordinate = y_coordinate + 0.5f ;

            this.counter--;
        }
    }

}
