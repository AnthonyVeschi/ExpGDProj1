using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] Items;
    public GameObject[] Slots;

    int pickUpID;

    bool spaceForItem;
    int spaceIndex;

    public float pickUpRange = 2f;
    int pickUpLayerMask;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        pickUpLayerMask = LayerMask.GetMask("Item");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickUpID = hit.transform.GetComponent<itemID>().ID;

                spaceForItem = false;
                foreach (GameObject item in Slots)
                {
                    if (item == null)
                    {
                        spaceForItem = true;
                        spaceIndex = System.Array.IndexOf(Slots, item);
                        break;
                    }
                }
                if (spaceForItem)
                {
                    Slots[spaceIndex] = Items[pickUpID];
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        spaceForItem = false;
    }
}
