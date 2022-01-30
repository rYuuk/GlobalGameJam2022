using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using VContainer;

public class PlayerLight : MonoBehaviour, IResetable
{
    [Range(0, 3)] [SerializeField] private float maxLightUnits = 3f;
    [SerializeField] private float currentLightUnits = 3;
    [SerializeField] private Light characterHalo;
    [SerializeField] private float defaultConsumptionRate;

    [Inject] private Volume volume;

    public Action LightConsumed;

    private Vignette cameraVignette;
    private VolumeProfile volumeProfile;
    private float consumptionRate;
    private bool isLightConsumed;

    private void Awake()
    {
        ResetConsumptionRate();
        volumeProfile = volume.profile;
        if (volumeProfile != null && !volumeProfile.TryGet(out cameraVignette))
        {
            throw new NullReferenceException(nameof(cameraVignette));
        }
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

        cameraVignette.intensity.Override(CalculateClampedValue());

        if (currentLightUnits == 0)
        {
            isLightConsumed = true;
            LightConsumed?.Invoke();
        }
    }

    public void ResetState()
    {
        isLightConsumed = false;
        currentLightUnits = maxLightUnits;
        ResetConsumptionRate();
    }

    private float CalculateClampedValue()
    {
        return 1 - (currentLightUnits / maxLightUnits);
    }
}