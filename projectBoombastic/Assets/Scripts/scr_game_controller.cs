using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_game_controller : MonoBehaviour
{

    // title
    [SerializeField] private bool skipTitleScreen;
    [SerializeField] private List<GameObject> gameplayElements;
    [SerializeField] private List<GameObject> titlescreenElements;

    //lives
    [SerializeField] private List<GameObject> heartsPrefabs;
    private float x_pos = 12;
    private float y_pos = 7;
    private GameObject heart;
    private List<GameObject> heartList = new List<GameObject>();
    public static bool damage = false;

    //score
    private GameObject scoreObject;
    private const string scoreTxt = "Score: ";
    private int coinVallue;

    private void Awake()
    {
        //Deactivate all gameplay elements at runtime to display the title screen
        deactivateGameplayElements();

        if (skipTitleScreen) {
            play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //init hearts/ lives
        for(int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = new Vector2(x_pos, y_pos);
            heart = Instantiate<GameObject>(heartsPrefabs[0]);
            heart.transform.position = spawnPos;

            heartList.Add(heart.gameObject);
            Debug.Log("added");

            //set next heart position
            x_pos += 1;

        }

        scoreObject = GameObject.Find("Score");
        
    }

    // Update is called once per frame
    void Update()
    {

        //update score
        coinVallue = CollectCoin.coin;
        //scoreObject.GetComponent<Text>().text = scoreTxt + coinVallue.ToString();

        //update lives
        if (false)//damage)
        {
            Debug.Log("Lost one live");
            Debug.Log(heartList.Count);
            Destroy(heartList[heartList.Count - 1]);
            damage = false;

            //check for game over
            /*if (heartList.Count == 0)
            {
                //gameover
                return;
            }*/
        }
    }

    public void play() {
        deactivateTitleScreenElements();
        activateGameplayElements();
    }


    void activateTitleScreenElements()
    {
        foreach (GameObject activatedElement in titlescreenElements)
        {
            activatedElement.SetActive(true);
        }
    }
    public void deactivateTitleScreenElements() {
        foreach (GameObject deactivatedElement in titlescreenElements)
        {
            deactivatedElement.SetActive(false);
        }
    }

    public void activateGameplayElements()
    {
        foreach (GameObject activatedElement in gameplayElements)
        {
            activatedElement.SetActive(true);
        }
    }

    void deactivateGameplayElements()
    {
        foreach (GameObject deactivatedElement in gameplayElements)
        {
            deactivatedElement.SetActive(false);
        }

    }
}
