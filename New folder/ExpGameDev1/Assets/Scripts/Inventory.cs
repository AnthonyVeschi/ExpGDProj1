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
    public GameObject dropButtonPrefab;
    GameObject dropButton;

    bool spaceForItem;
    int spaceIndex;

    public float pickUpRange = 2f;
    int pickUpLayerMask;

    GameObject broom;
    GameObject gun;
    GameObject wire;
    GameObject key;

    Rigidbody gunRB;

    public float gunThrowForce = 50f;

    public Camera cam;

    public Transform gunPosition;

    bool listenToInput = true;

    // Start is called before the first frame update
    void Start()
    {
        pickUpLayerMask = LayerMask.GetMask("Item");
        gunPosition = transform.Find("GunPos");
    }

    // Update is called once per frame
    void Update()
    {
        if (listenToInput)
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
                        dropButton = Instantiate(dropButtonPrefab, UISlots[spaceIndex].transform);
                        dropButton.GetComponent<DropButtonUIEvent>().AssignSlot(spaceIndex);
                        Destroy(hit.transform.gameObject);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                switch (equippedItemID)
                {
                    case (-1):
                        break;
                    case (0):
                        break;
                    case (1):
                        int i = -1;
                        foreach (GameObject item in Slots)
                        {
                            if (item != null && item.GetComponent<itemID>().ID == 1)
                            {
                                i = System.Array.IndexOf(Slots, item);
                                break;
                            }
                        }
                        Slots[i] = null;
                        foreach (Transform child in UISlots[i].transform)
                        {
                            Destroy(child.gameObject);
                        }

                        gun.transform.parent = null;
                        gunRB = gun.GetComponent<Rigidbody>();
                        gunRB.isKinematic = false;
                        gunRB.AddForce(cam.transform.forward * gunThrowForce, ForceMode.Impulse);
                        equippedItemID = -1;
                        break;
                    case (2):
                        break;
                    case (3):
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.R)) { PutAwayItem(); }
        }
    }

    public void EquipItem(int ID)
    {
        switch (ID)
        {
            case (0):
                if (equippedItemID == 0) { break; }
                else if (equippedItemID != -1) { PutAwayItem(); }

                broom = Instantiate(Items[ID], gunPosition, false);
                broom.GetComponent<Broom>().enabled = true;
                equippedItemID = 0;
                break;
            case (1):
                if (equippedItemID == 1) { break; }
                else if (equippedItemID != -1) { PutAwayItem(); }

                gun = Instantiate(Items[ID], gunPosition, false);
                equippedItemID = 1;
                break;
            case (2):
                if (equippedItemID == 2) { break; }
                else if (equippedItemID != -1) { PutAwayItem(); }

                wire = Instantiate(Items[ID], gunPosition, false);
                wire.GetComponent<Broom>().enabled = true;
                equippedItemID = 2;
                break;
            case (3):
                if (equippedItemID == 3) { break; }
                else if (equippedItemID != -1) { PutAwayItem(); }

                key = Instantiate(Items[ID], gunPosition, false);
                equippedItemID = 3;
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
                Destroy(broom);
                break;
            case (1):
                Destroy(gun);
                break;
            case (2):
                Destroy(wire);
                break;
            case (3):
                Destroy(key);
                break;
        }
        equippedItemID = -1;
    }

    public void DropItem(int slot)
    {
        Instantiate(Slots[slot], gunPosition.position, Quaternion.identity);
        Slots[slot] = null;
        foreach (Transform child in UISlots[slot].transform)
        {
            Destroy(child.gameObject);
        }
        
        PutAwayItem();
    }

    public void pauseGame()
    {
        listenToInput = false;
    }
    public void unPauseGame()
    {
        listenToInput = true;
    }
}
