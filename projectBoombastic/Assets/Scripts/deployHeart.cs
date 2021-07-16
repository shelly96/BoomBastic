using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployHeart : MonoBehaviour
{
    public GameObject heartPrefab;
    private bool heartWasDeployed = true;
    private Vector2 screenBounds; 
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 0.0f));
    }

    void Update(){
        if(CollectCoin.coin % 100 == 0 && CollectCoin.coin!= 0 && this.heartWasDeployed==true){
            spawnHeart();
            this.heartWasDeployed=false;
        }
    }

    private void spawnHeart(){
        GameObject h = Instantiate(heartPrefab) as GameObject;
        h.transform.position = new Vector2(screenBounds.x*2, Random.Range(-screenBounds.y+4, screenBounds.y-6));
    }


    public void setHeartWasDeployed(bool heartWasDeployed){
        this.heartWasDeployed = heartWasDeployed; 
    }
}
