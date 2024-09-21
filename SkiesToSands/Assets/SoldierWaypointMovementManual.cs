using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWaypointMovementManual : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints
    public float speed = 3f;       // Speed of the soldier's movement
    public float waypointRadius = 0.5f; // Radius to consider the waypoint reached
    private int currentWaypointIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotate to face the target waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        // Check if the soldier has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointRadius)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

