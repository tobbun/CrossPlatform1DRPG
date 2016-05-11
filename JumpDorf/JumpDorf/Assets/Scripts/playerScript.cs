using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public Animator anim;
    SpriteRenderer spr;
    public int jumpSpeed, steeringSpeed;
    public float currentYSpeed;
    public bool freeJump;
    public bool living = true;
    Rigidbody2D rb;
    public Transform steeringPoint;
    Vector2 mousePosition;

	// adds rigidbody and spriterenderer, 
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2();
	}
	
	void FixedUpdate () {
        currentYSpeed = rb.velocity.y;
        anim.SetFloat("Y Speed", currentYSpeed);
        //SteerWithKeys();
        SteerWithPoint();
        if (freeJump == true){
            YouCanJumpInTheAirNow();
        }
        EndStep();
	}

    void YouCanJumpInTheAirNow()
    {
        if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W)){
            Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("You've just touched: " + col.gameObject.tag);
        if (col.gameObject.tag == "Head")
        {
            Jump();
        }
        else if (col.gameObject.tag == "Sharp")
        {
            living = false;
            spr.color = new Color(255, 0, 0);
            Jump();        
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(0, jumpSpeed);
    }

    void SteerWithKeys()
    {
        rb.velocity = new Vector2(steeringSpeed * Input.GetAxis("Horizontal"), currentYSpeed);
    }

    void SteerWithPoint()
    {
        if(Application.platform == RuntimePlatform.Android) { mousePosition = Input.GetTouch(1).position; }
        if(Application.platform == RuntimePlatform.WebGLPlayer||Application.platform == RuntimePlatform.WindowsEditor) {
            mousePosition = Input.mousePosition;
        }
        
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        steeringPoint.position = mousePosition;
        //Debug.Log("Mouse is at: "+Input.mousePosition+". And Steering Point is at: "+ steeringPoint.position);
        float dis = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
            new Vector2(steeringPoint.position.x, steeringPoint.position.y));
        //Debug.Log(""+ dist);
        rb.velocity = new Vector2(steeringPoint.position.x - gameObject.transform.position.x, currentYSpeed);
        if (rb.velocity.x <= 0)
        {
            spr.flipX = true;
        }
        else { spr.flipX = false; }

    }

    void EndStep()
    {
        if (living == false || gameObject.transform.position.y <= 0) {

            Application.LoadLevel("test");
        
        }
    }
}
