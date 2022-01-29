using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    public Light characterHalo;
    [Range(0,3)] private float lightLife = 3;
    public float startConsumptionRate = 0.01f;
    public float consumptionRate = 0.01f;
    public float maxLightRange = 3f;

    // Update is called once per frame
    void Update()
    {
        lightLife -= Time.deltaTime * consumptionRate;
        characterHalo.range = lightLife;
    }

    private void OnTriggerStay(Collider other)
    {
        if (characterHalo.range < maxLightRange)
        {
            if (other.GetComponent<LightCharger>())
            {
                characterHalo.range += 0.3f;    
            }
        }
    }
}