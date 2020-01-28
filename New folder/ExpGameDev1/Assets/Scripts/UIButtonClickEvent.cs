using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonClickEvent : MonoBehaviour
{

    Button b;
    public int ID;
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
        player.GetComponent<Inventory>().EquipItem(ID);
    }
}
