using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfShooting : MonoBehaviour
{
    [SerializeField] float minHitForce = 10000f;
    [SerializeField] float maxHitForce = 50000f;
    [SerializeField] float changeInHitForce = 1000f;
    [SerializeField] Slider forceSliderUI;

    float hitForce = 10000f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Hit Force Initializers
        hitForce = minHitForce;
        forceSliderUI.minValue = minHitForce;
        forceSliderUI.maxValue = maxHitForce;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessChangeInForce();
        ProcessShot();
    }

    void ProcessChangeInForce()
    {
        // Mouse Wheel Up
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            forceSliderUI.value = forceSliderUI.value + changeInHitForce;
            hitForce = forceSliderUI.value;
        }
        // Mouse Wheel Down
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            forceSliderUI.value = forceSliderUI.value - changeInHitForce;
            hitForce = forceSliderUI.value;
        }
    }

    void ProcessShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
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
        rb.AddForce(GetShotDirection() * hitForce);
    }

    bool CheckStationary()
    {
        if (!Mathf.Approximately(rb.velocity.x, 0) || !Mathf.Approximately(rb.velocity.z, 0) || !Mathf.Approximately(rb.velocity.y, 0)) { return false; }
        return true;
    }
}
