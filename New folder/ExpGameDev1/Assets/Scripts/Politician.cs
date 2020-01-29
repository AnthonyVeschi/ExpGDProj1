using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Politician : MonoBehaviour
{
    public int myLaw;
    GameObject lawManager;
    public int health = 5;

    GameObject janitor;
    Animator anim;

    void Start()
    {
        janitor = transform.GetChild(0).gameObject;
        anim = janitor.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        lawManager = GameObject.Find("LevelChanger");

        switch (myLaw)
        {
            case (1):
                lawManager.GetComponent<LawManager>().enactClosedDoorLaw();
                break;
            case (2):
                lawManager.GetComponent<LawManager>().enactNoColorsLaw();
                break;
            case (3):
                lawManager.GetComponent<LawManager>().enactGreenNewDealLaw();
                break;
            case (4):
                lawManager.GetComponent<LawManager>().enactOpenDoorLaw();
                break;
            case (5):
                lawManager.GetComponent<LawManager>().enactNoLawsLaw();
                break;
            case (6):
                lawManager.GetComponent<LawManager>().enactSunglassesLaw();
                break;
            case (7):
                lawManager.GetComponent<LawManager>().enactXRayGlassesLaw();
                break;
            case (8):
                lawManager.GetComponent<LawManager>().enactNoFacesLaw();
                break;
            case (9):
                lawManager.GetComponent<LawManager>().enactSkateOrDieLaw();
                break;
        }

        //Destroy(gameObject);
        anim.SetTrigger("dead");
    }
}
