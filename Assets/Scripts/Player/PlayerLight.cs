using System;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [Range(0, 3)] [SerializeField] private float maxLightUnits = 3f;
    [SerializeField] private float currentLightUnits = 3;
    [SerializeField] private Light characterHalo;
    [SerializeField] private float defaultConsumptionRate;

    private float consumptionRate;

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
        currentLightUnits -= Time.deltaTime * consumptionRate;
        currentLightUnits = Mathf.Clamp(currentLightUnits, 0, maxLightUnits);
        characterHalo.range = currentLightUnits;
    }
}