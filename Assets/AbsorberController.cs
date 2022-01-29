using UnityEngine;

public class AbsorberController : MonoBehaviour
{

    private LightController playerLight;
    public float consumptionAffector = 0.05f;

    private void Awake()
    {
        playerLight = FindObjectOfType<LightController>();
    }

    private void OnTriggerStay(Collider other)
    {
        playerLight.consumptionRate += consumptionAffector;
    }

    private void OnTriggerExit(Collider other)
    {
        playerLight.consumptionRate = playerLight.startConsumptionRate;
    }
}
