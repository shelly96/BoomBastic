using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{

    [SerializeField] private List<GameObject> coinPrefabs;
    public static int coin = 0;
    private Vector2 screenBounds;

    private GameObject coinPrefab; 

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    private void OnTriggerEnter2D(Collider2D other){

        Debug.Log("Detected Collision");
        if(other.transform.tag == "Coin"){
            coin += 10;

            // move to the top left
            Vector2 spawnPos = new Vector2 ( (Random.Range((-screenBounds.x+7), (screenBounds.x-7))), (Random.Range((screenBounds.y - 7), (screenBounds.y - 8))) );
            other.transform.position = spawnPos;
        }
    }
}
