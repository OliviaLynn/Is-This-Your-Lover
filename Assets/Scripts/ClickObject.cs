using UnityEngine;
using System.Collections;

public class ClickObject : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject heldObject;
    public GameObject defaultItemParent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject != null)
            {
                heldObject.transform.SetParent(defaultItemParent.transform);
                //heldObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
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
                        //hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        Destroy(hit.collider.gameObject.GetComponent<Rigidbody>());
                        hit.collider.transform.SetParent(m_camera.transform);
                        heldObject = hit.collider.gameObject;
                    }
                }
            }

        }
    }
}