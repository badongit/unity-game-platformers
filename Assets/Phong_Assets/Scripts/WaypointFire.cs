using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFire : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 5f;
    private float dirX = 0f;

    private SpriteRenderer sprite;
     

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();  
    }
    private void Update()
    {
        
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        dirX = transform.position.x;

        if (currentWaypointIndex==0)
        {
            sprite.flipX = true;
        }
        else if (currentWaypointIndex == 1)
        {
            sprite.flipX = false;
            Destroy(waypoints[currentWaypointIndex].gameObject);
        }

       

    }

}
