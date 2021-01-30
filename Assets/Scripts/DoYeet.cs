using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoYeet : MonoBehaviour
{
    public GameObject Particles;

    //Detect collisions between the GameObjects with Colliders attached
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PossibleLover")
        {
            Destroy(other.gameObject);
            Particles.GetComponent<ParticleSystem>().Clear();
        }
    }
}