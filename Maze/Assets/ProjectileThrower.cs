using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{

    public float throwForce = 40f;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ThrowProjectile();
        }

    }

    void ThrowProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
