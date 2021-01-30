using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverFeedback : MonoBehaviour
{
    public GameObject player;
    private bool IsMouseOn = false;

    void Start()
    {
        player = GameObject.Find("Player"); //usually I prefer not to use this, but,,,
                                            //it'll be really tediuous adding it manually to all our items
    }

    void OnMouseOver()
    {
        if (!IsMouseOn)
        {
            if (player.GetComponent<InteractWithObjects>().pickUpRange >= Vector3.Distance(player.transform.position, transform.position)) {
                IsMouseOn = true;
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
            }
        }
    }

    void OnMouseExit()
    {
        IsMouseOn = false;
    }
}
