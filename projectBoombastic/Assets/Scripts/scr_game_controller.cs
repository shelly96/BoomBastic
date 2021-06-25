using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_game_controller : MonoBehaviour
{
    [SerializeField] private List<GameObject> heartsPrefabs;
    private float x_pos = -5;
    private float y_pos = 5;

    // Start is called before the first frame update
    void Start()
    {
        //init hearts/ lives
        for(int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = new Vector2(x_pos, y_pos);
            GameObject heart = Instantiate<GameObject>(heartsPrefabs[0]);
            heart.transform.position = spawnPos;

            //set new heart position
            x_pos += 2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
