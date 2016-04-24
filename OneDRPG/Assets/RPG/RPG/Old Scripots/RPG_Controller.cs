using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPG_Controller : MonoBehaviour {

    int turn;
    public int tileSize = 2;
    public int lengthOfLevel;
    public float levelDepth;
    public GameObject[] tiles;
    public GameObject turnText, timer;
    public GameObject tile,hero;
    public int startTime;
    public float turnTimer;
    bool timerON,combat;

    GameObject currentCombatant1, currentCombatant2;

    int heroAction; //1 = attack, 2 = defend

    public int heroDamage, enemyDamage;


	void Awake ()
    {
        
        // music.GetComponent<AudioSource>();


        turn = 0;//sets the turn to 0, as no turns have resolved yet.


        SpawnTiles();

    }

    void Start()
    {
        timerON = true;
    }

    private void SpawnTiles()
    {
        tiles = new GameObject[lengthOfLevel];//Initiates tiles as an Array of gameobjects, equal to the supposed length of the level.
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = GameObject.Instantiate(tile);
            
            

            /*The reason I do it like this is to keep the code adaptable, 
            so I can change the length of the level for each scene without having to hardcode it.
            Also the game is technically one-dimensional, so I don't need more than one one-dimensional array.
             */
            if(i > 2) //I just changed it from "more than 0" to "more than 2"
            {
                AssignRandomTileStatus(tiles[i]);
                    /*
                        Randomly generates blank, enemy, or treasure tiles.
                    */
            }
            if(i >= tiles.Length - 1)
            {
                tiles[tiles.Length - 1].GetComponent<Tile>().finishLine = true;
            }
            tiles[i].GetComponent<Tile>().Place(i,levelDepth);
            
        }
        tiles[0].GetComponent<Tile>().tileType = 3;
        hero = GameObject.Find("Hero");
        hero.GetComponent<RPG_Hero>().StartUp();
        
    }

    void AssignRandomTileStatus(GameObject tile)
    {
        tile.GetComponent<Tile>().tileType = Random.Range(0,2);
        /*
            In  this part I'll randomly generate a a number that I'll assign to the tile's tileType
            value. 

        */
    }

    void Update ()
    {
        Timer();
    }

    private void Timer()
    {
        if (timerON)
        {
            if (turnTimer > 0)
            {
                turnTimer -= Time.deltaTime;
                timer.GetComponent<Text>().text = "Time Left: " + turnTimer.ToString("F0");
            }
            else if (turnTimer <= 0) { timerON = false; ResolveTurn(); }

        }
    }

    public void ResolveTurn()
    {

        TurnDone();
        turnTimer = startTime;
        switch (hero.GetComponent<RPG_Hero>().CheckNextTile())
        {
            case 1: ;
                break;
            case 2:
                hero.GetComponent<RPG_Hero>().GoToNextTile();
                break;
            case 3: ;
                break;
            default: Debug.Log("What the fuck");
                break;
        }
        
    }
    public void InitiateCombat()
    {
        //GOTTA LOOK SOMETHING UP REAL QUICK okay got it
        /*
            In this method I'm gonna implement the combat system.
            The plan is to do it as a type-triangle? Maybe?
            Nimble Beats Strong Beats Defensive Beats Nimble.

            Currently I only have one enemy type. I'm thinking it's Strong.
            So I'm going to make the exchange of Nimble vs Strong
            Meaning you'll have to dodge/counter to beat the combat
            
            So the thought it is that when the Hero lands on a square next to an enemy, 
            the Combat is activated and the player has n seconds to respond and choose either
            Dodge or Counter. 

            Hmm, maybe Dodge should be the Nimble's attack, and Counter should be the Strong's attack?
            While Block could be the Defensive's attack?
            Shit, should I possibly make a turnbased combat system? Like Final Fantasy?
            Why the fuck not it seems like a fun idea!

            Okay so I'm just gonna quickly write up a to-do list of things I have to do to make this work

            First: Detect combat start? DONE (kinda)
            Second: Attack timer & Queue attack action?????????? 
            Third: ???????
            Fourth: Resolve attack actions?
            Fifth: Profit??????

            So I think I'm gonna implement the attack queueing

            <----  I think I'm gonna do combat detection in the Hero Script
        */
        if (!combat)
        {
            Debug.Log("TO BATTERU!!!!! HONOR DESU!!!!");
            combat = true;
            timerON = true;
            heroAction = 0;

        } 
    }
    
    private void TurnDone()
    {
        //This calls for the update of the "Turn" variable while also updating the visible turn counter.
        turn++;
        turnText.GetComponent<Text>().text = "Turn: " + turn;
        timerON = true;
    }

    public void TheyChoseToAttack()
    {
        heroAction = 1;
        /*
            This will be called by a button that is in the scene.
            Once called it will set the Hero Action variable to 1.
            If nothing is called, the variable will be 0.
        */
    }    

    public void TheyChoseToDefend()
    {
        heroAction = 2;
        //ResolveTurn(); We don't want it to prematurely resolve the turn
    }
}
