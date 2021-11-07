using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformManager : MonoBehaviour
{
    public List<Transform> point;
    public Transform platform;
    int goalPoint;
    public float moveSpeed = 2;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        platform.position = Vector2.MoveTowards(platform.position, point[goalPoint].position, Time.deltaTime * moveSpeed);

        if (Vector2.Distance(platform.position, point[goalPoint].position) < 0.1f)
        {
            if (goalPoint == point.Count - 1)
            {
                goalPoint = 0;
            }
            else
            {
                goalPoint++;
            }
        }
    }
}
