using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_game_controller : MonoBehaviour
{

    public GameObject player;

    // title
    [SerializeField] private bool skipTitleScreen;
    [SerializeField] private List<GameObject> gameplayElements;
    [SerializeField] private List<GameObject> titlescreenElements;

    //lives
    private int maxHealthPoints;
    [SerializeField] private GameObject heartsPrefabs;
    private float x_pos = 12.85f;
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
        maxHealthPoints = player.GetComponent<scr_player>().healthPoints;

        //init hearts/ lives
        for (int i = 0; i < maxHealthPoints; i++)
        {
            Vector2 spawnPos = new Vector2(x_pos, y_pos);
            heart = Instantiate<GameObject>(heartsPrefabs);
            heart.transform.position = spawnPos;

            heartList.Add(heart.gameObject);

            //manually deactivate hearts
            heartList[i].SetActive(false);
            Debug.Log("added");

            //set next heart position
            x_pos -= 1;

        }

        scoreObject = GameObject.Find("Score");
        //manually deactivate score
        scoreObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (heartList[0].activeSelf)
        {
            //healthPoints
            int currentHealthPoints = player.GetComponent<scr_player>().healthPoints;

            //init hearts/ lives
            for (int i = 0; i < maxHealthPoints; i++)
            {
                if (i < currentHealthPoints)
                {
                    heartList[i].GetComponent<scr_heart>().on = true;
                }
                else
                {
                    heartList[i].GetComponent<scr_heart>().on = false;
                }

            }
        

            if (currentHealthPoints <= 0)
            {
                //GAME OVER
            }
        }

        //update score
        if (scoreObject.activeSelf)
        {
            coinVallue = CollectCoin.coin;
            scoreObject.GetComponent<Text>().text = scoreTxt + coinVallue.ToString();
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

        //activate hearts and score
        for (int i = 0; i < maxHealthPoints; i++)
        {
            heartList[i].SetActive(true);
        }

        scoreObject.SetActive(true);
    }

    void deactivateGameplayElements()
    {
        foreach (GameObject deactivatedElement in gameplayElements)
        {
            deactivatedElement.SetActive(false);
        }

    }
}
