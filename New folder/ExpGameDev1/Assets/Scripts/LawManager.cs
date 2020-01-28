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

    //level 1 laws
    public void enactClosedDoorLaw()
    {
        if (!noColorsLaw && !greenNewDealLaw) { closedDoorsLaw = true; }
    }
    public void enactNoColorsLaw()
    {
        if (!closedDoorsLaw && !greenNewDealLaw) { noColorsLaw = true; }
    }
    public void enactGreenNewDealLaw()
    {
        if (!closedDoorsLaw && !noColorsLaw) { greenNewDealLaw = true; }
    }

    //level 2 laws
    public void enactOpenDoorLaw()
    {
        if (!noLawsLaw && !sunglassesLaw) { openDoorsLaw = true; }
    }
    public void enactNoLawsLaw()
    {
        if (!openDoorsLaw && !sunglassesLaw) { noLawsLaw = true; }
    }
    public void enactSunglassesLaw()
    {
        if (!noLawsLaw && !openDoorsLaw) { sunglassesLaw = true; }
    }

    //level 3 laws
    public void enactXRayGlassesLaw()
    {
        if (!noFacesLaw && !skateOrDieLaw) { xRayGlassesLaw = true; }
    }
    public void enactNoFacesLaw()
    {
        if (!xRayGlassesLaw && !skateOrDieLaw) { noFacesLaw = true; }
    }
    public void enactSkateOrDieLaw()
    {
        if (!noFacesLaw && !xRayGlassesLaw) { skateOrDieLaw = true; }
    }

    //law helpers
    void ClosedDoorsHelper()
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

    void NoColorsHelper()
    {
    }

    void GreenNewDealHelper()
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

    void OpenDoorsHelper()
    {
        container = GameObject.Find("DoorSpawns");
        for (int i = 0; i < doorSpawns.Length; i++)
        {
            doorSpawns[i] = container.transform.GetChild(i);
        }
        foreach (Transform ds in doorSpawns)
        {
            if (ds.childCount != 0) { Destroy(ds.GetChild(0).gameObject); }
        }

        //activate the extra guard spawns here
    }

    void NoLawsHelper()
    {
    }

    void SunglassesHelper()
    {
        GameObject player = GameObject.Find("Player");
        GameObject sunglasses = player.transform.GetChild(1).GetChild(0).gameObject;
        sunglasses.SetActive(true);
    }

    void XRayGlassesHelper()
    {
    }

    void NoFacesHelper()
    {
    }

    void SkateOrDieHelper()
    {
    }

    //functions that execute once the levels have fully loaded
    public void Level2Loaded()
    {
        if (closedDoorsLaw) { ClosedDoorsHelper(); }
        if (noColorsLaw){ NoColorsHelper(); }
        if (greenNewDealLaw) { GreenNewDealHelper(); }
    }

    public void Level3Loaded()
    {
        Level2Loaded();
        if (openDoorsLaw) { OpenDoorsHelper(); }
        if (noLawsLaw) { NoLawsHelper(); }
        if (sunglassesLaw) { SunglassesHelper(); }
    }

    public void Level4Loaded()
    {
        Level3Loaded();
        if (xRayGlassesLaw) { }
        if (noFacesLaw) { }
        if (skateOrDieLaw) { }
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
