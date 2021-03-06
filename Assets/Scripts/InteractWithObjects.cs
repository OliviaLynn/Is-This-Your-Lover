﻿using UnityEngine;
using System.Collections;

public class InteractWithObjects : MonoBehaviour
{
    public GameObject CustomerInteractionManager;

    public float pickUpRange = 5.0f;

    public GameObject m_camera;
    [Tooltip("The object all our items are nested under in the Hiearchy Panel")]
    public GameObject parentObjectOfPossibleLovers; // we could set this in start() to be whatever is initially the parent, but, eh

    private GameObject heldObject;
    private Vector3 heldObjectOffset;


    void Update()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = m_camera.transform.position + m_camera.transform.forward * 2.0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject != null)
            {
                DropHeldObject();
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "PossibleLover")
                    {
                        if (pickUpRange >= Vector3.Distance(hit.collider.transform.position, transform.position))
                        {
                            Destroy(hit.collider.gameObject.GetComponent<Rigidbody>());
                            //hit.collider.transform.SetParent(m_camera.transform);

                            heldObject = hit.collider.gameObject;
                            heldObjectOffset = heldObject.transform.position - m_camera.transform.position;
                            Physics.IgnoreCollision(hit.collider, GetComponent<Collider>(), true); 
                        }
                    }
                    if (hit.collider.gameObject.tag == "Door")
                    {
                        CustomerInteractionManager.GetComponent<CustomerInteractions>().ReceiveClick();
                    }
                }
            }

        }
    }
     public void DropHeldObject() // called by portal, in case we're still holding the item
     { 
        if (heldObject != null)
        {
            //heldObject.transform.SetParent(parentObjectOfPossibleLovers.transform);
            //todo stop syncing pos

            heldObject.gameObject.AddComponent<Rigidbody>();
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), GetComponent<Collider>(), false); //todo reset on drop
            heldObject = null;
        }

     }
}