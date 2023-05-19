
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketColliderQuickFireMode : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

    private void OnTriggerEnter(Collider collider)
    {
        //print("tRIGGER " + collider.GetComponent<Rigidbody>().velocity.magnitude);
        if (collider.GetComponent<Rigidbody>().velocity.magnitude < 5f)
        {
            collider.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collider.GetComponent<Rigidbody>().AddForce(collider.GetComponent<Rigidbody>().velocity, ForceMode.Force);
        }
    }
}

