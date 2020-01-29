using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public bool PhoneOut = false;
    public GameObject Phone;
    public GameObject TinyPhone;

    public float PhoneSlowTime = 0.2f;

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public int pageNum = 0;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PhoneOut)
            {
                ClosePhone();
            }
            else
            {
                openPhone();
            }
        }
    }

    void ClosePhone()
    {
        Phone.SetActive(false);
        TinyPhone.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        PhoneOut = false;

        player.GetComponent<PlayerMovement>().unPauseGame();
        player.GetComponent<Inventory>().unPauseGame();
        cam.GetComponent<CameraController>().unPauseGame();
    }

    void openPhone()
    {
        Phone.SetActive(true);
        TinyPhone.SetActive(false);
        Time.timeScale = PhoneSlowTime;
        Cursor.lockState = CursorLockMode.None;
        PhoneOut = true;

        player.GetComponent<PlayerMovement>().pauseGame();
        player.GetComponent<Inventory>().pauseGame();
        cam.GetComponent<CameraController>().pauseGame();
    }

    public void nextPage(){
        if(pageNum == 0){
            pageNum = 1;
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else if(pageNum == 1){
            pageNum = 2;
            page2.SetActive(false);
            page3.SetActive(true);
        }
    }

    public void backPage(){
        if(pageNum == 2){
            pageNum = 1;
            page3.SetActive(false);
            page2.SetActive(true);
        }
        else if(pageNum == 1){
            pageNum = 0;
            page2.SetActive(false);
            page1.SetActive(true);
        }
    }


}
