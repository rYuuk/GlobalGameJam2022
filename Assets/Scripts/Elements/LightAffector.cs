using UnityEngine;
using VContainer;

public class LightAffector : MonoBehaviour
{
    [SerializeField] private float affectRate = 0.05f;

    [Inject] private PlayerLight playerLight;

    private void Awake()
    {
        playerLight = FindObjectOfType<PlayerLight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerLight.SetConsumptionRate(affectRate);
    }

    private void OnTriggerExit(Collider other)
    {
        playerLight.ResetConsumptionRate();
    }
}