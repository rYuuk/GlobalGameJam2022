using DG.Tweening;
using UnityEngine;

public class AbsorberController : MonoBehaviour
{

    private LightController playerLight;
    private Vector3 startPosition;
    private Vector3 movePosition;
    public float consumptionAffector = 0.05f;

    private void Awake()
    {
        playerLight = FindObjectOfType<LightController>();
        startPosition = transform.position;
        movePosition = startPosition + (Vector3.up*1.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPosition)
        {
            transform.DOMoveY(movePosition.y,0.75f);
        }else if (transform.position == movePosition)
        {
            transform.DOMoveY(startPosition.y,0.5f);
        }
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
