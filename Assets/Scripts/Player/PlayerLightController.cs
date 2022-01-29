using System;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    public Light characterHalo;
    [Range(0,3)] public float lightLife = 3;
    private bool isInCharger;
    private float startConsumptionRate = 0.01f;
    public float consumptionRate = 0.01f;
    public float maxLightRange = 3f;

    // Update is called once per frame
    void Update()
    {
        lightLife = isInCharger ? lightLife + 0.3f : lightLife - (Time.deltaTime * consumptionRate);
        lightLife = Mathf.Clamp(lightLife,0, 3f);
        characterHalo.range = lightLife;
    }

    private void OnTriggerStay(Collider other)
    {
        if (characterHalo.range < maxLightRange)
        {
            if (other.GetComponent<LightCharger>())
            {
                isInCharger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LightCharger>())
        {
            isInCharger = false;
        }

        if (other.GetComponent<AbsorberController>())
        {
            consumptionRate = startConsumptionRate;
        }
    }
}