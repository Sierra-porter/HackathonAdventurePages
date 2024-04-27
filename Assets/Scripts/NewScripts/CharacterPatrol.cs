using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navmeshagent;
    //public Animator animator;

    public List<PathPoint> pathPoints;
    private int currentIndex = 0;
    private bool isMovingForward = true;
    public bool isPathing = true;

    public static Action OnStopNavMeshAgentMovement = delegate { };
    



    public void MoveToNextPoint()
    {
        if (currentIndex >= 0 && currentIndex < pathPoints.Count)
        {
            pathPoints[currentIndex].goToPoint(this);
        }
    }
    private void Start()
    {
        OnStopNavMeshAgentMovement += onStopMovement;
        navmeshagent.updateRotation = true;
        
        MoveToNextPoint();
    }
    
    private bool wasMoving = false;
    private void Update()
    {
        bool isMoving = navmeshagent.velocity.magnitude > 0.1f;

        if (wasMoving && !isMoving)
        {
            OnStopNavMeshAgentMovement.Invoke();
        }

        wasMoving = isMoving;
    }

   /*
    public void animation(string name)
    {
        animator.SetBool("go_to_idle", false);
        animator.Play(name);
    }
    */

    private void onStopMovement()
    {
        //animator.SetBool("go_to_idle", true);
        if(!isPathing) return;

        if (isMovingForward)
        {
            // Если агент достиг последней точки маршрута, начать движение в обратном порядке
            if (currentIndex == pathPoints.Count - 1)
            {
                isMovingForward = false;
                currentIndex--;
            }
            else
            {
                currentIndex++;
            }
        }
        else
        {
            // Если агент достиг первой точки маршрута, начать движение вперед
            if (currentIndex == 0)
            {
                isMovingForward = true;
                currentIndex++;
            }
            else
            {
                currentIndex--;
            }
        }
        
        MoveToNextPoint();
    }


}