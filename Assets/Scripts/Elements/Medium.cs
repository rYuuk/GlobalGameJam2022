using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Medium : MonoBehaviour
{
    [SerializeField] private Transform dashTarget;
    [SerializeField] private float dashValue;
    [SerializeField] private float dashTime;
    [SerializeField] private AudioSource audioSource;

    private Vector3 Target;
    private float holdingTime;
    private Player player;
    private float fadeSpeed = 0.1f;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.EnableDash(dashTarget, dashValue, dashTime);
        StopAllCoroutines();
        StartCoroutine(FadeIn(1));
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        player.DisableDash();
        StopAllCoroutines();
        StartCoroutine(FadeOut(0));
    }

    private IEnumerator FadeIn(float targetValue)
    {
        audioSource.volume = 0;
        while (audioSource.volume < targetValue)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetValue;
    }
    
    private IEnumerator FadeOut(float targetValue)
    {
        while (audioSource.volume > targetValue)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetValue;
    }
}