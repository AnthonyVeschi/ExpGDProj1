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

    GameObject gun;
    GameObject wire;

    Rigidbody gunRB;

    public float gunThrowForce = 50f;

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

        if (Input.GetMouseButtonDown(0))
        {
            switch (equippedItemID)
            {
                case (-1):
                    break;
                case (0):
                    int i = -1;
                    foreach (GameObject item in Slots)
                    {
                        if (item != null && item.GetComponent<itemID>().ID == 0)
                        {
                            i = System.Array.IndexOf(Slots, item);
                            break;
                        }
                    }
                    Slots[i] = null;
                    GameObject destroyableButton = UISlots[i].transform.Find("gunButton(Clone)").gameObject;
                    Destroy(destroyableButton);

                    gun.transform.parent = null;
                    gunRB = gun.GetComponent<Rigidbody>();
                    gunRB.isKinematic = false;
                    gunRB.AddForce(cam.transform.forward * gunThrowForce, ForceMode.Impulse);
                    equippedItemID = -1;
                    break;
                case (1):
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) { PutAwayItem(); }
    }

    public void EquipItem(int ID)
    {
        switch (ID)
        {
            case (0):
                if (equippedItemID == 0) { break; }
                else if (equippedItemID != -1) { PutAwayItem(); }

                gun = Instantiate(Items[ID], gunPosition, false);
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
                Destroy(gun);
                break;
            case (1):
                break;
        }
        equippedItemID = -1;
    }
}
