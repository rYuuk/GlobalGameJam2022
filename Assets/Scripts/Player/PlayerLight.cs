using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerLight : MonoBehaviour, IResetable
{
    [Range(0, 3)] [SerializeField] private float maxLightUnits = 3f;
    [SerializeField] private float currentLightUnits = 3;
    [SerializeField] private Light characterHalo;
    [SerializeField] private float defaultConsumptionRate;

    public Action LightConsumed;
    public float CurrentLightUnits => currentLightUnits;

    private float consumptionRate;
    private bool isLightConsumed;


    private void Awake()
    {
        ResetConsumptionRate();
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
    }

    public void ResetState()
    {
        isLightConsumed = false;
        ResetConsumptionRate();
    }
}