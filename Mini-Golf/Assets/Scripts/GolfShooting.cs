using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfShooting : MonoBehaviour
{
    [SerializeField] Slider forceSliderUI;
    [SerializeField] float yOutOfBounds = -3;


    Vector3 lastShotPosition;
    float hitForce = 10000f;
    Rigidbody rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        lastShotPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessShot();
        OutOfBoundsCheck();
    }

    void ProcessShot()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void OutOfBoundsCheck()
    {
        if(transform.position.y <= yOutOfBounds)
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        rb.velocity = Vector3.zero;
        transform.position = lastShotPosition;
    }

    Vector3 GetShotDirection()
    {
        Vector3 newDirection = Camera.main.transform.forward.normalized;
        newDirection.y = 0f;
        return newDirection;
    }

    void Shoot()
    {
        // Check if ball's velocity is about 0, if not, return (Stationary Check)
        if (!CheckStationary()) { return; }
        lastShotPosition = transform.position;
        rb.AddForce(GetShotDirection() * hitForce);
    }

    public bool CheckStationary()
    {
        if (!Mathf.Approximately(rb.velocity.x, 0) || !Mathf.Approximately(rb.velocity.z, 0) || !Mathf.Approximately(rb.velocity.y, 0)) { return false; }
        return true;
    }

    public void setHitForce(float force)
    {
        hitForce = force;
    }
}
