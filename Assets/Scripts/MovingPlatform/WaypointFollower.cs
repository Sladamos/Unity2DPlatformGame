using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private bool cyclicalMovement = false;

    private int currentWaypoint = 0;
    private bool goingToLastWaypoint = true;

    private void Update()
    {
        if (ReachedTheWaypoint())
        {
            ChangeMovementDirectionIfNecessary();
            UpdateCurrentWaypoint();
        }
        else
        {
            MoveToCurrentWaypoint();
        }
    }

    private bool ReachedTheWaypoint()
    {
        return Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f;
    }

    private void ChangeMovementDirectionIfNecessary()
    {
        if (cyclicalMovement)
        {
            return;
        }

        if (ReachedLastWaypoint() || ReachedFirstWaypoint())
        {
            goingToLastWaypoint = !goingToLastWaypoint;
        }
    }

    private bool ReachedLastWaypoint()
    {
        return currentWaypoint == waypoints.Length - 1 && goingToLastWaypoint;
    }

    private bool ReachedFirstWaypoint()
    {
        return currentWaypoint == 0 && !goingToLastWaypoint;
    }

    private void UpdateCurrentWaypoint()
    {
        if(goingToLastWaypoint)
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;
        }
        else
        {
            currentWaypoint--;
        }
    }

    private void MoveToCurrentWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
    }
}
