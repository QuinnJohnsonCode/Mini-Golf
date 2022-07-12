using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Slider forceSliderUI;
    [SerializeField] GolfShooting shootScript;

    [SerializeField] float changeInHitForce = 10f;
    [SerializeField] float minHitForce = 10000f;
    [SerializeField] float maxHitForce = 50000f;
    [SerializeField] float sliderScaler = 10f;


    void Awake()
    {
        // Hit Force Initializers
        shootScript.setHitForce(maxHitForce);
        forceSliderUI.minValue = minHitForce;
        forceSliderUI.maxValue = maxHitForce;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessChangeInForce();
        ChangeArrowSize();
    }

    void ChangeArrowSize()
    {
        float newSize = (forceSliderUI.value / maxHitForce) * sliderScaler;
        arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, arrow.transform.localScale.y, newSize);
    }



    void ProcessChangeInForce()
    {
        // -1 or 1
        float scrollDirection = Input.GetAxisRaw("Mouse ScrollWheel") * 10;
        forceSliderUI.value += changeInHitForce * scrollDirection;
        shootScript.setHitForce(forceSliderUI.value);
    }
}
