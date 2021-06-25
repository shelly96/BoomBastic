using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomb_spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombPrefabs;
    [SerializeField] private float spawnTime = 5.0f;
    [SerializeField] private int missingRedBombs = 3;

    private float counter = 0.0f;

    private int redBombCounter = 0;


    // get Scrennbounds
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        // spawn a bomb every x seconds
        counter += Time.deltaTime;

        if(counter >= spawnTime) 
        {
            counter = 0.0f;

            // move to the top left
            Vector2 spawnPos = new Vector2 ( (Random.Range((-screenBounds.x - 10), (screenBounds.x - 15))), (screenBounds.y + 3) );

            // initialized bomb 
            GameObject bombObject = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

            // spawn less red bombs (bomb_2)
            if(bombObject.name == "bomb_2") {
                redBombCounter ++;
                Debug.Log("Missed red Bombs: " + redBombCounter);
                if(redBombCounter > missingRedBombs) {
                    GameObject bomb = Instantiate(bombObject) as GameObject;
                    bomb.transform.position = spawnPos;
                    redBombCounter = 0;
                }
            } else {
                GameObject bomb = Instantiate(bombObject) as GameObject;
                bomb.transform.position = spawnPos;
            }
        }
    }
}
