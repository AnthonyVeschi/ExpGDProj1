using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] guardPath;
    public Transform player;
    public bool targetPlayer = false;

    private int i = 0;
    private bool incremented = false;
    private float distanceAway = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(guardPath[i].position);
    }

    // Update is called once per frame
    void Update()
    {
        //temp holder to target player instead of normal guard path
        if (Input.GetKeyDown("i"))
        {
            targetPlayer = !targetPlayer;
        }

        if (!targetPlayer)
        {
            //set next destination for guard
            if (agent.remainingDistance < 7.5f && System.Math.Abs(agent.remainingDistance) > Mathf.Epsilon && !incremented)
            {
                print("incremented to: " + i);
                i++;
                if (i == guardPath.Length)
                {
                    i = 0;
                }
                agent.SetDestination(guardPath[i].position);
                incremented = true;

            }
            if (agent.remainingDistance > 10f)
            {
                incremented = false;
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
