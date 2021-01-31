using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverFeedback : MonoBehaviour
{
    public GameObject player;
    private bool isMouseOn = false;
    private bool onCooldown = false;

    void Start()
    {
        player = GameObject.Find("Player"); //usually I prefer not to use this, but,,,
                                            //it'll be really tediuous adding it manually to all our items
    }

    void OnMouseOver()
    {
        if (!onCooldown && !isMouseOn)
        {
            if (player.GetComponent<InteractWithObjects>().pickUpRange >= Vector3.Distance(player.transform.position, transform.position)) {
                onCooldown = true;
                isMouseOn = true;
                Invoke("EndCooldown", 1);
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 1.75f, ForceMode.Impulse);
            }
        }
    }

    void EndCooldown()
    {
        onCooldown = false;
    }

    void OnMouseExit()
    {
        isMouseOn = false;
    }
}
