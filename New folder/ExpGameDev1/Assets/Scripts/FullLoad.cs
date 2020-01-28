using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullLoad : MonoBehaviour
{
    GameObject lawManager;
    
    // Start is called before the first frame update
    void Start()
    {
        lawManager = GameObject.Find("LevelChanger");
    }

    // Update is called once per frame
    void Update()
    {
        lawManager.GetComponent<LawManager>().Level2Loaded();
        Destroy(gameObject);
    }
}
