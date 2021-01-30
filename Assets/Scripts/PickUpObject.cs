﻿using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour
{
    public float pickUpRange = 5.0f;

    public GameObject m_camera;
    [Tooltip("The object all our items are nested under in the Hiearchy Panel")]
    public GameObject defaultItemParent; // we could set this in start() to be whatever is initially the parent, but, eh

    private GameObject heldObject;


    void Update()
    {
        /*
        RaycastHit hit2;
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hit2))
        {

            if (hit2.collider.gameObject.tag == "PossibleLover")
            {
                //Debug.Log("!!");
                //hit2.collider.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*0.03f, ForceMode.Impulse);
                Debug.Log(hit2.collider.gameObject.GetComponent<Renderer>().material);


            }
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject != null)
            {
                heldObject.transform.SetParent(defaultItemParent.transform);
                heldObject.gameObject.AddComponent<Rigidbody>();
                heldObject = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "PossibleLover")
                    {
                        Debug.Log(Vector3.Distance(hit.collider.transform.position, transform.position));
                        if(pickUpRange >= Vector3.Distance(hit.collider.transform.position, transform.position))
                        {
                            Destroy(hit.collider.gameObject.GetComponent<Rigidbody>());
                            hit.collider.transform.SetParent(m_camera.transform);
                            heldObject = hit.collider.gameObject;
                        }
                    }
                }
            }

        }
    }
}