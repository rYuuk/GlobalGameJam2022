using UnityEngine;
using VContainer;

public class LightAffector : MonoBehaviour
{
    [SerializeField] private float affectRate = 0.05f;
    [Range(0, 3)] [SerializeField] private float capacity = 3;
    [SerializeField] private bool infiniteCapacity;

    [Inject] private PlayerLight playerLight;

    private bool active;
    private float capacityReached;

    private void Awake()
    {
        playerLight = FindObjectOfType<PlayerLight>();
    }

    private void Update()
    {
        if (active)
        {
            if (capacityReached > capacity && !infiniteCapacity)
            {
                ResetConsumption();
            }

            capacityReached -= Time.deltaTime * affectRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        active = true;
        playerLight.SetConsumptionRate(affectRate);
    }

    private void OnTriggerExit(Collider other)
    {
        ResetConsumption();
    }

    private void ResetConsumption()
    {
        active = false;
        playerLight.ResetConsumptionRate();
    }
}