using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public Animator anim;
    SpriteRenderer spr;
    public int jumpSpeed, steeringSpeed;
    public float currentYSpeed;
    bool freeJump;
    public bool living = true;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentYSpeed = rb.velocity.y;
        anim.SetFloat("Y Speed", currentYSpeed);
        
        if (freeJump == true){
            YouCanJumpInTheAirNow();
        }

	}

    void YouCanJumpInTheAirNow()
    {
        if(Input.GetKey(KeyCode.UpArrow)){

        }
    }
}
