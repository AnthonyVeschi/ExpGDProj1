using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] guardPath;
    public Transform player;
    public float guardDistanceToCatch;

    private int i = 0;
    private bool incremented = false;
    private float distanceAway = 5f;
    private Vector3 playersLastSeenLocation;
    private bool targetPlayer = false;                  //currently tracking player location
    private bool lookingForPlayer = false;              //lost player but he is still wanted
    private float defaultGuardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(guardPath[i].position);
        defaultGuardSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        //temp holder to target player instead of normal guard path
        if (Input.GetKeyDown("i"))
        {
            TargetPlayer();
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
            //if player is in guards FOV
            if (ConeCheck(guardDistanceToCatch))
            {
                playersLastSeenLocation = player.transform.position;
                agent.SetDestination(playersLastSeenLocation);
            }
            //if guard reached last player location and cant see him go back to normal path
            else if(agent.remainingDistance <= 7.5f)
            {
                targetPlayer = false;
                lookingForPlayer = true;
                agent.speed = defaultGuardSpeed;
            }

        }
        if (lookingForPlayer)
        {
            if (ConeCheck(guardDistanceToCatch))
            {
                TargetPlayer();
            }
        }
    }

    void TargetPlayer()
    {
        print("Updating");
        targetPlayer = true;
        lookingForPlayer = false;
        playersLastSeenLocation = player.transform.position;
        agent.SetDestination(playersLastSeenLocation);
        FasterGuards(1.5f);

    }

    public bool ConeCheck(float distanceAway)
    {
        Vector3 targetDirection = player.transform.position - agent.transform.position;
        float angleToPlayer = (Vector3.Angle(targetDirection, transform.forward));

        //is the player in the guards field of view
        if(angleToPlayer >=-20 && angleToPlayer <= 20)
        {
            //checks if camera view is blocked by an object and if so adjusts the camera to not be blocked
            RaycastHit wallHit = new RaycastHit();
            //is there an object blocking the guards view of you ie. a wall
            if (Physics.Linecast(transform.position, player.transform.position, out wallHit))
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
                //ensure raycast hit player
                if (wallHit.collider.tag == "Player")
                {
                    //did the guard catch you
                    if(wallHit.distance <= distanceAway)
                    {
                        print("youve been caught");
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public void FasterGuards(float speedIncreaseFactor)
    {
        agent.speed *= speedIncreaseFactor;
    }
}
