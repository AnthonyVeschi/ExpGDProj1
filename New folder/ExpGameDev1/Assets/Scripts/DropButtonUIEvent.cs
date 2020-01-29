using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropButtonUIEvent : MonoBehaviour
{
    Button b;
    public int slot = -1;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        b = gameObject.GetComponent<Button>();
        player = GameObject.Find("Player");

        b.onClick.AddListener(BtnTask);
    }

    void BtnTask()
    {
        player.GetComponent<Inventory>().DropItem(slot);
    }

    public void AssignSlot(int mySlot)
    {
        slot = mySlot;
    }
}
