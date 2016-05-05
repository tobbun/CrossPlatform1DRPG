﻿using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public Animator anim;
    SpriteRenderer spr;
    public int jumpSpeed, steeringSpeed;
    public float currentYSpeed;
    public bool freeJump;
    public bool living = true;
    Rigidbody2D rb;

	// adds rigidbody and spriterenderer, 
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2();
	}
	
	
	void FixedUpdate () {
        currentYSpeed = rb.velocity.y;
        anim.SetFloat("Y Speed", currentYSpeed);
        
        if (freeJump == true){
            YouCanJumpInTheAirNow();
        }

	}

    void YouCanJumpInTheAirNow()
    {
        if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W)){
            Jump();
        }
    }

    void OnCollisionEnter(Collision colin)
    {
        Debug.Log("You've just touched: " + colin.gameObject.tag);
        if (colin.gameObject.tag == "Head")
        {
            Jump();
        }
        else if (colin.gameObject.tag == "Sharp")
        {
            living = false;
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(0, 10);
    }
}
