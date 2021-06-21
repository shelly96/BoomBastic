using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private float coin = 0; 

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.tag == "Coin"){
            Destroy(collider.gameObject);
        }
    }
}
