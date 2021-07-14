using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployCoin : MonoBehaviour
{
    public GameObject coinPrefab; 
    public float respawnTime = 5.0f; 
    private Vector2 screenBounds; 
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
        StartCoroutine(coinWave());
    }

    private void spawnCoin(){
        GameObject c = Instantiate(coinPrefab) as GameObject;
        c.transform.position = new Vector2(screenBounds.x*2, Random.Range(-screenBounds.y+4, screenBounds.y-6));
    }

    IEnumerator coinWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnCoin(); 
        }
    }
}
