using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    int tileSize = 2;

    public GameObject occupiedBy;
    public bool occupied, finishLine;
    public GameObject[] tileTypes = new GameObject[4];
    //public GameObject Hero, Enemy, Treasure;


    public int tileType;
    public int tileOrder;

    void Awake() {
        
        /*
        //tileTypes[0] = new GameObject();
        tileTypes[1] = Enemy;
        tileTypes[2] = Treasure;
        tileTypes[3] = Hero;
        */
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Hero") {
            if (finishLine)
            {

            }
            col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY;
            Debug.Log("The Hero Has Landed on Tile#" + tileOrder);
            col.gameObject.GetComponent<RPG_Hero>().currentTile = gameObject;
            gameObject.GetComponent<Renderer>().material.color = new Color(0,255,0,0);
        }
    }

    public void Place(int i, float depth)
    {
        tileOrder = i;
        gameObject.name = "Tile#" + tileOrder;
        transform.position = new Vector3(tileOrder * tileSize, 0, depth);
        if (tileType > 0)
        {
            
            occupied = true;
            Instantiate(tileTypes[tileType], gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            Debug.Log("Has spawned type #" + tileType);
            occupiedBy = tileTypes[tileType];
            /*
                Hmmm, could I possibly instantiate the thing AND assign what
                the tile is occupied by in the same go?
                Or would it be a case of the whatevers? 
                Anyways, I'm gonna see if this works.
                Whelp, good enough.
            */

            
        }
       
    }

}