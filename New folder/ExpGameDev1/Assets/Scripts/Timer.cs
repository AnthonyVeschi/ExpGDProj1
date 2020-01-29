using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeStart;
    Text t;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = 120f;

        t = gameObject.GetComponent<Text>();
        t.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        t.text = Mathf.Round(timeStart).ToString();

        if (timeStart <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
