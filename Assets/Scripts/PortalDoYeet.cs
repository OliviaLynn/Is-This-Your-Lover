using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoYeet : MonoBehaviour
{
    public float yeetForce;

    public GameObject Particles;
    public GameObject Door;
    public GameObject Player;

    private List<GameObject> failedAttempts;


    void Start()
    {
        failedAttempts = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PossibleLover" && !failedAttempts.Contains(other.gameObject))
        {
            int returnCode = Door.GetComponent<CustomerInteractions>().IsThisYourLover(other.gameObject);
            Debug.Log(returnCode);
            if (returnCode == 1)
            {
                Destroy(other.gameObject);
                Particles.GetComponent<ParticleSystem>().Clear();
                failedAttempts = new List<GameObject>();
            }
            else if (returnCode == 0)
            {
                failedAttempts.Add(other.gameObject);
                RandomYeet(other.gameObject);
            }
        }
    }

    private void RandomYeet(GameObject falseLover)
    {
        Player.GetComponent<InteractWithObjects>().DropHeldObject();
        falseLover.GetComponent<Rigidbody>().AddForce(yeetForce * new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.75f, 1.0f), Random.Range(-1.0f, 1.0f)), ForceMode.Impulse);
    }
}