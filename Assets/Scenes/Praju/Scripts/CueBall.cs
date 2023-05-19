using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //print("Collision object : " + collision.collider.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("Trigger object : " + other.name);
    }
}
