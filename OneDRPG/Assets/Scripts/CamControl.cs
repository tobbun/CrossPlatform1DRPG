using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
    Vector3 camPlace = new Vector3(2f,9.5f,-13f);
    public Transform hero;
	void Update ()
    {
        
        //TransforPos();
        forceFollow(hero.position);
        
    }

    void forceFollow(Vector3 hero)
    {
        
        gameObject.GetComponent<Rigidbody>().AddForce((hero - gameObject.transform.position) + camPlace);
        gameObject.transform.LookAt(hero);
    }

    private void TransforPos()
    {
        gameObject.transform.position = GameObject.Find("Hero").transform.position + camPlace;
        //the camera is obsessed with the player, it wants to feel them up close
    }
}
