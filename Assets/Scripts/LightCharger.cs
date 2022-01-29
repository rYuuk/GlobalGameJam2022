using UnityEngine;

public class LightCharger : MonoBehaviour
{
    [SerializeField] private Light spotLight;
    [SerializeField] private float consumeRatio = 0.15f;
    public bool isOnCooldown;
    public float timer;
    private float rechargeTime = 5.5f;

    // Update is called once per frame
    void Update()
    {
        if (spotLight.range <= 0f && !isOnCooldown)
        {
            isOnCooldown = true;
        }

        if (isOnCooldown)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                isOnCooldown = false;
                timer = 0;
            }
        }
        else
        {
            if (spotLight.range < 5f)
            {
                spotLight.range += Time.deltaTime * 1.25f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            spotLight.range -= Time.deltaTime * consumeRatio;
        }
    }
}