using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour, IResetable
{
    [Range(0, 3)] [SerializeField] private float maxLightUnits = 3f;
    [SerializeField] private float currentLightUnits = 3;
    [SerializeField] private Light characterHalo;
    [SerializeField] private float defaultConsumptionRate;

    public Action LightConsumed;
    public float CurrentLightUnits => currentLightUnits;

    [SerializeField] private Vignette cameraVignette;
    [SerializeField] VolumeProfile volumeProfile;
    
    private float consumptionRate;
    private bool isLightConsumed;


    private void Awake()
    {
        ResetConsumptionRate();
        volumeProfile = Camera.main.GetComponent<Volume>()?.profile;
        if (!volumeProfile.TryGet(out cameraVignette)) throw new NullReferenceException(nameof(cameraVignette));
    }

    public void SetConsumptionRate(float rate)
    {
        consumptionRate = rate;
    }

    public void ResetConsumptionRate()
    {
        consumptionRate = defaultConsumptionRate;
    }

    private void Update()
    {
        if (isLightConsumed)
        {
            return;
        }

        currentLightUnits -= Time.deltaTime * consumptionRate;
        currentLightUnits = Mathf.Clamp(currentLightUnits, 0, maxLightUnits);
        characterHalo.range = currentLightUnits;

        if (currentLightUnits == 0)
        {
            isLightConsumed = true;
            LightConsumed?.Invoke();
        }
        
        cameraVignette.intensity.Override(CalculateClampedValue()); 
    }

    public void ResetState()
    {
        isLightConsumed = false;
        ResetConsumptionRate();
    }

    private float CalculateClampedValue()
    {
        var clampedValue = 1 - (currentLightUnits/maxLightUnits);
        return clampedValue;
    }

    public void ResetVignette() => cameraVignette.intensity.Override(0f); 
}