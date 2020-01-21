using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public bool inventoryMenuIsOpen = false;
    public GameObject inventoryMenuUI;

    public float inventorySlowTime = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventoryMenuIsOpen)
            {
                closeInventoryMenu();
            }
            else
            {
                openInventoryMenu();
            }
        }
    }

    void closeInventoryMenu()
    {
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        inventoryMenuIsOpen = false;
    }

    void openInventoryMenu()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = inventorySlowTime;
        Cursor.lockState = CursorLockMode.None;
        inventoryMenuIsOpen = true;
    }
}
