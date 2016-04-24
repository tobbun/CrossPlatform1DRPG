using UnityEngine;
using System.Collections;

public class RPG_Enemy : MonoBehaviour {

    //int tileSize = 2;
   // Vector3 oldPos, newPos;
    Rigidbody rb;
    bool liveliness;
    AudioSource audun;

    // Use this for initialization
    void Start () {
        liveliness = true;
        //oldPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        audun = gameObject.GetComponent<AudioSource>();
    }

    //for now I'm hacking together a script to deal with how to best kill the batbox
    //since my hero script is currently moving by throwing itself forward, I'll make the
    /*
    enemy script deal with dying by first flashing red and then throwing itself off the map
    that seems like a good idea.
    might need to add a rigidbody to it, though. and a line of code that kills it once it's out of sight
    
        */
	void OnCollisionEnter(Collision col)
    {
        if (liveliness)
        {
            if (col.gameObject.name == "Hero")
            {
                audun.Play();
                gameObject.GetComponentInChildren<Renderer>().material.color = new Color(255, 0, 0, 0);

                rb.AddForce(new Vector3(0, -550, -500));
                liveliness = false;
            }
        }
    }
	// Update is called once per frame
	void Update () {
	    if(gameObject.transform.position.y <= -5)
        {
            GameObject.Destroy(gameObject);
        }
	}

   /* public void Moveforward()
    {
        newPos = transform.position + new Vector3(tileSize, 0, 0);

        oldPos = newPos;

        transform.position = oldPos;
    }*/
}
