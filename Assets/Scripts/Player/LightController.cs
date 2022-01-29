using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light characterHalo;

    public float startConsumptionRate = 0.01f;
    public float consumptionRate = 0.01f;
    public float maxLightRange = 3f;

    // Update is called once per frame
    void Update()
    {
        characterHalo.range -= Time.deltaTime * consumptionRate;
        //emisionMaterial.SetColor("Emission", new Color(1,1,1,emissionIntensity));
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