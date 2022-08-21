using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GolfShooting ballScript;

    Renderer[] renderers;
    bool isRendered = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ChangePosition();
        HideCursor();
    }

    void FixedUpdate()
    {
        ChangeRotation();
    }

    void ChangeRotation()
    {
        Vector3 newDirection = Camera.main.transform.forward.normalized;
        newDirection.y = 0f;

        Quaternion lookRotation = Quaternion.LookRotation(newDirection);
        transform.rotation = lookRotation;
    }

    void ChangePosition()
    {
        Vector3 newPosition = ball.transform.position;
        newPosition.y = ball.transform.position.y - (ball.transform.localScale.y / (ball.transform.localScale.y * 13f));
        transform.position = newPosition;
    }

    void HideCursor()
    {
        // todo refactor this (because it kinda ugly)
        // Logic: if the ball is stationary and not rendered, render and vice versa

        if(ballScript.CheckStationary())
        {
            if(!isRendered)
            {
                isRendered = true;
                foreach (Renderer r in renderers)
                    r.enabled = true;
            }
        } 
        else
        {
            if(isRendered)
            {
                isRendered = false;
                foreach (Renderer r in renderers)
                    r.enabled = false;
            }
        }
    }
}
