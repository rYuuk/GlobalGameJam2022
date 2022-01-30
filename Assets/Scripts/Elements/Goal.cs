using System.Collections;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class Goal : MonoBehaviour
{
    [SerializeField] private Transform nextLevelStartPosition;
    [SerializeField] private bool isGoalReached;

    [Header("Camera Shake")] [SerializeField]
    private float duration = 0.5f;

    [SerializeField] private float strength = 0.5f;
    [SerializeField] private float randomness = 90f;
    [SerializeField] private int vibrato = 10;


    [Inject] private Player player;
    [Inject] private Camera mainCamera;


    private void OnTriggerEnter(Collider other)
    {
        if (isGoalReached)
        {
            return;
        }

        isGoalReached = true;

        StartCoroutine(MovePlayerToNextLevel());
    }

    private IEnumerator MovePlayerToNextLevel()
    {
        mainCamera.transform.DOShakePosition(duration: duration, strength: strength, vibrato: vibrato,
            randomness: randomness);
        mainCamera.transform.DOShakeRotation(duration, strength, vibrato, randomness);
        yield return new WaitForSeconds(3f);
        player.transform.position = nextLevelStartPosition.position;
    }
}
