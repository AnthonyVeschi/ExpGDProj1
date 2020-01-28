using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullLoad : MonoBehaviour
{
    GameObject lawManager;
    public int thisLevel;

    // Start is called before the first frame update
    void Start()
    {
        lawManager = GameObject.Find("LevelChanger");
    }

    // Update is called once per frame
    void Update()
    {
        if (thisLevel == 2)
        {
            lawManager.GetComponent<LawManager>().Level2Loaded();
        }
        else if (thisLevel == 3)
        {
            lawManager.GetComponent<LawManager>().Level3Loaded();
        }
        else if (thisLevel == 4)
        {
            lawManager.GetComponent<LawManager>().Level4Loaded();
        }

        Destroy(gameObject);
    }
}
