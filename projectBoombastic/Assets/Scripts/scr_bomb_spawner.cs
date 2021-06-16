using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomb_spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombPrefabs;
    [SerializeField] private float spawnTime = 1.0f;

    private float counter = 0.0f;

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

            GameObject bomb = Instantiate(bombPrefabs[Random.Range(0, bombPrefabs.Count)]) as GameObject;
            bomb.transform.position = spawnPos;
        }
    }
}
