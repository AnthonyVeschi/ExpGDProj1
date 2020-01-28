using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string color;

    bool beingHeldUpToDoor = false;
    GameObject door;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == color + "Door")
        {
            beingHeldUpToDoor = true;
            door = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == color + "Door")
        {
            beingHeldUpToDoor = false;
            door = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (door && beingHeldUpToDoor && Input.GetMouseButtonDown(0))
        {
            Destroy(door);
            door = null;
        }
    }
}
