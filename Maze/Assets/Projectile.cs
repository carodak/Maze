/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float delay = 3f;

    public GameObject explositionEffect;

    bool hasExploded = false;
    float countdown;

	// Use this for initialization
	void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded){
            Explode();
            hasExploded = true;
        }

	}

    void Explode(){
        //Show effect
        Instantiate(explositionEffect, transform.position, transform.rotation);

        //Get nerby objects = destroy them

        Destroy(gameObject);
    }
}
*/