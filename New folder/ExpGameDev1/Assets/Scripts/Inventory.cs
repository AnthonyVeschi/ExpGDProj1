using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] Items;
    public GameObject[] Slots;
    public GameObject[] UISlots;

    public int equippedItemID = -1;

    int pickUpID;
    GameObject pickUpButton;

    bool spaceForItem;
    int spaceIndex;

    public float pickUpRange = 2f;
    int pickUpLayerMask;

    public Camera cam;

    public Transform gunPosition;

    // Start is called before the first frame update
    void Start()
    {
        pickUpLayerMask = LayerMask.GetMask("Item");
        gunPosition = transform.Find("GunPos");
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
                    pickUpButton = hit.transform.GetComponent<itemImage>().button;
                    Instantiate(pickUpButton, UISlots[spaceIndex].transform);
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        switch (equippedItemID)
        {
            case (-1):
                break;
            case (0):
                break;
            case (1):
                break;
        }
    }

    public void EquipItem(int ID)
    {
        switch (ID)
        {
            case (0):
                //Vector3 currentPos = gunPosition.localPosition;
                Instantiate(Items[ID], gunPosition, false);
                //gunPosition.localPosition = currentPos;
                //okay this is hecka fucky
                equippedItemID = 0;
                break;
            case (1):
                equippedItemID = 1;
                break;
        }
    }

    public void PutAwayItem()
    {
        switch (equippedItemID)
        {
            case (-1):
                break;
            case (0):
                break;
            case (1):
                break;
        }
        equippedItemID = -1;
    }
}
