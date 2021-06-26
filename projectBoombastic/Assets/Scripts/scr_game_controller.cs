using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_game_controller : MonoBehaviour
{
    //lives
    [SerializeField] private List<GameObject> heartsPrefabs;
    private float x_pos = 12;
    private float y_pos = 7;

    //score
    private GameObject scoreObject;
    private int coinVallue;
    private GameObject coinObject = GameObject.Find("Coin");
    //CollectCoin coinScript = coinObject.GetComponent<CollectCoin>();
    //GameObject.Find("Coin").GetComponent<CollectCoin>().coin;

    // Start is called before the first frame update
    void Start()
    {
        //init hearts/ lives
        for(int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = new Vector2(x_pos, y_pos);
            GameObject heart = Instantiate<GameObject>(heartsPrefabs[0]);
            heart.transform.position = spawnPos;

            //set next heart position
            x_pos += 1;
        }

        scoreObject = GameObject.Find("Score");
        
    }

    // Update is called once per frame
    void Update()
    {
        //update score
        //coinVallue = GameObject.Find("Coin").GetComponent<CollectCoin>().coin;
        //scoreObject.GetComponent<Text>().text = 'Score: ' + coinVallue;
    }
}
