using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class LawManager : MonoBehaviour
{
    public bool xRayGlassesLaw = true;
    public bool openDoorsLaw = true;
    public bool closedDoorsLaw = true;
    public bool noFacesLaw = true;
    public bool noColorsLaw = true;
    public bool noLawsLaw = true;
    public bool skateOrDieLaw = true;
    public bool sunglassesLaw = true;
    public bool greenNewDealLaw = true;

    public int currentLevel = 1;

    GameObject container;

    public Transform[] doorSpawns;
    public GameObject door;

    public Transform[] plantSpawns;
    public GameObject plant;

    GameObject guard;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //level 1 laws
    public void enactClosedDoorLaw()
    {
        if (noColorsLaw && greenNewDealLaw) { closedDoorsLaw = false; }
    }
    public void enactNoColorsLaw()
    {
        if (closedDoorsLaw && greenNewDealLaw) { noColorsLaw = false; }
    }
    public void enactGreenNewDealLaw()
    {
        if (closedDoorsLaw && noColorsLaw) { greenNewDealLaw = false; }
    }

    //level 2 laws
    public void enactOpenDoorLaw()
    {
        if (noLawsLaw && sunglassesLaw) { openDoorsLaw = false; }
    }
    public void enactNoLawsLaw()
    {
        if (openDoorsLaw && sunglassesLaw) { noLawsLaw = false; }
    }
    public void enactSunglassesLaw()
    {
        if (noLawsLaw && openDoorsLaw) { sunglassesLaw = false; }
    }

    //level 3 laws
    public void enactXRayGlassesLaw()
    {
        if (noFacesLaw && skateOrDieLaw) { xRayGlassesLaw = false; }
    }
    public void enactNoFacesLaw()
    {
        if (xRayGlassesLaw && skateOrDieLaw) { noFacesLaw = false; }
    }
    public void enactSkateOrDieLaw()
    {
        if (noFacesLaw && xRayGlassesLaw) { skateOrDieLaw = false; }
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
        guard = GameObject.Find("Guard1 (2x law)");
        guard.GetComponent<MeshRenderer>().enabled = true;
        guard.GetComponent<BoxCollider>().enabled = true;
        guard.GetComponent<NavMeshAgent>().enabled = true;
        guard.GetComponent<GuardAIController>().enabled = true;
        guard = GameObject.Find("Guard2 (2x law)");
        guard.GetComponent<MeshRenderer>().enabled = true;
        guard.GetComponent<BoxCollider>().enabled = true;
        guard.GetComponent<NavMeshAgent>().enabled = true;
        guard.GetComponent<GuardAIController>().enabled = true;
    }

    void NoLawsHelper()
    {
    }

    void SunglassesHelper()
    {
        GameObject player = GameObject.Find("Player");
        GameObject sunglasses = player.transform.GetChild(0).GetChild(0).gameObject;
        sunglasses.GetComponent<MeshRenderer>().enabled = true;
    }

    void XRayGlassesHelper()
    {
        //active the shiz on the guards here
        GameObject.Find("Guard1").GetComponent<GuardAIController>().FasterGuards(5);
        GameObject.Find("Guard2").GetComponent<GuardAIController>().FasterGuards(5);
        GameObject.Find("Guard1 (2x law)").GetComponent<GuardAIController>().FasterGuards(5);
        GameObject.Find("Guard2 (2x law)").GetComponent<GuardAIController>().FasterGuards(5);
    }

    void NoFacesHelper()
    {
    }

    void SkateOrDieHelper()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().Skate();

        //increase guards' movespeed here
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
        if (xRayGlassesLaw) { XRayGlassesHelper(); }
        if (noFacesLaw) { NoFacesHelper(); }
        if (skateOrDieLaw) { SkateOrDieHelper(); }
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
