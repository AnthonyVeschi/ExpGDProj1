using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LawManager : MonoBehaviour
{
    public bool xRayGlassesLaw = false;
    public bool openDoorsLaw = false;
    public bool closedDoorsLaw = false;
    public bool noFacesLaw = false;
    public bool noColorsLaw = false;
    public bool noLawsLaw = false;
    public bool skateOrDieLaw = false;
    public bool sunglassesLaw = false;
    public bool greenNewDealLaw = false;

    public int currentLevel = 1;

    GameObject container;

    public Transform[] doorSpawns;
    public GameObject door;

    public Transform[] plantSpawns;
    public GameObject plant;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void enactClosedDoorLaw()
    {
        if (!noColorsLaw && !greenNewDealLaw) { closedDoorsLaw = true; }
    }
    void enactNoColorsLaw()
    {
        if (!closedDoorsLaw && !greenNewDealLaw) { noColorsLaw = true; }
    }
    void enactGreenNewDealLaw()
    {
        if (!closedDoorsLaw && !noColorsLaw) { greenNewDealLaw = true; }
    }

    public void Level2Loaded()
    {
        if (closedDoorsLaw)
        {
            container = GameObject.Find("DoorSpawns");
            for (int i = 0; i < doorSpawns.Length; i++)
            {
                doorSpawns[i] = container.transform.GetChild(i);
            }
            foreach (Transform ds in doorSpawns)
            {
                if (ds.childCount == 0) { Instantiate(door, ds, false); }
            }
        }
        if (noColorsLaw)
        {
        }
        if (greenNewDealLaw)
        {
            container = GameObject.Find("PlantSpawns");
            for (int i = 0; i < plantSpawns.Length; i++)
            {
                plantSpawns[i] = container.transform.GetChild(i);
            }
            foreach (Transform ps in plantSpawns)
            {
                if (ps.childCount == 0) { Instantiate(plant, ps, false); }
            }
        }
    }

    void LoadNextLevel()
    {
        switch (currentLevel)
        {
            case (1):
                SceneManager.LoadScene("SampleScene2");
                currentLevel = 2;
                break;
            case (2):
                SceneManager.LoadScene("SampleScene3");
                currentLevel = 3;
                break;
            case (3):
                SceneManager.LoadScene("SampleScene4");
                currentLevel = 4;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadNextLevel();
        }
    }
}
