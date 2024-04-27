using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathPoint : MonoBehaviour
{
    
    public static Action inPoint = delegate { };

    public void goToPoint(EnemyController agent)
    {
        agent.navmeshagent.SetDestination(gameObject.transform.position);
        //agent.animation("walk");
    }
}