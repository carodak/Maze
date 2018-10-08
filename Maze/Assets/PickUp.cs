using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Here we're collecting the pick up objects
public class PickUp : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //use the rigidbody component to get our object
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Projectiles")
        {
            //Debug.Log("Hitting3");
            other.gameObject.SetActive(false);
            FindObjectOfType<MazeGrid>().incProjectiles();
        }


    }
}



