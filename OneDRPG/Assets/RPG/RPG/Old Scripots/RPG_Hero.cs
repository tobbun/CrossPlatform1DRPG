using UnityEngine;
using System.Collections;

public class RPG_Hero : MonoBehaviour {

    int tileSize = 2;
    public GameObject currentTile, nextTile, whatTheNextTileIsOccupiedBy;
    public int currentTileNumber;
    public GameObject[] allTheTiles;
    Vector3 oldPos, newPos;
    public bool isNextTileOccupied;
    public GameObject controller;
    Rigidbody rb;
    public float jumpforce;
    public bool portOrJoomp;

    int health, def, atk;
    
    /*
        So what I'm going to do in this script is to set up the functionality of the HERO
        */

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        

    }

    public void StartUp()
    {
        oldPos = transform.position;
        allTheTiles = controller.GetComponent<RPG_Controller>().tiles; //fetches the tile array from the controller object

        currentTileNumber = 0;
        currentTile = allTheTiles[currentTileNumber]; //sets the current tile to the first tile in the tile array

        UpdateNextTile();
    }

    // Update is called once per frame
    void Update () {
        //checks the status of the next tile
	}

   public void Moveforward()
    {
        if (!portOrJoomp) {
            JumpForward(); //this was mostly added for lulz. Nobody thought it was funny.
        } else { TelePort1Forward(); }//This is the basest one. First solution and incomplete for lack of interaction with the actual tiles.
        
        
    }

    public void GoToNextTile()
    {   
        currentTile = nextTile;
        gameObject.transform.position = currentTile.transform.position;
        UpdateNextTile();
        CheckNextTile();

    }

    private void UpdateNextTile()
    {
        nextTile = allTheTiles[currentTileNumber + 1];
        isNextTileOccupied = nextTile.GetComponent<Tile>().occupied;
        whatTheNextTileIsOccupiedBy = nextTile.GetComponent<Tile>().occupiedBy;
        currentTileNumber++;
    }

    private void JumpForward()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        rb.velocity = new Vector3(5, 5, 0);
    }

    

    private void TelePort1Forward()
    {
        //This is the very basest of functionality in moving the rpg character.
        newPos = transform.position + new Vector3(tileSize, 0, 0);
        //Here I tell the game that the character's new position should be one forward.

        oldPos = newPos; //Here I do a handy exchange of the old position for the new

        transform.position = oldPos; 
        //And here I tell the game that the character's position should change to this new position.
    }


    public int CheckNextTile()
    {   /*
        //oh, that's what I did wrong. Ah ok. Kids, know the difference between your || and your &&
        {
            controller.GetComponent<RPG_Controller>().InitiateCombat();
        }*/
       
        return nextTile.GetComponent<Tile>().tileType;
        /*
            Okay so this doesn't work as well. Instead of checking the next tile
            and initiating combat in this method, I should probably get the method
            return a number equal to the predefined tile type? So that it tells the
            controller what kind of entity might be occupying the next tile, 
            and then let the controller initiate combat.
        */
    }

    public void Shit()
    {
        GameObject shit = GameObject.CreatePrimitive(PrimitiveType.Cube);
        shit.transform.position = gameObject.transform.position - new Vector3(1,-0.5f,0);
        shit.gameObject.GetComponent<Renderer>().material.color = new Color(255,255,100,0);

    }

    public void Defend()
    {

    }

    public void Attack()
    {

    }
}
