using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform shoulder;
    public Transform target;
    public Vector3 offset;
    public bool useOffset;
    public float cameraRotateSpeed;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;

    private Quaternion rotation;
    private Vector3 cameraPosition;
    private Vector3 targetPosition;
    private Vector3 offsetX;
    private Vector3 offsetY;

    bool listenToInput = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffset)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
        Cursor.lockState = CursorLockMode.Locked;

        transform.LookAt(shoulder);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (listenToInput)
        //rotate just camera if holding c
        if (Input.GetKey("c"))
        {
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * cameraRotateSpeed, -Vector3.up);

            Vector3 newPos = target.position + (cameraTurnAngle * (transform.position-target.position));
            transform.position = newPos;

            //checks if camera view is blocked by an object and if so adjusts the camera to not be blocked
            RaycastHit wallHit = new RaycastHit();
            if (Physics.Linecast(target.position, transform.position, out wallHit))
            {
                Debug.DrawLine(transform.position, target.position, Color.green);
                if (wallHit.collider.tag != "Collectible")
                {
                    transform.position = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z) + wallHit.normal;
                }
            }

            transform.LookAt(shoulder);
        }
        else if (Input.GetKeyUp("c"))
        {
            StartCoroutine("ResetCameraPosition");
            //ResetCameraPosition();
        }
        //else rotate camera and target together
        else
        {
            //gets X of mouse and roatates the target
            float horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed;
            target.Rotate(0, horizontal, 0);

            //gets Y of mouse and roatates the target
            float vertical = Input.GetAxis("Mouse Y") * cameraRotateSpeed;
            pivot.Rotate(-vertical, 0, 0);

            //player can move camera above 60 degrees
            if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
            {
                pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
            }

            //player can't move camera below -10 degrees
            if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
            {
                pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
            }

            //moves the camera based on current rotation of target
            float yAngle = target.eulerAngles.y;
            float xAngle = pivot.eulerAngles.x;
            rotation = Quaternion.Euler(xAngle, yAngle, 0);

            transform.position = target.position - (rotation * offset);

            //checks if camera view is blocked by an object and if so adjusts the camera to not be blocked
            RaycastHit wallHit = new RaycastHit();
            if (Physics.Linecast(transform.position, target.position, out wallHit))
            {
                Debug.DrawLine(transform.position, target.position, Color.green);
                if (wallHit.collider.tag != "Collectible" && wallHit.collider.tag != "Player")
                {
                        print("******************* " + wallHit.collider.name);
                    transform.position = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z) + wallHit.normal;
                }
            }
            transform.LookAt(shoulder);
        }
        transform.LookAt(shoulder);
        cameraPosition = transform.position;
    }

    IEnumerator ResetCameraPosition()
    {
        Vector3 startPos = transform.position;
        float startTime = Time.time;
        float distance = Vector3.Distance(transform.position, cameraPosition);
        //while(cameraPosition != transform.position)
        for(int i = 0; i<100; i++)
        {
            float distCovered = (Time.time - startTime) / 1f;
            float fractionOfJourney = distCovered / distance;
            print("Camera Pos: " + transform.position + " endPos: " + cameraPosition);
            transform.position = Vector3.Slerp(startPos, cameraPosition, Time.deltaTime * cameraRotateSpeed);
            if (cameraPosition == transform.position)
            {
                print("Exiting");
                break;
            }
            yield return 0;
        }
    }

    public void pauseGame()
    {
        listenToInput = false;
    }
    public void unPauseGame()
    {
        listenToInput = true;
    }
}
