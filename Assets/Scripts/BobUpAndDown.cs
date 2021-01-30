using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour
{
    //adjust this to change speed
    float speed = 2f;
    //adjust this to change how high it goes
    float height = 0.0004f;

    float phaseOffset = 0;

    void Start()
    {
        // Nudge things a bit so the lights are a little out of sync
        speed *= Random.Range(0.8f, 1.2f);
        phaseOffset = Random.Range(0, speed);
    }

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed + phaseOffset);
        //set the object's Y to the new calculated Y
        transform.position = pos + Vector3.up * height * newY;
    }
}
