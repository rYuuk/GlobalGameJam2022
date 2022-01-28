using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light characterHalo;

    public float consumptionRate = 0.01f;
    public float maxLightRange = 3f;
    public Material emisionMaterial;
    public float emissionIntensity;
    public float maxEmission;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        characterHalo.range -= Time.deltaTime * consumptionRate;
        emissionIntensity -= Time.deltaTime * consumptionRate;
        emisionMaterial.SetColor("Emission", new Color(1,1,1,emissionIntensity));
    }

    private void OnTriggerStay(Collider other)
    {
        if (characterHalo.range < maxLightRange)
        {
            characterHalo.range += 0.3f;
        }
        
        if (characterHalo.range < maxEmission)
        {
            emissionIntensity += 0.3f;
            emisionMaterial.SetColor("Emission", new Color(1,1,1,emissionIntensity));
        }
    }
}