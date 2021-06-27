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
    private GameObject heart;
    private List<GameObject> heartList;
    public static bool damage = false;

    //score
    private GameObject scoreObject;
    private const string scoreTxt = "Score: ";
    private int coinVallue;

    // Start is called before the first frame update
    void Start()
    {
        //init hearts/ lives
        for(int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = new Vector2(x_pos, y_pos);
            heart = Instantiate<GameObject>(heartsPrefabs[0]);
            heart.transform.position = spawnPos;

            //heartList.Add(heart.gameObject);

            //set next heart position
            x_pos += 1;

        }

        scoreObject = GameObject.Find("Score");
        
    }

    // Update is called once per frame
    void Update()
    {
        //update score
        //coinVallue = GameObject.Find("Player").GetComponent<CollectCoin>().coin;
        //scoreObject.GetComponent<Text>().text = scoreTxt + coinVallue.ToString();

        //update lives
        if (damage)
        {
            Debug.Log("Lost one live");

            //Destroy(heartList[heartList.Count - 1]);
            damage = false;

            //check for game over
            /*if (heartList.Count == 0)
            {
                //gameover
                return;
            }*/
        }
    }
}
