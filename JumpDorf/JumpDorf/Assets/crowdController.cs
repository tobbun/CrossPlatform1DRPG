using UnityEngine;
using System.Collections;

public class crowdController : MonoBehaviour {

    public GameObject[] crowd;
    public Transform crowdSpawn;
    public float timer, timerDefault;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnRandomCitizen();
            timer = timerDefault;
        }
	}

    void SpawnRandomCitizen()
    {
        GameObject shithead = (GameObject)Instantiate(crowd[Random.Range(0, 1)], crowdSpawn.position, new Quaternion(0,0,0,0));

        Rigidbody2D rb = shithead.AddComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.velocity = new Vector2(-5,0);
        
        shithead.AddComponent<Dwarf>();

    }


    public class Dwarf : MonoBehaviour
    {
        void FixedUpdate()
        {
            if (gameObject.transform.position.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
