using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [HideInInspector]//public to access it from other scripts w/o being in inspector
    public GameObject[] waypoints; //6 waypoints

    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint+1].transform.position; //counts up after it reaches the end

        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength/ speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPosition)) //if the gameobject reaches the end
        {
            if (currentWaypoint < waypoints.Length - 2) //if its not at the last waypoint
                //-1 for the last in the array, another -1 because endPos is currentwp+1 ?
            {
                currentWaypoint++; //
                lastWaypointSwitchTime= Time.time;
                //TODO: rotate
            }
            else
            {
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                //ToDO : deduct health
            }
        }
    }
}