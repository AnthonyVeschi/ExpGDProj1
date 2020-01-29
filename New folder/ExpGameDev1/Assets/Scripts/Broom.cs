using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    GameObject politician;
    public int damage;
    bool onCooldown = false;

    GameObject player;
    GameObject janitor;
    Animator anim;


    AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        a = gameObject.GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        janitor = player.transform.GetChild(4).gameObject;
        anim = janitor.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Politician")
        {
            politician = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Politician")
        {
            politician = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (politician != null && Input.GetMouseButtonDown(0) && !onCooldown)
        {
            anim.SetTrigger("whack");
            politician.GetComponent<Politician>().TakeDamage(damage);
            StartCoroutine(Cooldown());
            a.Play();
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(1.2f);
        onCooldown = false;
    }
}
