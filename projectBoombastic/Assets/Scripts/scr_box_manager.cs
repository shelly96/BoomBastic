using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_box_manager : MonoBehaviour
{

    [SerializeField] private List<GameObject> boxPrefabs;
    private int counter;
    private float x_coordinate;
    private float y_coordinate;

    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(0, 4);
        x_coordinate = 9;
        y_coordinate = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //spawn boxes in the begining
        while(counter > 0)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-x_coordinate, x_coordinate), y_coordinate);
            GameObject box = Instantiate<GameObject>(boxPrefabs[0]);
            box.transform.position = spawnPos;

            this.counter --;
        }

    }
}
