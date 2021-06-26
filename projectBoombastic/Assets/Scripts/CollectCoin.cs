using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{

    [SerializeField] private List<GameObject> coinPrefabs;
    public float coin = 0;
    private Vector2 screenBounds;

    private GameObject coinPrefab; 

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    private void OnTriggerEnter2D(Collider2D other){

        Debug.Log("Detected Collision");
        if(other.transform.tag == "Coin"){
            this.coin += 1;

            // move to the top left
            Vector2 spawnPos = new Vector2 ( (Random.Range((-screenBounds.x), (screenBounds.x))), (screenBounds.y - 10) );
            other.transform.position = spawnPos;
        }
    }
}
