using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public bool inventoryMenuIsOpen = false;
    public GameObject inventoryMenuUI;

    public float inventorySlowTime = 0.2f;

    GameObject player;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Player").transform.Find("Main Camera").gameObject;
    }

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

        player.GetComponent<PlayerMovement>().unPauseGame();
        player.GetComponent<Inventory>().unPauseGame();
        cam.GetComponent<CameraController>().unPauseGame();
    }

    void openInventoryMenu()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = inventorySlowTime;
        Cursor.lockState = CursorLockMode.None;
        inventoryMenuIsOpen = true;

        player.GetComponent<PlayerMovement>().pauseGame();
        player.GetComponent<Inventory>().pauseGame();
        cam.GetComponent<CameraController>().pauseGame();
    }
}
