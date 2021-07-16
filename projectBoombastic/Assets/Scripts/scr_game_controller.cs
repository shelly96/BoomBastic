using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_game_controller : MonoBehaviour
{
    enum State { 
        titleScreen,
        gameScreen,
        endScreen
    }

    public GameObject player;
    private State currentState = State.titleScreen;

    // title
    [SerializeField] private List<GameObject> gameplayElements;
    [SerializeField] private List<GameObject> titlescreenElements;
    [SerializeField] private List<GameObject> endscreenElements;
    [SerializeField] private List<GameObject> howToPlayElements;


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
    private GameObject scoreEndscreenObject;
    private const string scoreTxt = "Score: ";
    private int coinValue;

    private void Awake()
    {
        //Deactivate all gameplay elements at runtime to display the title screen
        deactivateGameplayElements();
        deactivateGameOverScreenElements();
        deactivateHowToPLayScreenElements();
    }

    // Start is called before the first frame update
    void Start()
    {
        //play sound
        GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("ambient");
        GameObject.Find("AudioController").GetComponent<scr_audioController>().playSound("ambient_birds");

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

            //set next heart position
            x_pos -= 1;

        }

        scoreObject = GameObject.Find("Score");
        scoreEndscreenObject = GameObject.Find("ScoreEndscreen");
        //manually deactivate score
        scoreObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState) {
            case State.titleScreen:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    play();
                }
                
                break;

            case State.gameScreen:
                if (heartList[0].activeSelf)
                {
                    //init healthPoints
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
                        deactivateGameplayElements();
                        activateGameOverScreenElements();

                        currentState = State.endScreen;
                    }
                }


               

                //update score
                if (scoreObject.activeSelf)
                {
                    coinValue = CollectCoin.coin;
                    scoreObject.GetComponent<Text>().text = scoreTxt + coinValue.ToString();           
                }
                break;

            case State.endScreen:
      
                coinValue = CollectCoin.coin;
                scoreEndscreenObject.GetComponent<Text>().text = coinValue.ToString();
                
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    replay();
                }
                    
                break;

        }


        // Reset Position (DEBUG)
        if (Input.GetKey(KeyCode.R))
        {
            replay();
        }
    }

    public void play() {
        deactivateTitleScreenElements();
        activateGameplayElements();

        currentState = State.gameScreen;
    }

    public void replay() {
        //reload game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CollectCoin.coin = 0;
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

    public void activateGameOverScreenElements() {
        foreach (GameObject deactivatedElement in endscreenElements)
        {
            deactivatedElement.SetActive(true);

            if (deactivatedElement.name == "Boat") {
                deactivatedElement.GetComponent<scr_boat>().isSinking = true;
            }
        }

        //activate hearts and score
        for (int i = 0; i < maxHealthPoints; i++)
        {
            heartList[i].SetActive(false);
        }

        scoreObject.SetActive(false);
    }

    public void deactivateGameOverScreenElements() {
        foreach (GameObject deactivatedElement in endscreenElements)
        {
            deactivatedElement.SetActive(false);
        }
    }

    void activateHowToPlayScreenElements()
    {
        foreach (GameObject activatedElement in howToPlayElements)
        {
            activatedElement.SetActive(true);
        }
    }

    public void deactivateHowToPLayScreenElements() {
        foreach (GameObject deactivatedElement in howToPlayElements)
        {
            deactivatedElement.SetActive(false);
        }
    }

    public void showHowToPlayWindow() {
        activateHowToPlayScreenElements();

        //disable hover function of HowToPlayButton
        foreach (GameObject element in titlescreenElements)
        {
            if (element.name == "HowToPlayButton") {
                element.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void hideHowToPlayWindow() {
        deactivateHowToPLayScreenElements();

        //disable hover function of HowToPlayButton
        foreach (GameObject element in titlescreenElements)
        {
            if (element.name == "HowToPlayButton") {
                element.GetComponent<Button>().interactable = true;
            }
        }
    }

}
