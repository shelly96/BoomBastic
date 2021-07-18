using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_fish_spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishPrefabs;
    [SerializeField] private float spawnTime = 10.0f;

    private float counter = 0.0f;

    private float startTime = 0.0f;

    [SerializeField] private float moreFishInterval = 10.0f;

    // get Scrennbounds
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        // spawn a fish every x seconds
        counter += Time.deltaTime;

        if (counter >= spawnTime)
        {
            counter = 0.0f;

            // move to the bottom
            Vector2 spawnPos = new Vector2((Random.Range((screenBounds.x - 3), (screenBounds.x -1))), (-screenBounds.y + 0.5f));

            // initialized fish 
            GameObject fishObject = fishPrefabs[Random.Range(0, fishPrefabs.Count)];
            GameObject fish = Instantiate(fishObject) as GameObject;
            fish.transform.position = spawnPos;

        }

        //increase spawn time every 10 sec
        if (Time.time > startTime + moreFishInterval && spawnTime > 0.5f)
        {
            spawnTime = spawnTime - 0.1f;
            startTime = Time.time;
        }

    }

}
